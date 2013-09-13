using System;
using System.Threading;

namespace Auto_Browser
{
    public interface IAwaitableAction
    {
        int AwaitTime { get;}
    }

    public class AwaitAction : BrowserAction, IAwaitableAction
    {
        private int awaitTime;

        public AwaitAction(int awaitTime)
            : base()
        {
            this.AwaitTime = awaitTime;
        }

        public int AwaitTime
        {
            get { return this.awaitTime; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("The await time can not be negative value");
                }

                this.awaitTime = value;
            }
        }

        public override void Execute()
        {
            base.Execute();
            Thread.Sleep(this.AwaitTime);
        }

        public override object Clone()
        {
            return new AwaitAction(this.awaitTime);
        }
    }
}