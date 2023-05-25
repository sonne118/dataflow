
var actionBlock = new ActionBlock<int>(n =>
{
    Thread.Sleep(100);
    Console.WriteLine($"Message : {n} - Thread Id#{Thread.CurrentThread.ManagedThreadId}");
});

var tfBlock = new TransformBlock<int, int>(
    n =>
    {
        Thread.Sleep(500);
		return n * n;
	}, new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = 1 }); // Change DOP

tfBlock.LinkTo(actionBlock);

for (int i = 0; i < 10; i++)
{
	tfBlock.Post(i);
	Console.WriteLine($"Message {i} processed - queue count {actionBlock.InputCount}");
}

for (int i = 0; i < 10; i++)
{
	int result = tfBlock.Receive();
	Console.WriteLine($"Message {i} received - value {result}");
}
