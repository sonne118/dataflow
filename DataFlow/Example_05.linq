
// ActionBlock_Linking

var actionBlock1 = new ActionBlock<int>(n =>
		 {
			 Thread.Sleep(100);
			 Console.WriteLine($"Message (1): {n} - Thread Id#{Thread.CurrentThread.ManagedThreadId}");
		 });

var actionBlock2 = new ActionBlock<int>(n =>
{
	Thread.Sleep(100);
	Console.WriteLine($"Message (2) : {n} - Thread Id#{Thread.CurrentThread.ManagedThreadId}");
});

var bcBlock = new TransformBlock<int, int>(n => n);

bcBlock.LinkTo(actionBlock1, n => n % 2 == 0);
bcBlock.LinkTo(actionBlock2);

for (int i = 0; i < 10; i++)
{
	bcBlock.Post(i);
}
