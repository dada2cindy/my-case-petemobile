using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ExceptionObject
{
    public string NativeMessage { get; set; }

    public string StackTrace { get; set; }

    private ExceptionObject()
    { 
    }

    public ExceptionObject(Exception e)
    {
        NativeMessage = e.Message;
        StackTrace = e.StackTrace;
    }
}