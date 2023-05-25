
// BatchBlock_Receive
var batchBlock = new BatchBlock<int>(2);

for (int i = 0; i < 10; i++)
{
	batchBlock.Post(i);
}

batchBlock.Complete();

for (int i = 0; i < 5; i++)
{
	int[] result = batchBlock.Receive();

	// PROPER if(batchBlock.TryReceive(null,out result)){

	foreach (var r in result)
	{
		Console.Write(r + " ");
	}

	Console.Write("\n");
}
