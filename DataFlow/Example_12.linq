
// JoinBlock_SendAsync

var jBlock = new JoinBlock<string, string>(
			  new GroupingDataflowBlockOptions
			  {
				  Greedy = false,
				  BoundedCapacity = 2
			  });

for (int i = 0; i < 10; i++)
{
	Task<bool> task =
		jBlock.Target1.SendAsync(
			$"Message Target 1: {i} - Thread Id#{Thread.CurrentThread.ManagedThreadId}");
	int iCopy = i;
	task.ContinueWith(t =>
	{
		if (t.Result)
		{
			Console.WriteLine(
				$"Message Target 1: {i} ACCEPTED - Thread Id#{Thread.CurrentThread.ManagedThreadId}");
		}
		else
		{
			Console.WriteLine(
				$"Message Target 1: {i} REFUSED - Thread Id#{Thread.CurrentThread.ManagedThreadId}");
		}
	});
}

for (int i = 0; i < 10; i++)
{
	Task<bool> task =
		jBlock.Target2.SendAsync(
			$"Message Target 2: {i} - Thread Id#{Thread.CurrentThread.ManagedThreadId}");
	int iCopy = i;
	task.ContinueWith(t =>
	{
		if (t.Result)
		{
			Console.WriteLine(
				$"Message Target 2: {i} ACCEPTED - Thread Id#{Thread.CurrentThread.ManagedThreadId}");
		}
		else
		{
			Console.WriteLine(
				$"Message Target 2: {i} REFUSED - Thread Id#{Thread.CurrentThread.ManagedThreadId}");
		}
	});
}


for (int i = 0; i < 10; i++)
{
	var res = jBlock.Receive();
	Console.WriteLine(res.Item1 + ";" + res.Item2);
}
