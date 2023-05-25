
async Task Main()
{
	IEnumerable<int> range = Enumerable.Range(0, 100);
	
	await Task.WhenAll(
		Producer(range),
		Consumer(n =>
			Console.WriteLine($"value {n}")));
}

// Simple Producer Consumer using TPL Dataflow BufferBlock
BufferBlock<int> buffer = new BufferBlock<int>(new DataflowBlockOptions { BoundedCapacity = 10});

async Task Producer(IEnumerable<int> values)
{
    foreach (var value in values)
        buffer.Post(value);
    buffer.Complete();
}
async Task Consumer(Action<int> process)
{
    while (await buffer.OutputAvailableAsync())
        process(await buffer.ReceiveAsync());

}
      
