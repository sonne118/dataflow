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