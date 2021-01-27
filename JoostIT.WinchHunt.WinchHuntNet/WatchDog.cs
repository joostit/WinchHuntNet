using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace JoostIT.WinchHunt.WinchHuntNet
{
    internal class WatchDog : IDisposable
    {

        public event EventHandler Expired;

        private Timer timeoutTimer;

        private readonly int timeoutMs;

        /// <summary>
        /// Constructor. The timer will start immediately after the watchdog has been instantiated
        /// </summary>
        /// <param name="timeoutMs">The timeout to be set. </param>
        public WatchDog(int timeoutMs)
        {
            this.timeoutMs = timeoutMs;
            timeoutTimer = new Timer(TimerCallback, null, timeoutMs, Timeout.Infinite);
        }

        ~WatchDog()
        {
            Dispose(false);
        }

        /// <summary>
        /// Kick the watchdog so it restarts its timeout
        /// </summary>
        public void KickWatchdog()
        {
            timeoutTimer.Change(timeoutMs, Timeout.Infinite);
        }



        private void TimerCallback(object na)
        {
            Expired?.Invoke(this, EventArgs.Empty);
        }


        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                timeoutTimer.Dispose();
            }
        }


        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            
        }
    }
}
