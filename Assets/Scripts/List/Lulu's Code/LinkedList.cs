using System;
using System.Collections;
using System.Collections.Generic;

namespace Lulu
{
    public class LinkedList<T> : IEnumerable<T>
    {
        public int Count { private set; get; } = 0;
        LinkedListNode<T> head = null;

        public LinkedListNode<T> First => head;
        public LinkedListNode<T> Last => head?.prev;



        public LinkedList() { }

        void SetLink(LinkedListNode<T> nodeAdd, LinkedListNode<T> nodeNext)
        {
            if (nodeAdd == null)
                throw new Exception($"{nameof(nodeAdd)}가 null 입니다");
            if (nodeNext == null)
                throw new Exception($"{nameof(nodeNext)}가 null 입니다");

            nodeAdd.prev = nodeNext.prev;
            nodeNext.prev.next = nodeAdd;

            nodeAdd.next = nodeNext;
            nodeNext.prev = nodeAdd;
        }



        public bool Contains(T value) => Find(value) != null;
        public LinkedListNode<T> Find(T value)
        {
            var node = head;

            while (node != null)
            {
                if (EqualityComparer<T>.Default.Equals(node.Value, value))
                    return node;

                node = node.next;
                if (node == head)
                    break;
            }
            return null;
        }
        public LinkedListNode<T> FindLast(T value)
        {
            var node = head?.prev;

            while (node != null)
            {
                if (EqualityComparer<T>.Default.Equals(node.Value, value))
                    return node;

                node = node.prev;
                if (node == head.prev)
                    break;
            }
            return null;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<T> GetEnumerator()
        {
            var node = head;

            while (node != null)
            {
                yield return node.Value;
                node = node.next;
                if (node == head)
                    break;
            }
        }



        public LinkedListNode<T> AddFirst(T value)
        {
            var nodeAdd = new LinkedListNode<T>(this) { Value = value, };

            if (head != null)
                SetLink(nodeAdd, head);
            else
            {
                nodeAdd.prev = nodeAdd;
                nodeAdd.next = nodeAdd;
            }

            head = nodeAdd;
            ++Count;
            return nodeAdd;
        }
        public LinkedListNode<T> AddLast(T value)
        {
            var nodeAdd = new LinkedListNode<T>(this) { Value = value, };

            if (head != null)
                SetLink(nodeAdd, head);
            else
            {
                nodeAdd.prev = nodeAdd;
                nodeAdd.next = nodeAdd;
                head = nodeAdd;
            }

            ++Count;
            return nodeAdd;
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            if (node == null)
                throw new Exception($"{nameof(node)}가 null 입니다");

            var nodeAdd = new LinkedListNode<T>(this) { Value = value, };
            SetLink(nodeAdd, node);
            if (node == head)
                head = nodeAdd;

            ++Count;
            return nodeAdd;
        }
        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
        {
            if (node == null)
                throw new Exception($"{nameof(node)}가 null 입니다");

            var nodeAdd = new LinkedListNode<T>(this) { Value = value, };
            SetLink(nodeAdd, node.next);

            ++Count;
            return nodeAdd;
        }



        public bool Remove(T value)
        {
            if (head == null)
                return false;

            var node = head;
            while (node != null)
            {
                if (EqualityComparer<T>.Default.Equals(node.Value, value))
                {
                    Remove(node);
                    return true;
                }

                node = node.next;
                if (node == head)
                    break;
            }
            return false;
        }
        public void Remove(LinkedListNode<T> node)
        {
            if (node == null)
                throw new Exception($"{nameof(node)}가 null 입니다");
            if (node.List != this)
                throw new Exception("관리 대상이 아닙니다");

            if (head == null)
            {
                node.Release();
                Count = 0;
                return;
            }


            if (node == head)
            {
                if (node.next == head)
                {
                    head = null;
                    node.Release();
                    Count = 0;
                    return;
                }
                else
                    head = node.next;
            }

            node.prev.next = node.next;
            node.next.prev = node.prev;
            node.Release();
            --Count;
        }

        public void RemoveFirst()
        {
            if (head != null)
                Remove(head);
        }
        public void RemoveLast()
        {
            if (head != null)
                Remove(head.prev);
        }
        public void Clear()
        {
            var node = head;
            while (node != null)
            {
                var next = node.next;
                node.Release();
                if (next == head)
                    break;

                node = next;
            }

            head = null;
            Count = 0;
        }
    }
}