using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Justin
{
    public class GraphPath<T> : IEnumerable<T>
    {
        public GraphNode<T> Start { private set; get; } = null;
        public GraphNode<T> End { private set; get; } = null;

        public readonly List<T> Vertexs = new List<T>();
        public int Count => Vertexs.Count;

        public GraphPath(GraphNode<T> start, GraphNode<T> end)
        {
            Start = start;
            End = end;
        }

        public void SetStart(GraphNode<T> start)
        {
            Start = start;
        }

        public bool IsNoWay { get { return !IsPassed(Start.Vertex, End.Vertex); } }


        public int GetTotalWeight() // 찾으면서 더해줘야함
        {
            int s_Weights = 0;
            int l_Weights = 0;
            foreach (var item in Start.Edges.Values)
                s_Weights += item.Weight;

            foreach (var item in End.Edges.Values)
                l_Weights += item.Weight;

            return s_Weights + l_Weights;
        }

        public bool IsVisited(GraphNode<T> node)
        {
            // vertex의 방문 여부 확인	
            if (node.IsVisited == 0)
                return true;

            return false;
        }

        // public bool IsVisited(GraphNode<T> node) 
        // {
        //     // vertex의 방문 여부 확인	
        //     if (node.IsVisited == 0)
        //         return true;

        //     return false;
        // }

        // public bool IsPassed(T vertex)
        // {
        //     // 마지막 vertex에서 인자로 넘어온 vertex로 향한 edge의 통과 여부 확인
        // }

        Graph<T> graph = new Graph<T>();
        public bool IsPassed(T from, T to) //from 부터 검사하고 ++
        {
            List<GraphPath<T>> list = graph.SearchAll(from, to, Graph<T>.SearchPolicy.Visit);

            Debug.Log(list);

            // List의 마지막의 성공과 끝이 제주도-제주도 이면 성공
            if (list[list.Count - 1].Start == list[list.Count - 1].End)
            {
                return true;
            }
            return false;
            // 카운트가 4인 리스트
            // 1. 서울 - 제주도
            // 2. 경기 - 제주도
            // 3. 전라도 - 제주도
            // 4. 경상도 - 제주도
            // for (int i = 0; i < list.Count - 2; i++)
            // {
            //     // 마지막 리스트는 겹치지 않도록
            //     if (list[i].Start == list[Count - 2].End)
            //         return true;
            // }

        }

        // public GraphPath<T> Clone()
        // {
        //     GraphPath<T> newGraph = new GraphPath<T>(this.Start, this.End);
        // }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<T> GetEnumerator()
        {
            yield return default(T);
        }
    }
}