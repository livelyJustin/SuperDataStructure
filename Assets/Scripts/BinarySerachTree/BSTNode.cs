using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSTNode<T>
{
    public BSTNode<T> leftnode = default;
    public BSTNode<T> rightnode = default;
    public T data = default;

    public BSTNode(T _data)
    {
        this.data = _data;
    }

    public void Release()
    {
        this.data = default;
        leftnode = null;
        rightnode = null;
    }
}
