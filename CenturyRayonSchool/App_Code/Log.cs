using log4net;

namespace CenturyRayonSchool.Model
{
    public class Log
    {
        private static readonly Log _instance = new Log();
        protected ILog errorlogger;
        protected ILog eventlogger;

        private Log()
        {
            errorlogger = LogManager.GetLogger("ErrorsLog");
            eventlogger = LogManager.GetLogger("eventlogger");
        }

        public static void Info(string message)
        {
            _instance.errorlogger.Info(message);
        }


        public static void Error(string message, System.Exception exception)
        {
            _instance.errorlogger.Error(message, exception);
        }

        public static void event_Info(string action, string message)
        {
            _instance.eventlogger.Info(message);
        }


    }
}