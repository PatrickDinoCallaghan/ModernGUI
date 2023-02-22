using log4net;
using log4net.Core;
using Microsoft.VisualBasic.Logging;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using Exception = System.Exception;

namespace ModernGUI
{
    public class Logger : ILog
    {

        private readonly static ILog Filelog = log4net.LogManager.GetLogger("FileLogger");

        FileInfo _log4netInfo;
        string _Program, _LogOutputFile, _From;

        SmtpClient _smtpClient;
        NetworkCredential _networkCredential;
        List<string> _ToEmailAddress;

        bool _Emailer;

        public bool EmailErrorLog{get{ return _Emailer; } set { _Emailer = value; } }

        public Logger( string Program, bool Emailer)
        {
            _Program = Program;
            _Emailer = Emailer;

            string AppConfig = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Log4net.config");
            _log4netInfo = new FileInfo(AppConfig);

            _LogOutputFile = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"log\" + Environment.UserName + " - " + Program + ""); //log file path

            log4net.GlobalContext.Properties["LogFileName"] = _LogOutputFile;

            _LogOutputFile = _LogOutputFile + ".log";

            log4net.Config.XmlConfigurator.Configure(_log4netInfo);
        }



        public void Info(object message)
        {
            Filelog.Error(message);
        }

        public void Error(object message)
        {
            Filelog.Error(message);

            if (_Emailer && File.Exists(_LogOutputFile))
            {
                Email(_smtpClient, _networkCredential, "ERROR:" + Environment.UserName + " - " + _Program, message + "\n\n", _ToEmailAddress, _From, _LogOutputFile);
            }
        }


        public void Error(object message, Exception exception)
        {
            Filelog.Error(message, exception);
            if (_Emailer && File.Exists(_LogOutputFile))
            {
                Email(_smtpClient, _networkCredential, "ERROR:" + Environment.UserName + " - " + _Program, message + "\n\n" + exception.ToString(), _ToEmailAddress, _From, _LogOutputFile);
            }
        }

        public void ErrorFormat(string format, params object[] args)
        {

            Filelog.ErrorFormat(format, args);
        }

        public void ErrorFormat(string format, object arg0)
        {

            Filelog.ErrorFormat(format, arg0);
        }

