using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public partial class UnhandledExceptionEventArgs : System.EventArgs
    {
        public UnhandledExceptionEventArgs(object exception, bool isTerminating) { }
        public object ExceptionObject { get { throw null; } }
        public bool IsTerminating { get { throw null; } }
    }

    public delegate void UnhandledExceptionEventHandler(object sender, System.UnhandledExceptionEventArgs e);
}
