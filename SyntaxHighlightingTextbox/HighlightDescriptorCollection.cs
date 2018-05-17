using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxHighlightingTextbox
{
    /// <summary>
    /// The collection of highlight descriptors.
    /// </summary>
    public class HighlightDescriptorCollection: ICollection<HighlightDescriptor>
    {
        private List<HighlightDescriptor> descriptors = new List<HighlightDescriptor>();

        public int Count => descriptors.Count;

        public bool IsReadOnly => false;

        public void Add(HighlightDescriptor item)
        {
            descriptors.Add(item);
        }

        public void Clear()
        {
            descriptors.Clear();
        }

        public bool Contains(HighlightDescriptor item)
        {
            return descriptors.Contains(item);
        }

        public void CopyTo(HighlightDescriptor[] array, int arrayIndex)
        {
            descriptors.CopyTo(array, arrayIndex);
        }

        public IEnumerator<HighlightDescriptor> GetEnumerator()
        {
            return descriptors.GetEnumerator();
        }

        public bool Remove(HighlightDescriptor item)
        {
            return descriptors.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
