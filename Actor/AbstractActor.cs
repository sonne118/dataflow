using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Actor
{
    public abstract class AbstractActor<T>
    {
        private readonly BufferBlock<T> _mailBox;
        private readonly List<Task> _activeWorkers;

        protected abstract int ThreadCount { get; }

        public AbstractActor()
        {
            _mailBox = new BufferBlock<T>();
            _activeWorkers = new List<Task>();

            Task.Run(async () =>
            {
                while (true)
                {
                    while (_activeWorkers.Count < ThreadCount)
                    {
                        _activeWorkers.Add(Worker());
                    }

                    await Task.WhenAny(_activeWorkers);
                    _activeWorkers.RemoveAll(s => s.IsCompleted);
                }
            });
        }

        private async Task Worker()
        {
            var message = await _mailBox.ReceiveAsync();
            try
            {
                await HandleItem(message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        protected abstract Task HandleItem(T message);

        public Task SendMessage(T message) => _mailBox.SendAsync(message);
        public void ClearQueue() => _mailBox.TryReceiveAll(out var _);
    }
   
}
