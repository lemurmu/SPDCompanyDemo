using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLIDAR.BackEnd.UI
{
    class LinkQueue<T>
    {
        private LinkedListNode<T> front;//队列头指示器
        private LinkedList<T> _list = new LinkedList<T>();
        internal LinkedListNode<T> Front
        {
            get { return front; }
            set { front = value; }
        }
        private LinkedListNode<T> rear;//队列尾指示器

        internal LinkedListNode<T> Rear
        {
            get { return rear; }
            set { rear = value; }
        }
        private int nCount;//队列结点个数

        public int NCount
        {
            get { return nCount; }
            set { nCount = value; }
        }
        public LinkQueue()
        {
            front = rear = null;
            nCount = 0;
        }
        public int GetLength()
        {
            return nCount;
        }

        public void Clear()
        {
            front = rear = null;
            nCount = 0;
        }

        public bool IsEmpty()
        {
            if (front == rear && 0 == nCount)
            {
                return true;
            }
            return false;
        }

        public void Enqueue(T item)
        {
            LinkedListNode<T> p = new LinkedListNode<T>(item);
            if (IsEmpty())
            {
                front = rear = p;// 这步很重要 当第一个元素入队的时候，必须给front赋值，否则front一直都是null
                _list.AddFirst(p);
            }
            else
            {
                //rear.Next = p; 
                _list.AddAfter(rear,p);
                 rear = p;
            }
            ++nCount;
        }

        public T Dqueue()
        {
            if (IsEmpty())
            {
                Console.WriteLine("队列为空");
                return default(T);
            }
            LinkedListNode<T> p = front;//从队列头出对
            front = front.Next;
            _list.RemoveFirst();
            if (front == null)
            {
                rear = null;
            }
            --nCount;
            return p.Value;
        }
        //获取链队列头结点的值
        public T GetFront()
        {
            if (IsEmpty())
            {
                Console.WriteLine("队列为空");
                return default(T);
            }
            return front.Value;
        }
    }
}
