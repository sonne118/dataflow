

var bcBlock = new BroadcastBlock<int>(n => n);

var actionBlock1 = new ActionBlock<int>(n =>
{
	Thread.Sleep(100);
	Console.WriteLine($"Message Action block 1: {n} - Thread Id#{Thread.CurrentThread.ManagedThreadId}");
});
var actionBlock2 = new ActionBlock<int>(n =>
{
	Thread.Sleep(100);
	Console.WriteLine($"Message Action block 2: {n} - Thread Id#{Thread.CurrentThread.ManagedThreadId}");
});

bcBlock.LinkTo(actionBlock1);
bcBlock.LinkTo(actionBlock2);

for (int i = 0; i < 10; i++)
{
	bcBlock.Post(i);
}
