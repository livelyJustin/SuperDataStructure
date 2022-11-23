using System;
using System.Collections;
using System.Collections.Generic;

namespace Lulu
{
    public class Stack<T> : IEnumerable<T>
    {
        const int DefaultCapacity = 4;

        T[] array = null;

        int Capacity => array?.Length ?? 0;
        public int Count { private set; get; } = 0;
        int HaveSpace => Capacity - Count;



        public Stack() => SetArray(DefaultCapacity);
        public Stack(int capacity) => SetArray(capacity);

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
            }
            else if (curCapacity != capacity)
            {
                var old = array;
                array = new T[capacity];

                for (int no = 0; no < old.Length; ++no)
                    array[no] = old[no];
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
                throw new Exception("빈 Stack 입니다");
            return array[Count - 1];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<T> GetEnumerator()
        {
            for (int no = Count - 1; -1 < no; --no)
                yield return array[no];
        }



        public void Push(T value)
        {
            if (HaveSpace < 1)
                SetArray(Capacity << 1);

            array[Count] = value;
            ++Count;
        }

        public T Pop()
        {
            if (Count < 1)
                throw new Exception("빈 Stack 입니다");

            var result = array[Count - 1];
            array[Count - 1] = default;
            --Count;
            return result;
        }

        public void Clear()
        {
            for (int no = 0; no < Count; ++no)
                array[no] = default;
            Count = 0;
        }
    }
}