using System.Collections.Generic;
using System.Windows.Forms;

namespace Auto_Browser
{
    public class ClickElementsAction : BrowserAction
    {
        private GetElementByMode getMode;
        private string identifier;

        public ClickElementsAction(GetElementByMode getMode, string identifier)
        {
            this.GetMode = getMode;
            this.Identifier = identifier;
        }

        public GetElementByMode GetMode
        {
            get { return this.getMode; }
            private set { this.getMode = value; }
        }

        public string Identifier
        {
            get { return this.identifier; }
            private set { this.identifier = value; }
        }

        public override void Execute()
        {
            base.Execute();
            List<HtmlElement> elements = this.GetHtmlElements();
            foreach (HtmlElement element in elements)
            {
                element.InvokeMember("click");
            }
        }

        private List<HtmlElement> GetHtmlElements()
        {
            List<HtmlElement> results = new List<HtmlElement>();
            if (this.getMode == GetElementByMode.Id)
            {
                results.Add(this.Browser.Document.GetElementById(this.identifier));
            }
            else if (this.getMode == GetElementByMode.Class)
            {
                this.GetElementByClassName(this.identifier, results);
            }
            else if (this.getMode == GetElementByMode.TagName)
            {
                HtmlElementCollection tags = this.Browser.Document.GetElementsByTagName(this.identifier);
                foreach (HtmlElement tag in tags)
                {
                    results.Add(tag);
                }
            }

            return results;
        }

        private void GetElementByClassName(string className, List<HtmlElement> fill)
        {
            HtmlElementCollection all = this.Browser.Document.All;
            foreach (HtmlElement element in all)
            {
                if (element.GetAttribute("classname") == className ||
                    element.GetAttribute("classname").Contains(className))
                {
                    fill.Add(element);
                }
            }
        }

        public override object Clone()
        {
            return new ClickElementsAction(this.getMode, this.identifier);
        }
    }
}