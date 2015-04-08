using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common.Runnables;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common.ControlFlows
{
    public abstract class ControlFlowBase
    {
        /// <summary>
        /// Gets or sets the executor.
        /// </summary>
        public IExecutor Executor { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlFlowBase"/> class.
        /// </summary>
        protected ControlFlowBase(IExecutor executor)
        {
            Assume.NotNull(executor, "executor");
            Executor = executor;
        }

        /// <summary>
        /// Executes the specified parameter 
        /// </summary>
        public abstract Object Execute(Object parameter);
    }

    public abstract class ComplexControlFlowBase : ControlFlowBase, IHasChildren<ControlFlowBase>
    {
        /// <summary>
        /// Gets or sets children of this type
        /// </summary>
        public IEnumerable<ControlFlowBase> Children { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PipelineControlFlow"/> class.
        /// </summary>
        protected ComplexControlFlowBase(IEnumerable<ControlFlowBase> children, IExecutor executor)
            : base(executor)
        {
            Assume.NotNull(children, "children");
            Children = children;
        }
    }

    public class PipelineControlFlow : ComplexControlFlowBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PipelineControlFlow"/> class.
        /// </summary>
        public PipelineControlFlow(IEnumerable<ControlFlowBase> children, IExecutor executor)
            : base(children, executor)
        {
        }

        /// <summary>
        /// Executes the specified parameter
        /// </summary>
        public override Object Execute(Object parameter)
        {
            var result = parameter;
            foreach (var child in Children)
            {
                result = child.Execute(result);
            }
            return result;
        }
    }

    public class SequenceControlFlow : ComplexControlFlowBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceControlFlow"/> class.
        /// </summary>
        public SequenceControlFlow(IEnumerable<ControlFlowBase> children, IExecutor executor)
            : base(children, executor)
        {

        }

        /// <summary>
        /// Executes the specified parameter 
        /// </summary>
        public override Object Execute(Object parameter)
        {
            var result = parameter;
            foreach (var child in Children)
            {
                result = child.Execute(parameter);
            }
            return result;
        }
    }

    public class CommandCallControlFlow : ControlFlowBase
    {
        /// <summary>
        /// Gets or sets the command of call
        /// </summary>
        public Command Command { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandCallControlFlow"/> class.
        /// </summary>
        public CommandCallControlFlow(Command command, IExecutor executor)
            : base(executor)
        {
            Assume.NotNull(command, "command");
            Command = command;
        }

        /// <summary>
        /// Executes the specified parameter 
        /// </summary>
        public override Object Execute(Object parameter)
        {
            var runnable = Command.Runnable;
            return Executor.Execute(runnable, parameter);
        }
    }

    public class Command : IEquatable<Command>
    {
        /// <summary>
        /// Gets or sets the runnable.
        /// </summary>
        public IRunnable Runnable { get; protected set; }

        /// <summary>
        /// Gets or sets the command descriptor.
        /// </summary>
        public CommandDescriptor CommandDescriptor { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        public Command(IRunnable runnable, CommandDescriptor commandDescriptor)
        {
            Assume.NotNull(runnable, "runnable");
            Assume.NotNull(commandDescriptor, "commandDescriptor");

            Runnable = runnable;
            CommandDescriptor = commandDescriptor;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(Command other)
        {
            return EqualityHelper.IsNullOrReferenceEqual(other, this) ??
                   Runnable.Equals(other.Runnable) &&
                   CommandDescriptor.Equals(other.CommandDescriptor);
        }
    }

    public class CommandDescriptor : IEquatable<CommandDescriptor>
    {
        public static readonly CommandDescriptor Empty;

        /// <summary>
        /// Initializes the <see cref="CommandDescriptor"/> class.
        /// </summary>
        static CommandDescriptor()
        {
            Empty = new CommandDescriptor();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandDescriptor"/> class.
        /// </summary>
        public CommandDescriptor()
        {
            Arguments = Enumerable.Empty<ArgumentDescriptor>();
            CommandNames = Enumerable.Empty<String>();
        }

        /// <summary>
        /// Gets or sets the name of command
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the formal arguments of command
        /// </summary>
        public IEnumerable<ArgumentDescriptor> Arguments { get; set; }

        /// <summary>
        /// Gets or sets the alias names of command
        /// </summary>
        public IEnumerable<String> CommandNames { get; set; }

        /// <summary>
        /// Gets or sets the description of command
        /// </summary>
        public String Description { get; set; }

        #region Equality members

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Arguments != null ? Arguments.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (CommandNames != null ? CommandNames.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override Boolean Equals(Object obj)
        {
            return Equals(obj as CommandDescriptor);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(CommandDescriptor other)
        {
            return EqualityHelper.IsNullOrReferenceEqual(other, this) ??
                   String.Equals(Name, other.Name) &&
                   String.Equals(Description, other.Description) &&
                   Enumerable.SequenceEqual(Arguments, other.Arguments) &&
                   Enumerable.SequenceEqual(CommandNames, other.CommandNames);

        }

        #endregion

    }


    public class ArgumentDescriptor : IEquatable<ArgumentDescriptor>
    {
        /// <summary>
        /// Gets or sets a value indicating whether this argument is optional.
        /// </summary>
        public Boolean IsOptional { get; set; }

        /// <summary>
        /// Gets or sets the default value of argument
        /// </summary>
        public Object DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the type of the argument.
        /// </summary>
        public Type ArgumentType { get; set; }

        /// <summary>
        /// Gets or sets the name of the argument.
        /// </summary>
        public String ArgumentName { get; set; }

        #region Equality members

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                var hashCode = IsOptional.GetHashCode();
                hashCode = (hashCode * 397) ^ (DefaultValue != null ? DefaultValue.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ArgumentType != null ? ArgumentType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ArgumentName != null ? ArgumentName.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override Boolean Equals(Object obj)
        {
            return Equals(obj as ArgumentDescriptor);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(ArgumentDescriptor other)
        {
            return EqualityHelper.IsNullOrReferenceEqual(other, this) ??
                   String.Equals(ArgumentName, other.ArgumentName) &&
                   Equals(DefaultValue, other.DefaultValue) &&
                   IsOptional == other.IsOptional &&
                   ArgumentType == other.ArgumentType;
        }

        #endregion

    }

}