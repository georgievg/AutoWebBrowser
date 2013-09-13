using System;

namespace Auto_Browser
{
    public class BrowserActionExecutedEventArgs : EventArgs
    {
        public Type ActionType { get; private set; }
        public BrowserAction Action { get; private set; }

        public BrowserActionExecutedEventArgs(Type actionType, BrowserAction action)
        {
            this.ActionType = actionType;
            this.Action = action;
        }
    }
}