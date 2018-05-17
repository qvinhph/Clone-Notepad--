using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxHighlightingTextbox
{
    /// <summary>
    /// The collection of separators.
    /// </summary>
    public class SeparatorCollection: ICollection<char>
    {
        private List<char> separators = new List<char>();

        public int Count => separators.Count;

        public bool IsReadOnly => false;

        public void Add(char item)
        {
            separators.Add(item);
        }

        public void Clear()
        {
            separators.Clear();
        }

        public bool Contains(char item)
        {
            return separators.Contains(item);
        }

        public void CopyTo(char[] array, int arrayIndex)
        {
            separators.CopyTo(array, arrayIndex);
        }

        public IEnumerator<char> GetEnumerator()
        {
            return separators.GetEnumerator();
        }

        public bool Remove(char item)
        {
            return separators.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
