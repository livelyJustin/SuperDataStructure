using System;
using System.Collections;
using System.Collections.Generic;

namespace Lulu
{
    public class Queue<T> : IEnumerable<T>
    {
        const int DefaultCapacity = 4;

        T[] array = null;
        int head = 0;
        int tail = 0;

        int Capacity => array?.Length ?? 0;
        public int Count { private set; get; } = 0;
        int HaveSpace => Capacity - Count;



        public Queue() => SetArray(DefaultCapacity);
        public Queue(int capacity) => SetArray(capacity);

        void SetArray(int capacity)
        {
            if (capacity < 1)
                throw new Exception("capacity가 1보다 작습니다");
            if (capacity < DefaultCapacity)
                capacity = DefaultCapacity;

            var curCapacity = Capacity;
            if (capacity < curCapacity)
                throw new Exception($"capacity가 {curCapacity}보다 작습니다");

            if (array == null)
            {
                array = new T[capacity];
                Count = 0;
                head = 0;
                tail = 0;
            }
            else if (curCapacity != capacity)
            {
                var old = array;
                array = new T[capacity];

                if (head < tail)
                {
                    for (int no = head; no < tail; ++no)
                        array[no - head] = old[no];
                }
                else
                {
                    int id = head;
                    for (; id < old.Length; ++id)
                        array[id - head] = old[id];

                    ++id;
                    for (int no = 0; no < tail; ++no)
                        array[id - head + no] = old[no];
                }

                head = 0;
                tail = Count;
            }
        }



        public bool Contains(T value)
        {
            for (int no = 0; no < Count; ++no)
            {
                if (EqualityComparer<T>.Default.Equals(array[no], value))
                    return true;
            }
            return false;
        }

        public T Peek()
        {
            if (Count < 1)
                throw new Exception("빈 Queue 입니다");
            return array[0];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<T> GetEnumerator()
        {
            if (Count < 1)
                yield break;

            if (head < tail)
            {
                for (int no = head; no < tail; ++no)
                    yield return array[no];
            }
            else
            {
                for (int no = head; no < array.Length; ++no)
                    yield return array[no];

                for (int no = 0; no < tail; ++no)
                    yield return array[no];
            }
        }



        public void Enqueue(T value)
        {
            if (HaveSpace < 1)
                SetArray(Capacity << 1);

            array[tail] = value;
            tail = (tail + 1) % array.Length;
            ++Count;
        }

        public T Dequeue()
        {
            if (Count < 1)
                throw new Exception("빈 Queue 입니다");

            var result = array[head];
            array[head] = default;
            head = (head + 1) % array.Length;
            --Count;
            return result;
        }

        public void Clear()
        {
            while (0 < Count)
                Dequeue();
            head = 0;
            tail = 0;
        }
    }
}