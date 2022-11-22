using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public sealed class Queue<T> : IEnumerable<T>
{
    // 링버퍼 구조로 구현하세요
    int front = -1;
    int rear = -1;
    int capacity;
    const int defaultCapacity = 4;
    public int Count { private set; get; } = 0;
    T[] arr;

    public Queue()
    {
        capacity = defaultCapacity;
        arr = new T[capacity];
    }

    public T Peek()
    {
        if (front == -1)
            throw new Exception($"Queue is Empty");

        return arr[front];
    }

    public bool Contains(T value)
    {
        if (front == -1)
            throw new Exception($"Queue is Empty"); // 정책에 따라 False가 조금 더 적합.

        for (int i = front; i < Count + front; i++)
        {
            if (EqualityComparer<T>.Default.Equals(arr[i % arr.Length], value))
                return true;
        }

        return false;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<T> GetEnumerator()
    {
        if (front == -1)
            throw new Exception($"Queue is Empty"); // 정책에 따라 False가 조금 더 적합.

        for (int i = front; i < Count + front; i++)
            yield return arr[i % arr.Length];
    }

    public void Enqueue(T value)
    {
        /* 엔큐할 때 null 값이 들어오면 어떻게 들어올것인가 -> 확고한 규칙으로 Null을 막아야하는게 아니라면, Null도 받을 수 있어야한다.
        int? value; -> Nullable<T>: 초기화가 되어있는지 체크하는 bool 값까지 들어있다.
        */
        // if(value == null) -> null 값을 담아두고 싶을 때도 있을 거 같다.
        //     throw new Exception($"{nameof(value)}is not vaild");

        // if (EqualityComparer<T>.Default.Equals(value, default(T)))
        //     throw new Exception($"{nameof(value)}is not vaild");

        if (capacity <= Count + 1)
        {
            capacity = capacity << 1;
            Array.Resize(ref arr, capacity);
        }

        if (front == -1)
            front++;

        Count++;
        /* 순환구조로 수정 필요

        front가 1보다 크거나 같으면 면 배열에 이동이 있던 것이니 
        rear가 capacity에 도달했을 때 count 가 여유가 있으면 다시 나눠준다.

        */
        // arr[rear] = value;

        arr[++rear] = value;
    }


    public T Dequeue()
    {
        if (front == -1)
            throw new Exception($"Queue is Empty");

        T returnData = arr[front];
        arr[front] = default;

        if (front == rear)
            front = rear = -1;
        else
            front++;

        Count--;

        return returnData;
    }

    public void Clear()
    {
        if (front == -1)
            throw new Exception($"Queue is Empty");

        for (int i = front; i < Count + front; i++)
            arr[i % arr.Length] = default;

        front = rear = -1;
        Count = 0;
    }
}
