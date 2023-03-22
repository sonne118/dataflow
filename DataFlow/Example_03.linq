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