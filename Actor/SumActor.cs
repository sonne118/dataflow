using System.Threading.Tasks;

namespace Actor
{
    public class SumActor : AbstractActor<int>
    {
        protected override int ThreadCount => 1;


        private int State { get; set; }

        protected override Task HandleItem(int message)
        {
            State += message;

            return Task.CompletedTask;
        }

        public int GetState() => State;
    }

}
