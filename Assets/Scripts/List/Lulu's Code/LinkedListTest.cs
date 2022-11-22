namespace Lulu
{
    public class LinkedListTest : StudyBase
    {
        protected override void OnLog()
        {
            var list = new LinkedList<int>();
            list.AddFirst(7);
            list.LogValues();

            list.AddFirst(6);
            list.LogValues();

            list.AddLast(8);
            list.LogValues();

            list.AddLast(10);
            list.LogValues();


            list.AddAfter(list.First, 10);
            list.LogValues();

            list.AddBefore(list.Last, 9);
            list.LogValues();


            list.Remove(10);
            list.LogValues();

            list.RemoveFirst();
            list.LogValues();

            list.RemoveLast();
            list.LogValues();
        }
    }
}