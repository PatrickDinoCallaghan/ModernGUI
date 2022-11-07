
using System.Timers;


namespace ModernGUI.Controls
{
    /// <summary>
    /// Allows the program to schedule a method at a given time
    /// Example: PatchForms.ScheduleMethod<int, int> scheduleMeth = new PatchForms.ScheduleMethod<int, int>();
    /// scheduleMeth.Start(testschedule, 0, DateTime.Now.AddSeconds(15));
    /// </summary>
    /// <typeparam name="T">Argument type</typeparam>
    /// <typeparam name="TResult">return type</typeparam>
    public class ScheduleMethod<T, TResult>
    {
        private static System.Timers.Timer timer;
        private Func<T, TResult> _MethodName;
        private object _Arg;
        private bool _asynchronous = false;
        private System.Threading.SynchronizationContext _UI_Context;

        public bool AutoRestart = false;

        /// <summary>
        /// This will allow you to set the function you want to run at a given time
        /// </summary>
        /// <param name="MethodName">mehtod you want to run</param>
        /// <param name="Arg">argument you want to pass through to the method</param>
        /// <param name="RunAt">time you wantfunction to run</param>
        /// <param name="asynchronous">if you want to run the function asyncronously set to true</param>
        public void Start(Func<T, TResult> MethodName, object Arg, DateTime scheduledTime, bool asynchronous = false)
        {
            double tickTime = (double)(scheduledTime - DateTime.Now).TotalMilliseconds;

            Start(MethodName, Arg, tickTime, asynchronous);
        }
        public void Start(Func<T, TResult> MethodName, object Arg, double Milliseconds, bool asynchronous = false)
        {
            _UI_Context = System.Threading.SynchronizationContext.Current;
            _MethodName = MethodName;
            _Arg = Arg;
            _asynchronous = asynchronous;

            if (Milliseconds < 0)
            {
                throw new ArgumentException("Scheduled time has already elapsed");
            }

            System.Timers.Timer timer = new System.Timers.Timer(Milliseconds);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_asynchronous)
            {
                _UI_Context.Send((o) =>
                {
                    _MethodName.Invoke((T)_Arg);
                }, null);
            }

            else
            {
                _MethodName.Invoke((T)_Arg);
            }
            timer.Stop();


            if (AutoRestart)
            {
                timer.Start();
            }

        }

    }
}