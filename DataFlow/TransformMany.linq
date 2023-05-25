
var printAction = new ActionBlock<string>(n =>
{
	Thread.Sleep(100);
	Console.WriteLine($"Message : {n} - Thread Id#{Thread.CurrentThread.ManagedThreadId}");
});

IEnumerable<string> IsEven(int n)
{
	for (int i = 0; i < n; i++)
		if (i % 2 == 0)
			yield return $"{n} : {i}";
}

var tfManyBlock = new TransformManyBlock<int, string>(n => IsEven(n));
tfManyBlock.LinkTo(printAction);

for (int i = 0; i < 10; i++)
{
	tfManyBlock.Post(i);
	Console.WriteLine($"Message {i} processed - queue count {printAction.InputCount}");
}
