using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueTest : StudyBase
{
    protected override void OnLog()
    {
        Queue<string> queue = new Queue<string>();
        queue.Enqueue("1stJob");
        queue.Enqueue("2ndJob");
        // queue.Enqueue(default);
        queue.LogValues();

        queue.Peek();
        // 1stJob
        Log(queue.Peek());

        queue.Enqueue("3rdJob");
        var str = queue.Dequeue();
        // 1stJob;
        Log(str);

        queue.Enqueue("4thJob");
        // 2ndJob, 3rdJob, 4thJob
        queue.LogValues();

        Debug.Log("*******************************");
        queue.Enqueue("5th Job");
        queue.Enqueue("6th Job");

        queue.Dequeue();
        queue.LogValues();

        queue.Dequeue();
        queue.LogValues();

        queue.Dequeue();
        queue.LogValues();

        queue.Dequeue();
        queue.LogValues();

        queue.Dequeue();
        queue.LogValues();

        Debug.Log("*******************************");
        queue.Enqueue("다시 1번 차지");
        queue.Enqueue("다시 2번 차지");
        queue.LogValues();
    }
}
