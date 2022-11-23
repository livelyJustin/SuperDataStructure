// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;

// namespace Justin
// {
//     public class LinkedListNode<T>
//     {
//         public LinkedList<T> List { private set; get; } = null; // 어떤 리스트 속해 있는가?
//         public T Value { get; set; } = default; // 노드 담을 값

//         // 같은 어셈블리 내에서만 보이도록 선언
//         internal LinkedListNode<T> next { get; set; }
//         internal LinkedListNode<T> prev { get; set; }


//         // 메인문에서 호출할 때 사용하기 위함
//         public LinkedListNode<T> Prev => prev != null && this != List.Head ? prev : null;
//         public LinkedListNode<T> Next => next != null && next != List.Tail ? next : null;

//         //노드의 생성자로 list를 받아서 어디에 속해있는지 체크하기 위함
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
