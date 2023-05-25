var source = new BufferBlock<string>();

var actionBlock = new ActionBlock<string>(n =>
{
    Thread.Sleep(200);
    Console.WriteLine($"Message : {n} - Thread Id#{Thread.CurrentThread.ManagedThreadId}");
});

source.LinkTo(actionBlock, new DataflowLinkOptions()
{
    PropagateCompletion = true
});

for (int i = 0; i < 10; i++)
{
	source.Post($"Item #{i}");
}

actionBlock.Completion.ContinueWith(a => Console.WriteLine("actionBlock completed"));
source.Complete();
actionBlock.Completion.Wait();
