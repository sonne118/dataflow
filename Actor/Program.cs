using System;
using System.Threading;
using System.Threading.Tasks;

namespace Actor
{
    class Program
    {
        private static ParserActor actor;
        private static SumActor sumActor;
        private static EventBus eventbus;

        static async Task Main(string[] args)
        {
            eventbus = new EventBus();
            actor = new ParserActor(eventbus);
            sumActor = new SumActor();

            eventbus.Subscribe<OnTopicParsed>(HandleOnTopicParsed);


            await sumActor.SendMessage(1);
            await sumActor.SendMessage(1);


            await Task.Delay(1);
            var state = sumActor.GetState();

            await DoParse();

            Console.ReadLine();
        }

        private static Task HandleOnTopicParsed(OnTopicParsed arg)
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Parsed: {arg.Topic.Title} - {arg.Topic.Url}");

            return Task.CompletedTask;
        }

        private static async Task DoParse()
        {
            var parser = new HabrParser();

            await foreach (var url in parser.GetPages())
            {
                await actor.SendMessage(url);
            }
        }

    }


}


