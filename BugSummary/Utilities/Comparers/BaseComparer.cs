using System.Collections;

namespace Utilities.Comparers
{
    public abstract class BaseComparer<T> : IComparer where T : class
    {
        public int Compare(object x, object y)
        {
            var expected = x as T;
            var actual = y as T;

            var equals = ConcreteCompare(expected, actual);

            return equals ? 0 : 1;
        }

        protected abstract bool ConcreteCompare(T expected, T actual);
    }
}