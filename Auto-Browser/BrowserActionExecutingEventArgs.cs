using System;

namespace Auto_Browser
{
    public class BrowserActionExecutingEventArgs : BrowserActionExecutedEventArgs
    {
        public bool Cancel { get; set; }
          
        public BrowserActionExecutingEventArgs(Type actionType, BrowserAction action)
            : base(actionType, action)
        {
        }
    }
}