<Query Kind="Statements">
  <NuGetReference>Microsoft.Tpl.Dataflow</NuGetReference>
  <NuGetReference>System.Collections.Immutable</NuGetReference>
  <NuGetReference>System.Reactive</NuGetReference>
  <Namespace>System</Namespace>
  <Namespace>System.Collections.Concurrent</Namespace>
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.Collections.Immutable</Namespace>
  <Namespace>System.Linq</Namespace>
  <Namespace>System.Reactive</Namespace>
  <Namespace>System.Reactive.Concurrency</Namespace>
  <Namespace>System.Reactive.Disposables</Namespace>
  <Namespace>System.Reactive.Joins</Namespace>
  <Namespace>System.Reactive.Linq</Namespace>
  <Namespace>System.Reactive.PlatformServices</Namespace>
  <Namespace>System.Reactive.Subjects</Namespace>
  <Namespace>System.Reactive.Threading.Tasks</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Threading.Tasks.Dataflow</Namespace>
</Query>

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