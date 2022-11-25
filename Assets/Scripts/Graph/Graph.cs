using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Justin
{
    public sealed class Graph<T> : IEnumerable<GraphNode<T>>, ICloneable
    {
        readonly Dictionary<T, GraphNode<T>> nodes = new Dictionary<T, GraphNode<T>>();
        public int Count => nodes.Count;


        public GraphNode<T> Add(T vertex)
        {
            if (Contains(vertex))
                throw new Exception("Duplictated vaulue...");

            nodes.Add(vertex, new GraphNode<T>(vertex));
            return nodes[vertex];
        }

        public bool Contains(T vertex)
        {
            if (nodes.ContainsKey(vertex))
                return true;

            return false;
        }

        public bool TryGetValue(T vertex, out GraphNode<T> result)
        {
            result = Find(vertex);
            if (result != null)
                return true;
            return false;
        }

        public GraphNode<T> Find(T vertex)
        {
            if (Contains(vertex))
                return nodes[vertex];

            return null;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<GraphNode<T>> GetEnumerator()
        {
            foreach (var item in nodes)
            {
                yield return item.Value;
            }

            // vist -> 방문한 버텍스는 가지마라
            // pass -> 방문했던 경로만 가지마라 
        }

        public void SetEdge(T from, T to, int weight, bool isBoth)
        {
            GraphNode<T> from_Node = Find(from);
            GraphNode<T> to_Node = Find(to);
            // 두 개가 같을 때 처리 필요

            if (from_Node == null || to_Node == null)
                throw new Exception($"Value is not valild from: {from_Node} to_Node: {to_Node}");

            from_Node.AddEdgeNode(to, to_Node, weight);
            if (isBoth)
                to_Node.AddEdgeNode(from, from_Node, weight);

        }
        public void SetEdge(T a, T b, int weigth_ab, int weigth_ba)
        {
            GraphNode<T> from_Node = Find(a);
            GraphNode<T> to_Node = Find(b);

            if (from_Node == null || to_Node == null)
                throw new Exception($"Value is not valild from: {from_Node} to_Node: {to_Node}");

            from_Node.AddEdgeNode(a, to_Node, weigth_ab);
            to_Node.AddEdgeNode(b, from_Node, weigth_ba);
        }

        public GraphPath<T> CreatePath(T start, T end)
        {
            GraphNode<T> st_Node = Find(start);
            GraphNode<T> ed_Node = Find(end);
            if (st_Node != null && ed_Node != null)
            {
                GraphPath<T> pathClass = new GraphPath<T>(Find(start), Find(end));
                return pathClass;
            }

            return null;

        }

        bool isOut = false; // 추후 리셋 해야함
        private void Search(GraphNode<T> st_node, GraphNode<T> ed_node, ref List<GraphPath<T>> paths)
        {
            if (st_node == ed_node)
            {
                Debug.Log($"마지막 St: {st_node.Vertex} ed: {ed_node.Vertex}");
                paths.Add(CreatePath(st_node.Vertex, ed_node.Vertex));
                if (paths == null)
                    Debug.Log("you?: 탐색와뇨");

                isOut = true;
                return;
            }
            else
            {
                foreach (var item in st_node.Edges.Values)
                {
                    if (item.Node.IsVisited == 0 && isOut == false) // isvisit을 사용해야함 
                    {
                        item.Node.IsVisited = 1;
                        Debug.Log($"중간 St: {st_node.Vertex} ed: {ed_node.Vertex}");
                        paths.Add(CreatePath(st_node.Vertex, ed_node.Vertex));
                        if (paths == null)
                            Debug.Log("you?: 탐색중");

                        Search(item.Node, ed_node, ref paths);
                        // Debug.Log($"추가되는 놈 st: {st_node.Vertex} ed: {ed_node.Vertex}");
                    }
                }
            }
        }

        public List<GraphPath<T>> SearchAll(T start, T end, SearchPolicy policy) // 서치 이슈는 깊이 우선 탐색으로
        {
            var path = CreatePath(start, end);
            if (path == null)
                Debug.Log("you?: searchall");
            var paths = new List<GraphPath<T>>();

            if (path == null)
                return null;
            // throw new Exception("Something Wrong in SearchALL");

            Search(path.Start, path.End, ref paths);
            return paths;
        }

        // 노드 제거, 간선처리
        // public bool Remove(T vertex)
        // { 

        // }

        public void Clear() => nodes.Clear();

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public enum SearchPolicy
        {
            Visit = 0,
            Pass,
        }
    }
}