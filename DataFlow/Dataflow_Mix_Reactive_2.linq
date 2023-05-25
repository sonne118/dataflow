
IObservable<int> originalInts = Observable.Range(1, 20);

IPropagatorBlock<int, int[]> batch = new BatchBlock<int>(2);
IObservable<int[]> batched = batch.AsObservable();
originalInts.Subscribe(batch.AsObserver());

IObservable<int> added = batched.Timeout(TimeSpan.FromMilliseconds(250)).Select(a => a.Sum());

IPropagatorBlock<int, string> toString = new TransformBlock<int, string>(i => i.ToString());
added.Subscribe(toString.AsObserver());

JoinBlock<string, int> join = new JoinBlock<string, int>();
toString.LinkTo(join.Target1);

IObserver<int> joinIn2 = join.Target2.AsObserver();
originalInts.Subscribe(joinIn2);

IObservable<Tuple<string, int>> joined = join.AsObservable();

joined.Subscribe(t => Debug.WriteLine("{0};{1}", t.Item1, t.Item2));
