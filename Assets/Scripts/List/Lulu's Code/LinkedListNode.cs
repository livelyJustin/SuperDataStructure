using System;

namespace Lulu
{
    public class LinkedListNode<T>
    {
        public LinkedList<T> List { private set; get; } = null;

        internal LinkedListNode<T> prev = null;
        internal LinkedListNode<T> next = null;

        public LinkedListNode<T> Prev => prev != null && this != List.First ? prev : null;
        public LinkedListNode<T> Next => next != null && next != List.First ? next : null;
        public T Value { set; get; } = default;



        internal LinkedListNode(LinkedList<T> list)
        {
            if (list == null)
                throw new Exception($"{nameof(list)}가 null 입니다");

            List = list;
        }

        internal void Release()
        {
            List = null;
            prev = null;
            next = null;
            Value = default;
        }
    }
}