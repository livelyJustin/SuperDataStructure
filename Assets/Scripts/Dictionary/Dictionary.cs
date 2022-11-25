// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;

// public sealed class Dictionary<K, T> : IEnumerable<KeyValuePair<K, T>>
// {
//     // buckets, entries 구현이 필요합니다.
//     public int Count { private set; get; } = 0;

//     // preidx와 같은 int값 필요 
//     int capacity = 0;
//     const int defaultCapacity = 4;
//     Entry[] arr = null;

//     // 테스트중
//     int[] bucket; // 버켓을 초기화 할 때 -1로 모두 변경해주어야 한다.
//     Entry[] entries;


//     public Dictionary()
//     {
//         arr = new Entry[defaultCapacity];
//         bucket = new int[defaultCapacity];
//         entries = new Entry[defaultCapacity];
//         capacity = defaultCapacity;

//         for (int i = 0; i < entries.Length; i++)
//         {
//             bucket[i] = -1;
//             entries[i].next = -1;
//             entries[i].hashCode = -1;
//         }
//     }
//     public T this[K key]
//     {
//         set
//         {
//             if (ContainsKey(key))
//             {
//                 // 이미 키가 존재하니까 다른 키로 변경해주어야 한다.
//                 Debug.Log("value:? " + value);
//                 Add(key, value);
//                 return;
//             }
//             arr[ConvertHashIdx(key)].value = value;
//         }
//         get
//         {
//             var temp = arr[ConvertHashIdx(key)].value;
//             if (temp == null)
//                 throw new Exception("해당 값은 비어있어욘");

//             return arr[ConvertHashIdx(key)].value;
//         }
//     }

//     int ConvertHashIdx(K key)
//     {
//         int hash = key.GetHashCode();
//         int index = (hash % capacity);
//         return index;
//     }

//     public bool ContainsKey(K key)
//     {
//         var temp = bucket[ConvertHashIdx(key)];
//         if ((IEqualityComparer<KeyValuePair<K, T>>.Equals(arr[ConvertHashIdx(key)].key, default(K)))) // Key를 통해 버켓을 찾아서, 엔트리에서 값을 체크해야한다.
//             return true;

//         return false;
//     }

//     // public bool ContainsValue(T value)
//     // {
//     //     ...
//     // 	}

//     // 검색을 한 번만 하기 위해서 필요
//     // public bool TryGetValue(K key, out T result)
//     // {

//     // }

//     IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
//     public IEnumerator<KeyValuePair<K, T>> GetEnumerator()
//     {
//         foreach (var e in bucket)
//         {
//             if (-1 != e) // val
//             {
//                 // Debug.Log($"해쉬값: {  e.hashCode} 벨류: {e.value} 키: {e.key} ");
//                 yield return new KeyValuePair<K, T>(entries[e].key, entries[e].value);
//             }
//         }
//     }

//     void ReHashing()
//     {
//         // 크기를 키워주고 내용 담기
//         capacity = capacity << 1;

//         Entry[] newEntries = new Entry[capacity];
//         // int[] newBuck = new int[capacity];

//         for (int i = 0; i < newEntries.Length; i++)
//         {
//             if (newEntries[i].hashCode != -1) // 버켓 크기는 채우지 않고, 엔트리스만 키우자
//             {
//                 // newBuck[i] = bucket[i]; // 버켓은 키우지 말자
//                 newEntries[i] = entries[i];
//             }
//         }

//         entries = newEntries;
//         // bucket = newBuck;
//     }


//     public bool Add(K key, T value)
//     {
//         // 범위 넓혀주기
//         if (Count + 1 == capacity)
//             ReHashing();

//         int index = ConvertHashIdx(key);

//         // 만약 해당 영역에 뭐가 있으면
//         if (bucket[index] != -1)
//         {
//             // Entry로 가서 해당 영역의 넥스트가 있는지 체크한다.
//             if (entries[index].next == -1) // 넥스트가 비어있을 때 남은 영역에 채운다.
//             {
//                 for (int i = 0; i < entries.Length; i++)
//                 {
//                     // 해당 Entry가 비어있는지는 Hashcode 값을 통해 비교
//                     if (entries[i].hashCode == -1)
//                     {
//                         entries[i].hashCode = index; // i는 Entry배열 속 위치일뿐이고 index가 실제 해쉬코드가 되기에 index저장
//                         entries[index].next = i;
//                     }
//                 }
//             }
//             // entry의 넥스트가 이미 존재하는 경우
//             else
//             {


//                 // for (int i = 0; i < entries.Length; i++)
//                 // {
//                 //     if (entries[i].hashCode == -1)
//                 //     {
//                 //         entries[i].hashCode = index;
//                 //         entries[index].next = i;
//                 //     }
//                 // }
//                 // int tem = entries[index].next;
//                 // entries[entries[index].next].hashCode = -1;
//             }

//             for (int i = index; i < bucket.Length + index; i++)
//             {
//                 int temp = i % bucket.Length;
//                 // next 처리 필요
//                 if (bucket[temp] == -1)
//                 {
//                     entries[temp] = new Entry(key, value, 0, temp);
//                     bucket[temp] = temp;
//                     Count++;
//                     return true;
//                 }
//             }
//             return false;
//         }
//         else
//         {
//             entries[index] = new Entry(key, value, -1, index);
//             bucket[index] = index;
//         }

//         Count++;
//         return true;
//     }
//     public bool Remove(K key)
//     {
//         if (ContainsKey(key))
//         {
//             int index = ConvertHashIdx(key);
//             // bucket[index]
//         }
//         return false;
//     }

//     public void Clear()
//     {
//         for (int i = 0; i < capacity; i++)
//         {
//             bucket[i] = default;
//             entries[i] = default;
//         }

//         Count = 0;
//     }


//     struct Entry
//     {
//         public int hashCode;
//         public int next;
//         public K key;
//         public T value;

//         public Entry(K _key, T _value, int _next, int _hashCode)
//         {
//             this.key = _key;
//             this.value = _value;
//             this.next = _next;
//             this.hashCode = _hashCode;
//         }
//     }
// }
