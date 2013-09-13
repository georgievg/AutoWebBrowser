using Auto_Browser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Face_Browser_Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        void Form1_Load(object sender, EventArgs e)
        {
            this.autoBrowser1.BrowserActionExecuting += autoBrowser1_BrowserActionExecuting;

            NavigateAction navigateAction = new NavigateAction("http://google.bg");
            AwaitAction awaitAction = new AwaitAction(5000);
            NavigateAction navAction2 = new NavigateAction("http://facebook.com");

            this.autoBrowser1.EnqueueAction(navigateAction);
            this.autoBrowser1.EnqueueAction(awaitAction);
            this.autoBrowser1.EnqueueAction(navAction2);

            var actions = this.autoBrowser1.GetBrowserActions<NavigateAction>();

            this.autoBrowser1.EnqueueAction((BrowserAction)awaitAction.Clone());
            ClickElementsAction clickAction = new ClickElementsAction(GetElementByMode.Id, "websubmit");
            this.autoBrowser1.EnqueueAction(clickAction);
        }

        void autoBrowser1_BrowserActionExecuting(object sender, BrowserActionExecutingEventArgs e)
        {
            Console.WriteLine(e.ActionType.Name);
        }
    }
}
