using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common;

namespace CodeSharper.Core.Texts
{
    public struct TextSpan 
    {
        private readonly TextLocation _start;
        private readonly TextLocation _stop;
        private readonly string _text;

        public TextSpan(TextLocation start, string text)
            : this(start, SpecifyStopTextLocation(text).Offset(start), text)
        {
                    
        }

        public TextSpan(TextLocation start, TextLocation stop, string text)
        {
            if (start.CompareTo(stop) > 0)
                throw ThrowHelper.ArgumentException(message: "Start must be lesser than stop!");

            _start = start;
            _stop = stop;
            _text = text;
        }

        public TextSpan(TextLocation start, TextLocation stop)
            : this(start, stop, null)
        {
            var distance = stop.GetDistanceFrom(start);
            _text = GenerateEmptyStringLiteral(distance);
        }

        private string GenerateEmptyStringLiteral(TextLocation distance)
        {
            var result = string.Empty;

            for (var i = 0; i < distance.Line; i++)
                result += Environment.NewLine;

            for (var i = 0; i < distance.Column; i++)
                result += " ";

            return result;
        }

        public TextSpan(string text)
            : this(SpecifyStartTextLocation(text),
                   SpecifyStopTextLocation(text),
                   text)
        {

        }

        public string Text
        {
            get { return _text; }
        }

        public TextLocation Start
        {
            get { return _start; }
        }

        public TextLocation Stop
        {
            get { return _stop; }
        }

        public TextSpan Offset(TextLocation location)
        {
            return new TextSpan(Start.Offset(location), Stop.Offset(location));
        }

        public override string ToString()
        {
            return String.Format("Start: {0}{1}Stop:{2}", Start, Environment.NewLine, Stop);
        }

        public static TextSpan FromString(string text)
        {
            HandleNullStringLiteral(text);
            return GenerateTextSpanFromString(text);
        }

        #region Private static methods for FromString static method

        private static void HandleNullStringLiteral(string text)
        {
            if (text == null)
                throw ThrowHelper.ArgumentNullException("text");
        }

        private static TextSpan GenerateTextSpanFromString(string text)
        {
            var start = SpecifyStartTextLocation(text);
            var stop = SpecifyStopTextLocation(text);

            return new TextSpan(start, stop, text);
        }

        private static TextLocation SpecifyStartTextLocation(string text)
        {
            return TextLocation.Zero;
        }

        private static TextLocation SpecifyStopTextLocation(string text)
        {
            var lines = GetLinesFromString(text);
            var lastLine = GetLastLine(lines);

            var stop = new TextLocation(lastLine.Length, lines.Length - 1, text.Length);
            return stop;
        }

        private static string GetLastLine(IEnumerable<string> lines)
        {
            return lines.LastOrDefault();
        }

        private static string[] GetLinesFromString(string text)
        {
            return text.Split(new[] { "\n" }, StringSplitOptions.None);
        }
        #endregion

        // TODO: It may problem when two TextSpan overlay each other or when this.Stop != span.Start
        public TextSpan Append(TextSpan span, bool ignorePositions = true)
        {
            return new TextSpan(this.Start, span.Stop, Text + span.Text);
        }
    }
}