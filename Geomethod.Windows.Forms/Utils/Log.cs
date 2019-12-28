using System;
using System.Windows.Forms;
using Geomethod;

namespace Geomethod
{
	using Geomethod.Windows.Forms;
/*	public class Log
	{
		static LogSystem log = new LogSystem();
		static Log()
		{
			log.LogInformer2 = new MessageBoxLogInformer();
			log.LogInformer = new MessageFormLogInformer();
//			log.LogWriter2 = new FileLog();
		}
		public static LogSystem LogSystem { get { return log;} }
		public static void Info(string message) { log.Put(LogType.Info, message); }
		public static void Info(string header, string message) { log.Put(LogType.Info, header, message); }
		public static void Warning(string message) { log.Put(LogType.Warning, message); }
		public static void Warning(string header, string message) { log.Put(LogType.Warning, header, message); }
		public static void Error(string message) { log.Put(LogType.Error, message); }
		public static void Exception(Exception ex) { log.Put(ex); }
	}*/
}

namespace Geomethod.Windows.Forms
{
	public class MessageFormLogInformer : ILogHandler
	{
        LogSystem logSystem=null;

        #region ILogHandler Members

        public void Init(LogSystem logSystem)
        {
            this.logSystem=logSystem;
        }

        public void Put(LogRecord lr)
        {			
            if ((lr.logFlags&LogFlags.Hide)==0 && logSystem.IsDisplayed(lr.logType))
            {
                if (!MessageForm.IsIgnored(lr.logType, lr.header))
                {
                    MessageForm form = new MessageForm(lr);
                    form.ShowDialog();
                }
            }
        }

        public void Close()
        {
        }

        #endregion
    }

	public class MessageBoxLogInformer :ILogHandler
	{
        LogSystem logSystem;
		public void Show(LogRecord lr)
		{
			MessageBox.Show(lr.header + "  " + lr.message,lr.logType.ToString());
		}


        #region ILogHandler Members

        public void Init(LogSystem logSystem)
        {
            this.logSystem = logSystem;
        }

        public void Put(LogRecord lr)
        {
            if (logSystem.IsDisplayed(lr.logType))
            {
                Show(lr);
            }
        }

        public void Close()
        {
        }

        #endregion
    }
}
