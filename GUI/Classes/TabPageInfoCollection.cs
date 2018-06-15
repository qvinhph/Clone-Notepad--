using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI   
{
    public class TabPageInfoCollection: ICollection<TabPageInfo>
    {
        private List<TabPageInfo> list = new List<TabPageInfo>();


        #region Methods



        #endregion


        #region ICollection methods

        public int Count => list.Count;

        public bool IsReadOnly => false;

        public void Add(TabPageInfo item)
        {
            list.Add(item);
        }
        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(TabPageInfo item)
        {
            return list.Contains(item);
        }

        public bool Remove(TabPageInfo item)
        {
            return list.Remove(item);
        }

        public IEnumerator<TabPageInfo> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }


        #endregion


        #region ICollection methods not using

        public void CopyTo(TabPageInfo[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
