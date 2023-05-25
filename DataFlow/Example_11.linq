
// JoinBlock_Simple
var jBlock = new JoinBlock<string, string>();

for (int i = 0; i < 10; i++)
{
	jBlock.Target1.Post($"Message Target 1: {i} - Thread Id#{Thread.CurrentThread.ManagedThreadId}");
}

for (int i = 0; i < 10; i++)
{
	jBlock.Target2.Post($"Message Target 2: {i} - Thread Id#{Thread.CurrentThread.ManagedThreadId}");
}

for (int i = 0; i < 10; i++)
{
	Tuple<string, string> res = jBlock.Receive();
	Console.WriteLine(res.Item1 + ";" + res.Item2);
}
