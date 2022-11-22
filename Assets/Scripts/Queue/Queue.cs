using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public sealed class Queue<T> : IEnumerable<T>
{
    // 링버퍼 구조로 구현하세요
    int front = 0;
    int rear = 0;
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
        if (Count <= 0)
            throw new Exception($"Queue is Empty");

        return arr[front];
    }

    public bool Contains(T value)
    {
        if (Count <= 0)
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
        if (Count <= 0)
            throw new Exception($"Queue is Empty"); // 정책에 따라 False가 조금 더 적합.

        for (int i = front; i < Count + front; i++)
            yield return arr[(i) % capacity];
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
        // if (front == -1)
        // {
        //     front = rear = 0;
        // }
        // else


        if ((rear + 1) % capacity == (front + 1 % capacity))
        {
            Debug.Log("array full: " + Count + capacity);
            // 범위가 부족하다면 늘려주고 
            if (capacity <= Count + 1)
            {
                Debug.Log("real array full");
                capacity = capacity << 1;
                Array.Resize(ref arr, capacity);
            }
            // 아니라면 배열의 처음부터 다시 채운다 (
        }
        // front = (front + 1) % arr.Length;
        arr[rear] = value;
        rear = (rear + 1) % capacity;
        Count++;
        // arr[rear] = value;

        // Debug.Log($"프론트: {front} {arr[front]} 리어: {rear} {arr[rear]}");
    }


    public T Dequeue()
    {
        if (Count <= 0)
            throw new Exception($"Queue is Empty");

        T returnData = arr[front];
        arr[front] = default;

        // if (front == rear)
        //     front = rear = -1;
        // else
        //     front++;

        front = (front + 1) % capacity;

        Count--;

        return returnData;
    }

    public void Clear()
    {
        if (Count <= 0)
            throw new Exception($"Queue is Empty");

        for (int i = front; i < Count + front; i++)
            arr[i % arr.Length] = default;

        front = rear = 0;
        Count = 0;
    }
}
