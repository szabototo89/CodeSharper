using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSharper.Core.Transformation
{
    public interface ICanAdd<in TElement>
    {
        /// <summary>
        /// Appends the specified element.
        /// </summary>
        void Add(TElement element);
    }
}
