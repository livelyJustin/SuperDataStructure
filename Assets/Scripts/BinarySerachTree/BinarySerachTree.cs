using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BinarySerachTree<T> : IEnumerable<T>
{
    public int Count { private set; get; } = 0;
    public BSTNode<T> Root { private set; get; } = null;
    public IComparer<T> Comparer { private set; get; } = null; // 생성자에서 따로 넣을 수 있게

    public bool Contains(T value)
    {
        BSTNode<T> node = Find(value);

        if (node != null)
            return true;

        return false;
    }

    void FindAndChangeNode(BSTNode<T> from_node, T value, ref BSTNode<T> to_node)
    {
        if (from_node != null)
        {
            int result = Comparer<T>.Default.Compare(from_node.data, value);
            if (result == 0)
                to_node = from_node;

            FindAndChangeNode(from_node.leftnode, value, ref to_node);
            FindAndChangeNode(from_node.rightnode, value, ref to_node);
        }
    }

    public BSTNode<T> Find(T value)
    {
        if (Count <= 0)
            throw new Exception("Tree is Empty - Contains");

        BSTNode<T> node = new BSTNode<T>(default);
        FindAndChangeNode(Root, value, ref node);

        if (null != node)
            return node;

        return null;
    }

    public BSTNode<T> GetParent(T value)
    {
        if (Count < 0)
            throw new Exception("Tree is Empty - Contains");

        BSTNode<T> node = Root;
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
        BSTNode<T> node = Root;
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

    public void Add(BSTNode<T> node, T value)
    {
        if (Count == 0)
        {
            Root = new BSTNode<T>(value);
            Count++;
            return;
        }

        if (node != null)
        {
            int result = Comparer<T>.Default.Compare(node.data, value);
            if (result == 0) throw new Exception("Duplicate value");

            else if (result > 0)
            {
                if (node.leftnode == null)
                {
                    node.leftnode = new BSTNode<T>(value);
                    Count++; return;
                }
                node = node.leftnode;
            }
            else if (result < 0)
            {
                if (node.rightnode == null)
                {
                    node.rightnode = new BSTNode<T>(value);
                    Count++; return;
                }
                node = node.rightnode;
            }
            Add(node, value);
        }
    }
    public void Insert(T value)
    {
        Add(Root, value);
    }

    public bool Remove(T value)
    {
        if (Count < 0) // false
            throw new Exception("Tree is Empty - Remove");

        BSTNode<T> node = Root;
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
        BSTNode<T> node = Root;
        if (Root != null)
        {

        }
    }
}
