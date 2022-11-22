// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;

// namespace Justin
// {
//     public class LinkedListNode<T>
//     {
//         public LinkedList<T> List { private set; get; } = null;
//         public T Value { get; set; } = default;

//         // 같은 어셈블리 내에서만 보이도록 선언
//         internal LinkedListNode<T> next { get; set; }
//         internal LinkedListNode<T> prev { get; set; }


//         public LinkedListNode<T> Prev => prev != null && this != List.Head ? prev : null;
//         public LinkedListNode<T> Next => next != null && next != List.Tail ? next : null;

//         internal LinkedListNode(LinkedList<T> list)
//         {
//             if (list == null)
//                 throw new Exception($"{nameof(list)}가 null 입니다");

//             List = list;
//         }
//         internal void Release()
//         {
//             List = null;
//             prev = null;
//             next = null;
//             Value = default;
//         }
//     }
// }
