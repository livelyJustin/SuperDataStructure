// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;

// namespace Justin
// {
//     public sealed class LinkedList<T> : IEnumerable<T>
//     {
//         // 양방향 링크로 구현하세요
//         public int Count { private set; get; } = 0;

//         LinkedListNode<T> head = null;
//         // LinkedListNode<T> current = null;
//         LinkedListNode<T> tail = null;

//         public LinkedListNode<T> Head
//         {
//             get { return head; }
//             private set { this.head = value; } // 접근제한자 필요
//         }
//         public LinkedListNode<T> Tail
//         {
//             get { return tail; }
//             private set { this.tail = value; }
//             // head 가 null이 아니면 head.prev 를 하면 된다.
//         }


//         public bool Contains(T value)
//         {
//             // 1번 부터 뒤로 돌면서 찾기
//             var node = head;
//             for (int i = 0; i < Count; i++)
//             {
//                 if (EqualityComparer<T>.Default.Equals(node.Value, value))
//                     return true;

//                 node = node.next;
//             }
//             return false;
//         }

//         IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
//         public IEnumerator<T> GetEnumerator()
//         {
//             var node = head;

//             for (int i = 0; i < Count; i++)
//             {
//                 //Debug.Log($"cur node: {current.Value}");
//                 yield return node.Value;
//                 node = node.next;
//             }
//         }

//         void ConnectAB(LinkedListNode<T> nodeAdd, LinkedListNode<T> nodeNext)
//         {
//             if (nodeAdd == null)
//                 throw new Exception($"{nameof(nodeAdd)}가 null 입니다");
//             if (nodeNext == null)
//                 throw new Exception($"{nameof(nodeNext)}가 null 입니다");

//             nodeAdd.prev = nodeNext.prev; // A노드의 이전 노드를 B노드의 이전 노드와 연결
//             nodeNext.prev.next = nodeAdd;

//             nodeAdd.next = nodeNext;
//             nodeNext.prev = nodeAdd;
//         }


//         public LinkedListNode<T> AddFirst(T value)
//         {
//             // value 널체
//             LinkedListNode<T> node = new LinkedListNode<T>(this)
//             {
//                 Value = value,
//                 next = head,
//             };

//             if (head == null)
//             {
//                 head = node;
//                 Tail = head;
//                 Tail.prev = head;
//             }
//             else
//             {
//                 node.next = head;
//                 head = node;
//             }

//             current = node;
//             Count++;

//             return current;
//         }
//         public LinkedListNode<T> AddLast(T value)
//         {
//             // value 널 체크
//             LinkedListNode<T> node = new LinkedListNode<T>(this)
//             {
//                 Value = value,
//                 //Prev = tail.Prev,
//             };

//             if (tail == null)
//             {
//                 tail = node;
//                 tail.Prev = head;
//                 head = tail;
//             }
//             else
//             {
//                 node.Prev = tail;
//                 tail.Next = node;
//                 tail = node;
//             }

//             current = node;
//             Count++;

//             return current;
//         }

//         public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
//         {
//             // 널 체크

//             // 생성이 될 때
//             LinkedListNode<T> newNode = new LinkedListNode<T>(this)
//             {
//                 Value = value,
//                 Next = node,
//                 Prev = node.Prev,
//             };

//             // 원래 있던 노드와 연결
//             node.Prev = newNode;
//             current = node.Prev.Next = newNode;
//             Count++;
//             // 처음이면 프론트에 연겴
//             return newNode;
//         }
//         public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
//         {
//             LinkedListNode<T> newNode = new LinkedListNode<T>(this)
//             {
//                 Value = value,
//                 Next = node.Next,
//                 Prev = node,
//             };
//             // 원래 있던 노드와 연결
//             newNode.Next.Prev = newNode;
//             node.Next = newNode;
//             // 마지막이면 테일에 연결
//             Count++;
//             return newNode;
//         }

//         public bool Remove(T value)
//         {
//             if (head == null)
//                 return false;

//             current = head;
//             for (int i = 0; i < Count; i++)
//             {
//                 if (EqualityComparer<T>.Default.Equals(current.Value, value))
//                 {
//                     if (Count == i + 1)
//                     {
//                         RemoveLast();
//                         //Count--;
//                         return true;
//                     }

//                     current.Next.Prev = current.Prev;
//                     current.Prev.Next = current.Next;
//                     Count--;
//                     return true;
//                 }

//                 current = current.Next;
//             }
//             Count--;
//             return false;
//         }
//         //public void Remove(LinkedListNode<T> node)
//         //{
//         //	if (node == null)
//         //		throw new Exception($"{nameof(node)}가 null 입니다.");
//         //	...
//         //	}

//         public void RemoveFirst()
//         {
//             //tail.Prev.Next = head;
//             //tail = tail.Prev;

//             tail.Next = head.Next;
//             head = head.Next;

//             Count--;
//         }

//         public void RemoveLast()
//         {
//             tail.Prev.Next = head;
//             tail = tail.Prev;

//             Count--;
//             // 값 비워주기
//         }

//         public void Clear()
//         {
//             current = head;
//             for (int i = 0; i < Count; i++)
//             {
//                 // head, tail, current 다 비워주어야한다.
//                 current = current.Next;
//             }
//         }
//     }
// }