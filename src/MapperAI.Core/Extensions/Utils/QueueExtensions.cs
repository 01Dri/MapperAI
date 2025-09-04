namespace MapperAI.Core.Extensions.Utils;

public static  class QueueExtensions
{
    public static T? DequeueOrDefault<T>(this Queue<T> queue) where T : class
    {
        return queue.Count > 0 ? queue.Dequeue() : null;
    }
}