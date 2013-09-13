using System;

namespace Auto_Browser
{
    public class NavigateAction : BrowserAction
    {
        string navigationUrl;

        public NavigateAction(string navigationUrl)
            : base()
        {
            this.navigationUrl = navigationUrl;
        }

        public string NavigationUrl
        {
            get { return this.navigationUrl; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Url can not be emoty");
                }

                this.navigationUrl = value;
            }
        }

        public override void Execute()
        {
            base.Execute();
            this.Browser.Navigate(this.navigationUrl);
        }

        public override object Clone()
        {
            return new NavigateAction(this.navigationUrl);
        }
    }
}