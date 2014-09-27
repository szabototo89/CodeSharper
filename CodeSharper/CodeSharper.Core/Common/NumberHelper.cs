using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.Common.ConstraintChecking;

namespace CodeSharper.Core.Common
{
    public static class NumberHelper
    {
        public static int Times(this int that, Action action)
        {
            Constraints
                .NotNull(() => action);

            return Times(that, value => action());
        }

        public static int Times(this int that, Action<int> action)
        {
            Constraints
                .NotNull(() => action);

            for (int i = 0; i < that; i++)
                action(i);

            return that;
        }

        public static IEnumerable<int> To(this int start, int stop)
        {
            var times = Math.Abs(start - stop);
            var direction = stop.CompareTo(start);

            for (var i = 0; i < times; i++)
            {
                yield return start + i * direction;
            }
        }
    }
}
