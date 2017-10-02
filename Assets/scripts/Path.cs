// -----------------------------------------------------------------------------------------------
//  Creation date :  13.06.2017
//  Project       :  Myosotis Village
//  Authors       :  Andrea Zirn, Joel Blumer, Patrick Del Conte
// -----------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.scripts
{
    /// <summary>
    /// Helper class for storing paths and using them
    /// </summary>
    public class Path : IList<Vector2>
    {
        private List<Vector2> path;
        private int index = 0;

        public Path(int count)
        {
            this.path = new List<Vector2>(count);
        }

        public Vector2 Dequeue()
        {
            return path[index++];
        }

        public Vector2 Peek()
        {
            return path[index];
        }

        public Vector2 Peek(int i)
        {
            return path[index + i];
        }

        public Boolean hasNext()
        {
            return Count > 0;
        }

        public Boolean hasNext(int i)
        {
            return index + i < Count;
        }

        public Boolean hasPrevious(int i)
        {
            return index - i >= 0;
        }

        public void Enqueue(Vector2 v)
        {
            //TODO sanetize input
            path.Add(v);
        }

        public int Count
        {
            get { return ((IList<Vector2>)path).Count - index; }
        }

        public int TotalSize
        {
            get { return ((IList<Vector2>)path).Count; }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public Vector2 this[int index]
        {
            get { return ((IList<Vector2>)path)[index]; }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int IndexOf(Vector2 item)
        {
            return ((IList<Vector2>)path).IndexOf(item);
        }

        public void Insert(int index, Vector2 item)
        {
            ((IList<Vector2>)path).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Add(Vector2 item)
        {
            ((IList<Vector2>)path).Add(item);
        }

        public void Clear()
        {
            ((IList<Vector2>)path).Clear();
            index = 0;
        }

        public bool Contains(Vector2 item)
        {
            return ((IList<Vector2>)path).Contains(item);
        }

        public void CopyTo(Vector2[] array, int arrayIndex)
        {
            ((IList<Vector2>)path).CopyTo(array, arrayIndex);
        }

        public bool Remove(Vector2 item)
        {
            return ((IList<Vector2>)path).Remove(item);
        }

        public IEnumerator<Vector2> GetEnumerator()
        {
            var num = ((IList<Vector2>)path).GetEnumerator();
            var i = 0;
            while (i++ < index)
            {
                num.MoveNext();
            }
            return num;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var num = ((IList<Vector2>)path).GetEnumerator();
            var i = 0;
            while (i++ < index)
            {
                num.MoveNext();
            }
            return num;
        }
    }

}