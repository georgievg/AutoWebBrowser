using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auto_Browser
{
    public class AutoBrowser : WebBrowser
    {
        #region Fields
        private Queue<BrowserAction> actionsQueue;
        private Task actionsExecutorTask;
        private bool browserIsComplete;

        #endregion
        
        #region Properties

        public int TaskExecutionDelay { get; set; }

        public bool BrowserIsComplete
        {
            get { return this.browserIsComplete; }
        }

        #endregion

        #region Constructors
        
        public AutoBrowser()
        {
            this.Navigate("http://google.bg");
            this.Initialize();
        }

        #endregion
        
        #region Initialization
        protected virtual void Initialize()
        {
            this.InitializeFields();
            this.InitializeActionsExecution();
        }
        
        protected virtual void InitializeFields()
        {
            this.TaskExecutionDelay = 100;
            this.actionsQueue = new Queue<BrowserAction>();
        }
        #endregion

        #region ActionsExecution
        private void InitializeActionsExecution()
        {
            this.actionsExecutorTask = Task.Factory.StartNew(() => this.ExecuteActions(), TaskCreationOptions.LongRunning);
        }

        private void ExecuteActions()
        {
            while (!this.Disposing)
            {
                if (this.actionsQueue.Count > 0 && this.BrowserIsComplete)
                {
                    BrowserAction action = this.actionsQueue.Dequeue();
                    Type actionType = action.GetType();
                    if (this.OnActionExecuting(actionType, action))
                    {
                        action.Execute();
                        this.OnActionExecuted(actionType, action);
                    }
                }
                else
                {
                    Thread.Sleep(this.TaskExecutionDelay);
                }
            }
        }
  
        public delegate void BrowserActionExecutingEventHandler(object sender, BrowserActionExecutingEventArgs e);
        public event BrowserActionExecutingEventHandler BrowserActionExecuting;

        protected virtual bool OnActionExecuting(Type actionType, BrowserAction action)
        {
            BrowserActionExecutingEventArgs args = new BrowserActionExecutingEventArgs(actionType, action);
            if (this.BrowserActionExecuting != null)
            {
                this.BrowserActionExecuting(this, args);
            }

            return !args.Cancel;
        }

        public delegate void BrowserActionExecutedEventHandler(object sender, BrowserActionExecutedEventArgs e);
        public event BrowserActionExecutedEventHandler BrowserActionExecuted;

        protected virtual void OnActionExecuted(Type actionType, BrowserAction action)
        {
            BrowserActionExecutedEventArgs args = new BrowserActionExecutedEventArgs(actionType, action);
            if (this.BrowserActionExecuted != null)
            {
                this.BrowserActionExecuted(this, args);
            }
        }

        #endregion

        #region EnqueueActions
        public void EnqueueAction(BrowserAction action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("The action can not be null");
            }

            if (action.Browser != null)
            {
                if (action.Browser != this)
                {
                    throw new ArgumentException("Action with different browser can not be added");
                }
            }
            else
            {
                action.Browser = this;
            }

            this.actionsQueue.Enqueue(action);
        }
        #endregion

        #region ActionsAccess
        public IEnumerable<BrowserAction> GetBrowserActions<T>() where T : BrowserAction
        {
            return this.actionsQueue.Where(x => x is T);
        }

        public BrowserAction GetBrowserAction<T>() where T : BrowserAction
        {
            return (T)this.actionsQueue.FirstOrDefault(x => x is T);
        }

        #endregion

        #region DocumentCompleted
        protected override void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
        {
            if (this.ReadyState == WebBrowserReadyState.Complete)
            {
                this.browserIsComplete = true;
                base.OnDocumentCompleted(e);
            }
            else
            {
                this.browserIsComplete = false;
            }
        }
        #endregion
    }
}