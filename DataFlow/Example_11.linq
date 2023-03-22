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
