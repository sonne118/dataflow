

// ActionBlock_Simple
var actionBlock = new ActionBlock<int>(n =>
			{
				Thread.Sleep(1000);
				Console.WriteLine($"Message : {n} - Thread Id#{Thread.CurrentThread.ManagedThreadId}");
			});

for (int i = 0; i < 10; i++)
{
	actionBlock.Post(i);
	Console.WriteLine($"Message {i} processed - queue count {actionBlock.InputCount}");
}

"Completed".Dump();

actionBlock.Complete();
actionBlock.Completion.Wait();

"Really Completed".Dump();
