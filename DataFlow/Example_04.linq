
// ActionBlock_Parallel
var actionBlock = new ActionBlock<int>(n =>
			{
				Thread.Sleep(10);
				Console.WriteLine($"Message : {n} - Thread Id#{Thread.CurrentThread.ManagedThreadId}");
			}, new ExecutionDataflowBlockOptions()
			{
				MaxDegreeOfParallelism = 8,
				 BoundedCapacity = 1,
				  CancellationToken = CancellationToken.None,
				     NameFormat = "", 
					 
			});

for (int i = 0; i < 10; i++)
{
	actionBlock.Post(i);
	Console.WriteLine($"Message {i} processed - queue count {actionBlock.InputCount}");
}

actionBlock.Complete();
actionBlock.Completion.Wait();
