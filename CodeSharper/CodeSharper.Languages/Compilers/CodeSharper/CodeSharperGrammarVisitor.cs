using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Antlr4.Runtime;
using CodeSharper.Core.Commands;
using CodeSharper.Core.Utilities;
using CodeSharper.Languages.Grammar;

namespace CodeSharper.Languages.Compilers.CodeSharper
{
    public class CodeSharperGrammarVisitor : CodeSharperGrammarBaseVisitor<Object>, IVisitor<ICommandCall>
    {
        private readonly List<CommandParameter> _parameters;
        private readonly Stack<CommandCallDescriptor> _commandCallDescriptors;
        private readonly Stack<ICommandCall> _commandCallTreeDescriptors;
        private CommandParameter _parameter;

        private struct CommandParameter
        {
            public String Name { get; set; }
            public Object Value { get; set; }
        }

        public CodeSharperGrammarVisitor()
        {
            _parameters = new List<CommandParameter>();
            _commandCallDescriptors = new Stack<CommandCallDescriptor>();
            _commandCallTreeDescriptors = new Stack<ICommandCall>();
        }

        public override Object VisitBooleanParameterValue(CodeSharperGrammarParser.BooleanParameterValueContext context)
        {
            _parameter.Value = parseBooleanValue(context);
            return base.VisitBooleanParameterValue(context);
        }

        public override Object VisitNumberParameterValue(CodeSharperGrammarParser.NumberParameterValueContext context)
        {
            _parameter.Value = parseNumberValue(context);
            return base.VisitNumberParameterValue(context);
        }

        public override Object VisitStringParameterValue(CodeSharperGrammarParser.StringParameterValueContext context)
        {
            _parameter.Value = parseStringValue(context);
            return base.VisitStringParameterValue(context);
        }

        public override Object VisitParameterName(CodeSharperGrammarParser.ParameterNameContext context)
        {
            _parameter.Name = context.ParameterName.Text;

            return base.VisitParameterName(context);
        }

        public override Object VisitNamedParameter(CodeSharperGrammarParser.NamedParameterContext context)
        {
            base.VisitNamedParameter(context);
            _parameters.Add(_parameter);
            return null;
        }

        public override Object VisitParameter(CodeSharperGrammarParser.ParameterContext context)
        {
            base.VisitParameter(context);
            _parameters.Add(_parameter);
            return null;
        }

        public override Object VisitParameters(CodeSharperGrammarParser.ParametersContext context)
        {
            _parameters.Clear();
            return base.VisitParameters(context);
        }

        public override Object VisitCommandCall(CodeSharperGrammarParser.CommandCallContext context)
        {
            base.VisitCommandCall(context);

            var commandCallName = context.CommandName.Text;

            _commandCallDescriptors.Push(
                new CommandCallDescriptor(
                    commandCallName,
                    arguments: _parameters.TakeWhile(parameter => String.IsNullOrWhiteSpace(parameter.Name))
                                          .Select(parameter => parameter.Value)
                                          .ToArray(),
                    namedArguments: _parameters
                                          .SkipWhile(parameter => String.IsNullOrWhiteSpace(parameter.Name))
                                          .ToDictionary(
                                                parameter => parameter.Name,
                                                parameter => parameter.Value)));

            return null;
        }

        public override Object VisitPipeLineCommandExpression(CodeSharperGrammarParser.PipeLineCommandExpressionContext context)
        {
            base.VisitPipeLineCommandExpression(context);
            return parseCommandCallOperator<PipeLineCommandCall>(_commandCallTreeDescriptors);
        }

        public override Object VisitAndCommandExpression(CodeSharperGrammarParser.AndCommandExpressionContext context)
        {
            base.VisitAndCommandExpression(context);
            return parseCommandCallOperator<LazyAndCommandCall>(_commandCallTreeDescriptors);
        }

        public override Object VisitOrCommandExpression(CodeSharperGrammarParser.OrCommandExpressionContext context)
        {
            base.VisitOrCommandExpression(context);
            return parseCommandCallOperator<LazyOrCommandCall>(_commandCallTreeDescriptors);
        }

        public override Object VisitSemicolonCommandExpression(CodeSharperGrammarParser.SemicolonCommandExpressionContext context)
        {
            base.VisitSemicolonCommandExpression(context);
            return parseCommandCallOperator<SequenceCommandCall>(_commandCallTreeDescriptors);
        }

        public override Object VisitSingleCommandExpression(CodeSharperGrammarParser.SingleCommandExpressionContext context)
        {
            base.VisitSingleCommandExpression(context); 
            if (_commandCallDescriptors.Any())
            {
                var descriptor = _commandCallDescriptors.Pop();
                _commandCallTreeDescriptors.Push(new SingleCommandCall(descriptor));
            }
            return null;
        }

        public override Object VisitCommands(CodeSharperGrammarParser.CommandsContext context)
        {
            _commandCallDescriptors.Clear();
            base.VisitCommands(context);

            return null;
        }

        public ICommandCall Visit(RuleContext context)
        {
            VisitStart(context as CodeSharperGrammarParser.StartContext);
            var result = _commandCallTreeDescriptors.Pop();
            return result;
        }

        private Object parseCommandCallOperator<TCommandCallOperator>(Stack<ICommandCall> commandCallTreeDescriptors)
            where TCommandCallOperator : ICommandCall, new()
        {
            var right = commandCallTreeDescriptors.Pop();
            var left = commandCallTreeDescriptors.Pop();

            var tree = new TCommandCallOperator();
            tree.AddCommandCallTree(left);
            tree.AddCommandCallTree(right);

            commandCallTreeDescriptors.Push(tree);

            return null;
        }

        private String parseStringValue(CodeSharperGrammarParser.StringParameterValueContext context)
        {
            var value = context.STRING();
            if (value == null)
                throw new Exception("Invalid Boolean Parameter Value Context!");

            var result = value.GetText();
            foreach (var ch in new[] { '"', '\'' })
            {
                if (result.StartsWith(ch.ToString()) || result.EndsWith(ch.ToString()))
                {
                    result = result.Trim(ch);
                    break;
                }
            }

            return result;
        }

        private Int32 parseNumberValue(CodeSharperGrammarParser.NumberParameterValueContext context)
        {
            var value = context.NUMBER();
            if (value == null)
                throw new Exception("Invalid Number Parameter Value Context!");

            Int32 result = 0;
            if (!Int32.TryParse(value.GetText(), NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                throw new Exception("Invalid number type!");

            return result;
        }

        private Boolean parseBooleanValue(CodeSharperGrammarParser.BooleanParameterValueContext context)
        {
            var value = context.BOOLEAN();
            if (value == null)
                throw new Exception("Invalid Boolean Parameter Value Context!");

            switch (value.GetText())
            {
                case "false":
                    return Option.Some(false);
                case "true":
                    return Option.Some(true);
                default:
                    throw new NotSupportedException("Not supported value of Boolean!");
            }
        }
    }
}
