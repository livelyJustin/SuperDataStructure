using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public sealed class Dictionary<K, T> : IEnumerable<KeyValuePair<K, T>>
{
    // buckets, entries 구현이 필요합니다.
    public int Count { private set; get; } = 0;

    // preidx와 같은 int값 필요 
    int capacity = 0;
    const int defaultCapacity = 4;
    Entry[] arr = null;

    // 테스트중
    int[] bucket = new int[4] { -1, -1, -1, -1 }; // 버켓을 초기화 할 때 -1로 모두 변경해주어야 한다.
    Entry[] entries = new Entry[4];


    public Dictionary()
    {
        arr = new Entry[defaultCapacity];
        capacity = defaultCapacity;
    }
    public T this[K key]
    {
        set
        {
            if (ContainsKey(key))
            {
                // 이미 키가 존재하니까 다른 키로 변경해주어야 한다.
                Debug.Log("value:? " + value);
                Add(key, value);
                return;
            }
            arr[ConvertHashIdx(key)].value = value;
        }
        get
        {
            var temp = arr[ConvertHashIdx(key)].value;
            if (temp == null)
                throw new Exception("해당 값은 비어있어욘");

            return arr[ConvertHashIdx(key)].value;
        }
    }

    int ConvertHashIdx(K key)
    {
        int hash = key.GetHashCode();
        int index = (hash % capacity);
        return index;
    }

    public bool ContainsKey(K key)
    {
        var temp = bucket[ConvertHashIdx(key)];
        if ((IEqualityComparer<KeyValuePair<K, T>>.Equals(arr[ConvertHashIdx(key)].key, default(K)))) // Key를 통해 버켓을 찾아서, 엔트리에서 값을 체크해야한다.
            return true;

        return false;
    }

    // public bool ContainsValue(T value)
    // {
    //     ...
    // 	}

    // 검색을 한 번만 하기 위해서 필요
    // public bool TryGetValue(K key, out T result)
    // {

    // }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<KeyValuePair<K, T>> GetEnumerator()
    {
        foreach (var e in bucket)
        {
            if (-1 != e) // val
            {
                // Debug.Log($"해쉬값: {  e.hashCode} 벨류: {e.value} 키: {e.key} ");
                yield return new KeyValuePair<K, T>(entries[e].key, entries[e].value);
            }
        }
    }

    void ReHashing()
    {
        // 크기를 키워주고 내용 담기
        capacity = capacity << 1;

        Entry[] newEntries = new Entry[capacity];
        int[] newBuck = new int[capacity];

        for (int i = 0; i < bucket.Length; i++)
        {
            if (bucket[i] != -1)
            {
                newBuck[i] = bucket[i];
                newEntries[i] = entries[i];
            }
        }

        entries = newEntries;
        bucket = newBuck;
    }


    public bool Add(K key, T value)
    {
        if (Count + 1 == capacity)
            ReHashing();

        int index = ConvertHashIdx(key);

        if (bucket[index] != -1)
        {
            for (int i = index; i < bucket.Length + index; i++)
            {
                int temp = i % bucket.Length;
                // next 처리 필요
                if (bucket[temp] == -1)
                {
                    entries[temp] = new Entry(key, value, 0, temp);
                    bucket[temp] = temp;
                    Count++;
                    return true;
                }
            }
            return false;
        }
        else
        {
            entries[index] = new Entry(key, value, 0, index);
            bucket[index] = index;
        }

        Count++;
        return true;
    }
    public bool Remove(K key)
    {
        if (ContainsKey(key))
        {
            int index = ConvertHashIdx(key);
            // bucket[index]
        }
        return false;
    }

    public void Clear()
    {
        for (int i = 0; i < capacity; i++)
        {
            bucket[i] = default;
            entries[i] = default;
        }

        Count = 0;
    }


    struct Entry
    {
        public int hashCode;
        public int next;
        public K key;
        public T value;

        public Entry(K _key, T _value, int _next, int _hashCode)
        {
            this.key = _key;
            this.value = _value;
            this.next = _next;
            this.hashCode = _hashCode;
        }
    }
}
