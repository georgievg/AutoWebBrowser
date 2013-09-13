using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Auto_Browser
{
    public abstract class BrowserAction : IBrowserAction, ICloneable
    {
        private AutoBrowser browser;

        public BrowserAction(AutoBrowser browser)
        {
            this.Browser = browser;
        }

        public BrowserAction()
            : this(null)
        {
        }

        public AutoBrowser Browser
        {
            get { return this.browser; }
            internal set { this.browser = value; }
        }

        public virtual void Execute()
        {
            if (this.Browser.InvokeRequired && !(this is IAwaitableAction))
            {
                this.Browser.Invoke(new Action(() => this.Execute()));
                return;
            }
        }

        public abstract object Clone();
    }
}