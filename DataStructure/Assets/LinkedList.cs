using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LinkedListNode<T>
{
	public T Value { get; set; }
	public LinkedListNode<T> NextNode {
		get;
		set;
	}
	public LinkedListNode<T> PrevNode { get; set; }
}

public sealed class LinkedList<T> : IEnumerable<T>
{
	// 양방향 링크로 구현하세요
	public int Count { private set; get; } = 0;
	//const int defaultCapacity = 4;

	LinkedListNode<T> head = null;
	LinkedListNode<T> current = null;
	LinkedListNode<T> tail = null;

	//T[] arr = new T[defaultCapacity];
    public LinkedListNode<T> First
	{
		get{ return head; }
		set{ this.head = value; } // 접근제한자 필요
	}
	public LinkedListNode<T> Last
	{
		get { return tail; }
		set { this.tail = value; }
		// head 가 null이 아니면 head.prev 를 하면 된다.
	}


	public bool Contains(T value)
	{
		// 1번 부터 뒤로 돌면서 찾기
		current = head;
        for (int i = 0; i < Count; i++)
        {
			if(EqualityComparer<T>.Default.Equals(current.Value, value))
				return true;

			current = current.NextNode;
		}
		return false;
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	public IEnumerator<T> GetEnumerator()
	{
		current = head;

		for (int i = 0; i < Count; i++)
		{
			//Debug.Log($"cur node: {current.Value}");
			yield return current.Value;
			current = current.NextNode;
		}
	}


	public LinkedListNode<T> AddFirst(T value)
	{
		// value 널체
		LinkedListNode<T> node = new LinkedListNode<T>(){
			Value = value,
			NextNode = head,
		};

		if(head == null)
        {
			head = node;
		 	tail = head;
			tail.PrevNode = head;
		}
		else
        {
			node.NextNode = head;
			head = node;
		}

		current = node;
		Count++;

		return current;
		}
	public LinkedListNode<T> AddLast(T value)
	{
		// value 널 체크
		LinkedListNode<T> node = new LinkedListNode<T>(){
			Value = value,
			//PrevNode = tail.PrevNode,
		};

		if (tail == null)
		{
			tail = node;
			tail.PrevNode = head;
			head = tail;
		}
		else
		{
			node.PrevNode = tail;
			tail.NextNode = node;
			tail = node;
		}

		current = node;
		Count++;

		return current;
	}

	public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
    {
		// 널 체크

		// 생성이 될 때
		LinkedListNode<T> newNode = new LinkedListNode<T>()
		{
			Value = value,
			NextNode = node,
			PrevNode = node.PrevNode,
		};

		// 원래 있던 노드와 연결
		node.PrevNode = newNode;
		current	= node.PrevNode.NextNode = newNode;
		Count++;
		// 처음이면 프론트에 연겴
		return newNode;
	}
    public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
    {
		LinkedListNode<T> newNode = new LinkedListNode<T>()
		{
			Value = value,
			NextNode = node.NextNode,
			PrevNode = node,
		};
		// 원래 있던 노드와 연결
		newNode.NextNode.PrevNode = newNode;
		node.NextNode = newNode;
		// 마지막이면 테일에 연결
		Count++;
		return newNode;
	}

    public bool Remove(T value)
    {
		current = head;
		for (int i = 0; i < Count; i++)
		{
			if (EqualityComparer<T>.Default.Equals(current.Value, value))
            {
				if (Count == i + 1)
				{
					RemoveLast();
					//Count--;
					return true;
				}

				current.NextNode.PrevNode = current.PrevNode;
				current.PrevNode.NextNode = current.NextNode;
				Count--;
				return true;
            }

			current = current.NextNode;
		}
			Count--;
		return false;
	}
    //public void Remove(LinkedListNode<T> node)
    //{
    //	if (node == null)
    //		throw new Exception($"{nameof(node)}가 null 입니다.");
    //	...
    //	}

    public void RemoveFirst()
    {
		//tail.PrevNode.NextNode = head;
		//tail = tail.PrevNode;

		tail.NextNode = head.NextNode;
		head = head.NextNode;

		Count--;
	}

    public void RemoveLast()
    {
		tail.PrevNode.NextNode = head;
		tail = tail.PrevNode;

		Count--;
		// 값 비워주기
	}

    public void Clear()
    {
		current = head;
		for (int i = 0; i < Count; i++)
		{
			// head, tail, current 다 비워주어야한다.
			current = current.NextNode;
		}
	}
}