        public void ErrorFormat(string format, object arg0, object arg1)
        {
            Filelog.ErrorFormat(format, arg0, arg1);

        }

        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            Filelog.ErrorFormat(format, arg0, arg1, arg2);

        }


        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            Filelog.ErrorFormat(provider, format,args);
        }

        public void Fatal(object message)
        {
            Filelog.Fatal(message); 
            if (_Emailer && File.Exists(_LogOutputFile))
            {
                Email(_smtpClient, _networkCredential, "FATAL:" + Environment.UserName + " - " + _Program, message + "\n\n", _ToEmailAddress, _From, _LogOutputFile);
            }

        }

        public void Fatal(object message, Exception exception)
        {
            Filelog.Fatal(message, exception);
           if (_Emailer && File.Exists(_LogOutputFile))
            {
                Email(_smtpClient, _networkCredential, "FATAL:" + Environment.UserName + " - " + _Program, message + "\n\n" + exception.ToString(), _ToEmailAddress, _From, _LogOutputFile);
            }
        }

        public void FatalFormat(string format, params object[] args)
        {
            Filelog.FatalFormat(format, args);
        }

        public void FatalFormat(string format, object arg0)
        {
            Filelog.FatalFormat(format, arg0);
        }

        public void FatalFormat(string format, object arg0, object arg1)
        {
            Filelog.FatalFormat(format, arg0, arg1);

        }

        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            Filelog.FatalFormat(format, arg0, arg1, arg2);

        }

        public void SetMailer(SmtpClient smtpClient,
                                NetworkCredential networkCredential,
                                List<string> ToEmailAddress,
                                string From)
        {
            _smtpClient = smtpClient;
            _networkCredential = networkCredential;
            _ToEmailAddress = ToEmailAddress;
            _From = From;
        }



        private void Email(SmtpClient smtpClient,
                           NetworkCredential networkCredential,
                           string Subject,
                           string body,
                           List<string> ToEmailAddress,
                           string From,
                           string filename = "")
        {
            try
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                smtpClient.EnableSsl = true;

                MailAddress from = new MailAddress(From);

                MailMessage mail = new System.Net.Mail.MailMessage();

                foreach (string item in ToEmailAddress)
                {
                    mail.To.Add(item);
                }

                mail.From = from;
                mail.Subject = Subject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = body;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;

                if (filename != "")
                {
                    Cursor currentcursor = Cursor.Current;

                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        int scnnumber = 0;

                        foreach (Screen screen in Screen.AllScreens)
                        {
                            Rectangle bounds = screen.Bounds;
                            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))

                            using (Graphics g = Graphics.FromImage(bitmap))
                            {
                                g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);

                                var stream = new MemoryStream();
                                bitmap.Save(stream, ImageFormat.Jpeg);
                                stream.Position = 0;

                                mail.Attachments.Add(new Attachment(stream, "screenshot" + scnnumber.ToString() + ".jpg"));

                                scnnumber++;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        System.Diagnostics.Debug.WriteLine("Unable to take screnshot");
                    }
                    finally
                    {
                        if (currentcursor == null)
                        {
                            Cursor.Current = Cursors.Default;
                        }
                        else
                        {
                            Cursor.Current = currentcursor;
                        }
                    }
                }

                

                smtpClient.Send(mail);
            }
            catch (SmtpException ex)
            {
                Filelog.Error("ERROR: sending logger email - SmtpException has occured", ex);
            }

        }

        private const int SRC_COPY = 0xCC0020;

        public bool IsDebugEnabled { get { return Filelog.IsDebugEnabled; } }

        bool IsInfoEnabled { get { return Filelog.IsInfoEnabled; } }

        bool IsWarnEnabled { get { return Filelog.IsWarnEnabled; } }

        bool IsErrorEnabled { get { return Filelog.IsErrorEnabled; } }


        bool IsFatalEnabled { get { return Filelog.IsFatalEnabled; } }

        bool ILog.IsInfoEnabled => Filelog.IsInfoEnabled;

        bool ILog.IsWarnEnabled => Filelog.IsWarnEnabled;

        bool ILog.IsErrorEnabled => Filelog.IsErrorEnabled;

        bool ILog.IsFatalEnabled => Filelog.IsFatalEnabled;

        ILogger ILoggerWrapper.Logger => Filelog.Logger;

        void Debug(object message)
        {
            Filelog.Debug(message);
        }


        void Debug(object message, Exception exception)
        {
            Filelog.Debug(message, exception);
        }

        void DebugFormat(string format, params object[] args)
        {

            Filelog.DebugFormat(format, args);
        }

        void DebugFormat(string format, object arg0)
        {

            Filelog.DebugFormat(format, arg0);

        }

        void DebugFormat(string format, object arg0, object arg1)
        {

            Filelog.DebugFormat(format, arg0, arg1);
        }

       void DebugFormat(string format, object arg0, object arg1, object arg2)
        {

            Filelog.DebugFormat(format, arg0, arg1, arg2);
        }

        void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {

            Filelog.DebugFormat(provider, format, args);
        }


        void Info(object message, Exception exception)
        {
            Filelog.Info(message, exception);
        }

        void InfoFormat(string format, params object[] args)
        {

            Filelog.InfoFormat(format, args);
        }

        void InfoFormat(string format, object arg0)
        {
            Filelog.InfoFormat(format, arg0);
        }


        void InfoFormat(string format, object arg0, object arg1)
        {

            Filelog.InfoFormat(format, arg0, arg1);
        }


        void InfoFormat(string format, object arg0, object arg1, object arg2)
        {

            Filelog.InfoFormat(format, arg0, arg1, arg2);
        }

        void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            Filelog.InfoFormat(provider, format, args);
        }

        void Warn(object message)
        {
            Filelog.Warn(message);
        }

        void Warn(object message, Exception exception)
        {
            Filelog.Warn(message, exception);
        }

        void WarnFormat(string format, params object[] args)
        {
            Filelog.WarnFormat(format, args);
        }

        void WarnFormat(string format, object arg0)
        {
            Filelog.WarnFormat(format, arg0);
        }

        void WarnFormat(string format, object arg0, object arg1)
        {
            Filelog.WarnFormat(format, arg0, arg1);
        }

        void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            Filelog.WarnFormat(format, arg0, arg1, arg2);
        }

        void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            Filelog.WarnFormat(provider, format, args);
        }

        void ILog.Debug(object message)
        {
            Filelog.Debug(message);
        }

        void ILog.Debug(object message, Exception exception)
        {
            Filelog.Debug(message, exception);
        }

        void ILog.DebugFormat(string format, params object[] args)
        {
            Filelog.DebugFormat(format, args);
        }

        void ILog.DebugFormat(string format, object arg0)
        {
            Filelog.DebugFormat(format, arg0);
        }

        void ILog.DebugFormat(string format, object arg0, object arg1)
        {
            Filelog.DebugFormat(format, arg0, arg1);
        }

        void ILog.DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            Filelog.DebugFormat(format, arg0, arg1, arg2);
        }

        void ILog.DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            Filelog.DebugFormat(provider, format, args);
        }

        void ILog.Info(object message, Exception exception)
        {
            Filelog.Info(message, exception);
        }

        void ILog.InfoFormat(string format, params object[] args)
        {
            Filelog.InfoFormat(format, args);
        }

        void ILog.InfoFormat(string format, object arg0)
        {
            Filelog.InfoFormat(format, arg0);
        }

        void ILog.InfoFormat(string format, object arg0, object arg1)
        {
            Filelog.InfoFormat(format, arg0, arg1);
        }

        void ILog.InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            Filelog.InfoFormat(format, arg0, arg1, arg2);
        }

        void ILog.InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            Filelog.InfoFormat(provider, format, args);
        }

        void ILog.Warn(object message)
        {
            Filelog.Warn(message);
        }

        void ILog.Warn(object message, Exception exception)
        {
            Filelog.Warn(message, exception);
        }

        void ILog.WarnFormat(string format, params object[] args)
        {
            Filelog.WarnFormat(format, args);
        }

        void ILog.WarnFormat(string format, object arg0)
        {
            Filelog.WarnFormat(format, arg0);
        }

        void ILog.WarnFormat(string format, object arg0, object arg1)
        {
            Filelog.WarnFormat(format, arg0, arg1);
        }

        void ILog.WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            Filelog.WarnFormat(format, arg0, arg1, arg2);
        }

        void ILog.WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            Filelog.WarnFormat(provider, format, args);
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            Filelog.FatalFormat(provider, format, args);
        }
    }

}
