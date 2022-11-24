using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BinarySerachTree<T> : IEnumerable<T>
{
    public int Count { private set; get; } = 0;
    BSTNode<T> root;
    public BSTNode<T> Root
    {
        private set
        {
            root.data = value.data ?? default;
            root.leftnode = value?.leftnode ?? default;
            root.rightnode = value?.rightnode ?? default;
        }
        get { return root; }
    }
    public IComparer<T> Comparer { private set; get; } = null; // 생성자에서 따로 넣을 수 있게

    public bool Contains(T value)
    {
        BSTNode<T> node = Find(value);

        int result = Comparer<T>.Default.Compare(node.data, value);
        if (result == 0)
            return true;

        return false;
    }
    public void FindTest(T value)
    {
        FindMySelf(root, value);
    }
    void FindMySelf(BSTNode<T> node, T value)
    {
        if (node != null)
        {
            int result = Comparer<T>.Default.Compare(node.data, value);
            if (result == 0)
                Debug.Log($"찾았당 {node.data} {value}");

            FindMySelf(node.leftnode, value);
            Debug.Log(node.data);
            FindMySelf(node.rightnode, value);
        }
    }

    public BSTNode<T> Find(T value)
    {
        if (Count <= 0)
            throw new Exception("Tree is Empty - Contains");

        BSTNode<T> node = root;

        while (node != null) // 재귀처리를 하는게 더 편하다.
        {
            int result = Comparer<T>.Default.Compare(node.data, value);
            if (result == 0)
                return node;
            // 그 다음 데이터와 비교
            else if (result > 0)
            {
                int leftresult = Comparer<T>.Default.Compare(node.leftnode.data, value);
                // 전위, 중위, 후위가 더 편하다.
                if (leftresult == 0)
                    return node;
                node = node.leftnode;
            }
            else
            {
                int rightnode = Comparer<T>.Default.Compare(node.rightnode.data, value);
                if (rightnode == 0)
                    return node;
                node = node.rightnode;
            }
        }
        return null;
    }

    public BSTNode<T> GetParent(T value)
    {
        if (Count < 0)
            throw new Exception("Tree is Empty - Contains");

        BSTNode<T> node = root;
        BSTNode<T> parent;

        while (node.data != null) // fake null 이슈!!!!! 부들부들 null이 왜생기는거야!!!!
        {
            int result = Comparer<T>.Default.Compare(node.data, value);
            if (result == 0)
            {
                parent = node;
                return parent;
            }
            // 그 다음 데이터와 비교
            else if (result > 0)
            {
                int leftresult = Comparer<T>.Default.Compare(node.leftnode.data, value);

                if (leftresult == 0)
                {
                    parent = node;
                    return node;
                }
                else
                {
                    parent = node;
                    node = node.leftnode;
                }
            }
            else
            {
                int rightnode = Comparer<T>.Default.Compare(node.rightnode.data, value);

                if (rightnode == 0)
                {
                    parent = node;
                    return parent;
                }
                else
                {
                    parent = node;
                    node = node.rightnode;
                }
            }
        }
        return null;
    }


    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<T> GetEnumerator()
    {
        BSTNode<T> node = root;
        if (node != null)
        {
            // 노드를 strucuture 구조로 사용


            // GetEnumerator(node.leftnode);
            // GetEnumerator(node.rightnode);
            yield return node.data;
        }
        // 계층을 구분해서 가장 왼쪽 아래 하단 계층 부터 위로 꼭대기 찍으면 우측 계층은 순차적으로
    }


    // public IEnumerator<T> GetOverlaps(T min, T max)
    // {

    //     // ...
    // }


    private T MinValue(BSTNode<T> node)
    {
        T minVal = node.data;

        while (null != node.leftnode)
        {
            minVal = node.leftnode.data;
            node = node.leftnode;
        }

        return minVal;
    }

    private T MaxValue(BSTNode<T> node)
    {
        T maxVal = node.data;

        while (null != node.rightnode)
        {
            maxVal = node.rightnode.data;
            node = node.rightnode;
        }

        return maxVal;
    }

    public void Insert(T value)
    {
        BSTNode<T> node = root;

        if (Count == 0)
        {
            root = new BSTNode<T>(value);
            Count++;
            return;
        }

        while (node != null)
        {
            // Debug.Log($"insert: {node.data} value: {value} ");

            int result = Comparer<T>.Default.Compare(node.data, value);

            if (result == 0)
            {
                throw new Exception("Duplicate value");
            }
            else if (result > 0)
            {
                if (node.leftnode == default)
                {
                    node.leftnode = new BSTNode<T>(value);
                    // Debug.Log($"nodeleft: {node.leftnode.data} value: {value} ");
                    Count++;
                    return;
                }
                node = node.leftnode;
            }
            else
            {
                if (node.rightnode == default)
                {
                    node.rightnode = new BSTNode<T>(value);
                    // Debug.Log($"rightnode: {node.rightnode.data} value: {value} ");

                    Count++;
                    return;
                }
                node = node.rightnode;
            }
        }
    }



    public bool Remove(T value)
    {
        if (Count < 0) // false
            throw new Exception("Tree is Empty - Remove");

        BSTNode<T> node = root;
        node = Find(value);
        Debug.Log(node.data);

        BSTNode<T> parentNode = GetParent(value);
        Debug.Log($"부모가 누구야: {parentNode.data} 난 누구야: {value}");
        // 들고 있는 놈을 어떻게 찾을까?

        while (node.data != null)
        {
            if (parentNode == default)
                parentNode = node;

            if (node == default)
                throw new Exception("The value is not exist...");

            // 자식이 없을 때
            if (node.leftnode == default && node.rightnode == default)
            {
                // 부모에게 자신이 좌츠인지 우측인지 확인하고 끊어준다.
                int result = Comparer<T>.Default.Compare(parentNode.data, node.data);
                if (result > 0) // 내가 부모보다 크다면
                    parentNode.rightnode = default;
                else
                    parentNode.leftnode = default;

                node.Release();
                Count--;
                return true;
            }
            // 자식이 두 개 있을 때
            else if (node.leftnode != default && node.rightnode != default)
            {
                int result = Comparer<T>.Default.Compare(parentNode.data, node.data);
                if (result > 0) // 내가 부모보다 크다면
                    parentNode.rightnode = node.rightnode;
                else
                    parentNode.leftnode = node.rightnode;
                node.Release();
                Count--;

                return true;
            }
            // 자식이 하나 있을 때
            else if (node.leftnode != default || node.rightnode != default)
            {
                int result = Comparer<T>.Default.Compare(parentNode.data, node.data);
                if (result > 0) // 내가 부모보다 크다면
                    parentNode.rightnode = node;
                else
                    parentNode.leftnode = node;

                node.Release();
                Count--;

                return true;
            }
        }


        return false;
    }

    public void Remove(BSTNode<T> node)
    {
        if (node.data != null)
            Remove(node.data);
    }

    public void Clear()
    {
        BSTNode<T> node = root;
        if (root != null)
        {

        }
    }
}
