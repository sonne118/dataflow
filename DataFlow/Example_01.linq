
// BufferBlock_Simple

var bufferBlock = new BufferBlock<int>();

for (int i = 0; i < 10; i++)
{
	bufferBlock.Post(i);
}

for (int i = 0; i < 10; i++)
{
	int result = bufferBlock.Receive();
	Console.WriteLine(result);
}
