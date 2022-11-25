using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class GraphNode<T> : IEnumerable<KeyValuePair<T, int>>
{
    public Dictionary<T, Edge> Edges = new Dictionary<T, Edge>();
    public T Vertex { get; set; }
    public int IsVisited = 0;
    // private Edge myEdge;

    public GraphNode(T _vertex)
    {
        Vertex = _vertex;
    }

    public struct Edge
    {
        public T Vertex;
        public GraphNode<T> Node; // 다음 노드 담아야할까? 
        public int Weight; // 어디서 부터 어디까지 이어질때 가중치
    }


    // public GraphNode(T _vertex, int _weight = default)
    // {
    //     myEdge.Vertex = _vertex;
    //     myEdge.Node = this;
    //     myEdge.Weight = _weight;
    // }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<KeyValuePair<T, int>> GetEnumerator()
    {
        foreach (var item in Edges)
            yield return new KeyValuePair<T, int>(item.Key, item.Value.Weight);
    }


    public void AddEdgeNode(T value, GraphNode<T> _node = null, int _weight = default)
    {
        if (!Edges.ContainsKey(value))
            Edges.Add(value, SetNode(value, _node, _weight));
    }

    Edge SetNode(T _vertex, GraphNode<T> _node = null, int _weight = default)
    {
        Edge newEdge = new Edge();
        newEdge.Vertex = _vertex;
        newEdge.Node = _node;
        newEdge.Weight = _weight;
        return newEdge;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in Edges)
        {
            sb.Append("Key: ").Append(item.Key).Append("  ").Append("Weight: ").Append(item.Value.Weight).Append("  ").Append('\n');
        }

        return sb.ToString();
    }



    public bool TryGetValue(T vertex, out Edge edge) // trygetvalue는 항상 bool 필요
    {
        if (Edges.ContainsKey(vertex))
        {
            edge = Edges[vertex];
            return true;
        }
        edge = default;
        return false;
    }

}
