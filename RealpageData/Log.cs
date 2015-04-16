    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using log4net;
    using log4net.Appender;
    using log4net.Repository.Hierarchy;

    public static class Log
    {
        // Create an instance of the log4net logger
        static readonly ILog logger4net = LogManager.GetLogger("---");


        public static void Debug(object message)
        {
            var m = new StackFrame(1).GetMethod();
            string msg = "(" + m.DeclaringType.Name + "." +m.Name + ") " + message; 
            logger4net.Debug(msg);
        }

        public static void DebugFmt(object message, params Object[] args)
        {
            var m = new StackFrame(1).GetMethod();
            string msg = "(" + m.DeclaringType.Name + "." +m.Name + ") " + message; 
            logger4net.Debug(String.Format(msg, args));
        }

        public static void Info(object message)
        {
            var m = new StackFrame(1).GetMethod();
            string msg = "(" + m.DeclaringType.Name + "." +m.Name + ") " + message; 
            logger4net.Info(msg);
        }

        public static void InfoFmt(object message, params Object[] args)
        {
            var m = new StackFrame(1).GetMethod();
            string msg = "(" + m.DeclaringType.Name + "." +m.Name + ") " + message; 
            logger4net.Info(String.Format(msg, args));
        }

        public static void Warn(object message)
        {
            var m = new StackFrame(1).GetMethod();
            string msg = "(" + m.DeclaringType.Name + "." +m.Name + ") " + message; 
            logger4net.Warn(msg);
        }

        public static void WarnFmt(object message, params Object[] args)
        {
            var m = new StackFrame(1).GetMethod();
            string msg = "(" + m.DeclaringType.Name + "." +m.Name + ") " + message; 
            logger4net.Warn(String.Format(msg, args));
        }

        public static void Error(object message)
        {
            var m = new StackFrame(1).GetMethod();
            string msg = "(" + m.DeclaringType.Name + "." +m.Name + ") " + message; 
            logger4net.Error(msg);
        }

        public static void ErrorFmt(object message, params Object[] args)
        {
            var m = new StackFrame(1).GetMethod();
            string msg = "(" + m.DeclaringType.Name + "." + m.Name + ") " + message; 
            logger4net.Error(String.Format(msg, args));
        }

        public static void Fatal(object message)
        {
            var m = new StackFrame(1).GetMethod();
            string msg = "(" + m.DeclaringType.Name + "." +m.Name + ") " + message; 
            logger4net.Fatal(msg);
        }

        public static void FatalFmt(object message, params Object[] args)
        {
            var m = new StackFrame(1).GetMethod();
            string msg = "(" + m.DeclaringType.Name + "." +m.Name + ") " + message; 
            logger4net.Fatal(String.Format(msg, args));
        }


        public static string GetLogFilePath()
        {
            var rootAppender = ((Hierarchy) LogManager.GetRepository()).Root.Appenders.OfType<FileAppender>().FirstOrDefault();
            return rootAppender != null ? rootAppender.File : string.Empty;
        }
    }

