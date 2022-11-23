namespace Lulu
{
    public class Stack_QueueTest : StudyBase
    {
        protected override void OnLog()
        {
            var stack = new Stack<int>();

            stack.Push(10);
            stack.LogValues();

            stack.Push(9);
            stack.LogValues();

            stack.Push(8);
            stack.LogValues();

            stack.Pop();
            stack.LogValues();

            stack.Push(7);
            stack.LogValues();


            stack.Pop();
            stack.LogValues();

            stack.Pop();
            stack.LogValues();


            var queue = new Queue<int>();
            queue.Enqueue(10);
            queue.LogValues();

            queue.Enqueue(9);
            queue.LogValues();

            queue.Enqueue(8);
            queue.LogValues();

            queue.Dequeue();
            queue.LogValues();

            queue.Enqueue(7);
            queue.LogValues();


            queue.Dequeue();
            queue.LogValues();

            queue.Dequeue();
            queue.LogValues();
        }
    }
}