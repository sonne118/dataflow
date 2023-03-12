using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Actor
{
    public class ParserActor : AbstractActor<string>
    {
        private readonly HabrParser _parser;
        private readonly EventBus _eventBus;

        protected override int ThreadCount => 10;

        public ParserActor(EventBus eventBus)
        {
            _parser = new HabrParser();
            _eventBus = eventBus;
        }

        protected override async Task HandleItem(string message)
        {
            try
            {
                var topics = await _parser.ParseTopics(message);

                foreach (var item in topics)
                {
                    _ = _eventBus.Publish(new OnTopicParsed(item));
                }
            }
            catch (Exception ex)
            {
                await SendMessage(message);
            }
        }

    }


    public class OnTopicParsed : IEvent
    {
        public OnTopicParsed(Topic topic)
        {
            Topic = topic;
        }

        public Topic Topic { get; set; }
    }

}
