using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Justin
{
    public class List : StudyBase
    {
        protected override void OnLog()
        {
            Debug.Log("zz");
            var aList = new List<int>();
            aList.Add(2);
            // 2
            aList.LogValues();

            aList.Insert(0, 1);
            // 1, 2
            aList.LogValues();

            aList.Add(4);
            aList.Insert(aList.Count - 1, 3);
            // 1, 2, 3, 4
            aList.LogValues();

            aList.Remove(2);
            aList.RemoveAt(0);
            // 4
            Log(aList[aList.Count - 1]);

            Debug.Log("**********************************");

            var lList = new LinkedList<string>();

            lList.AddFirst("My name is");
            lList.AddLast("AlphaGo");
            lList.AddLast("Hi");
            lList.LogValues();

            // My name is, AlphaGo, Hi
            lList.Remove("Hi");
            lList.LogValues();

            lList.AddFirst("Hello");
            // Hello, My name is, AlphaGo
            lList.LogValues();

            lList.RemoveFirst();
            lList.AddLast("I'm glad to meet you");
            // My name is, AlphaGo, I'm glad to meet you
            lList.LogValues();
        }
    }
    public sealed class List<T> : IEnumerable<T>
    {
        public int Count { private set; get; } = 0;
        const int defaultCapacity = 4;
        public int curCapacity = defaultCapacity; // 

        T[] arr = new T[defaultCapacity];
        // 생성자 선언하는게 좋다.

        public T this[int index]
        {
            set {
                if (index >= Count || index < 0)
                    throw new Exception("인덱스가 잘못되었어연");

                arr[index] = value; } // throw를 할 수 있으면 더 좋다. index >= count, 음수
            get { return arr[index]; }
        }

        public bool Contains(T value)
        {
            foreach (var item in arr)
                if (EqualityComparer<T>.Default.Equals(item, value))
                    return true;

            return false;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return arr[i];
            }
        }


        public void Add(T value)
        {

            // throw

            // 총 수용 용량이 없으면 Capacity를 늘린다.
            if (Count >= curCapacity)
            {
                curCapacity = curCapacity * 2; // Array.Resize를 통해서도 가능
                T[] temp = new T[curCapacity];
                for (int i = 0; i < arr.Length; i++)
                {
                    temp[i] = arr[i];
                }
                arr = temp;
                arr[Count] = value;
                return;
            }

            // 이미 공간이 있을 때는 해당 공간에 바로 값 넣어준기
            arr[Count] = value;
            Count++;
        }
        public void Insert(int index, T value)
        {
            // 해당 공간 뒤로 미루고 채워 넣는다.

            // throw 빠짐

            // 접근하는 인덱스가 count 같으면 그냥 add를 해버린다.
            // 현재 허용량 보다 count가 크다면 capacity를 늘려야한다.
            if (Count == index)
            {
                Add(value);
                return;
            }

            if (Count + 1 >= curCapacity)
            {
                curCapacity = curCapacity * 2;
            }

            // 새로운 배열을 통해 값 옮겨 담기
            T[] temp = new T[curCapacity];
            // index이전 값 담기
            for (int i = 0; i < index; i++)
                temp[i] = arr[i];


            // 중간에 값 넣기
            temp[index] = value;

            // index이후 값 담기
            for (int i = index + 1; i < arr.Length; i++)
                temp[i] = arr[i - 1];

            arr = temp;
            Count++;
        }

        public bool Remove(T value)
        {
            if (arr[Count] == null)
                throw new Exception("Remove를 할 수 없습니다.");

            // 배열을 탐색하여 값이 있는지 체크
            for (int i = 0; i < arr.Length; i++)
            {
                if (EqualityComparer<T>.Default.Equals(arr[i], value))
                {
                    // arr[i] = default; // default 를 한 번만 해줘도 된다.
                    Count--;
                    // 지워진 부분(i) 부터 담아주기
                    for (int j = i; j < Count; j++)
                    {
                        arr[j] = arr[j+1]; 
                    }
                    arr[Count] = default;
                    return true;
                }
            }

            return false;
        }
        public void RemoveAt(int index)
        {
            if (arr[index] == null) // 실제 null 일 수 있음. 범위 검사를 해주는게 좋음 
                throw new Exception("해당 인덱스 없어연");

            arr[index] = default;
            // 해당 인덱스 부터 시작하여 뒤에 값들만 땡겨오기
            for(int i = index; i < Count; i++)
            {
                arr[i] = arr[i + 1];
            }
            Count--;
        }

        public void Clear()
        {
            for (int i = 0; i < Count; i++)
                arr[i] = default;

            Count = 0;
        }
    }


}

