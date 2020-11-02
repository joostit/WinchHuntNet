using JoostIT.WinchHunt.WinchHuntConnectionLib.Data;
using JoostIT.WinchHunt.WinchHuntConnectionLib.LoraMessaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntConnectionLib
{
    public class DeviceManager
    {
        private const int CleanupTimerInterval = 1000;
        private System.Timers.Timer cleanupTimer = new System.Timers.Timer(CleanupTimerInterval);

        private readonly Object devicesLock = new object();
        private Dictionary<string, WinchFox> foxes = new Dictionary<string, WinchFox>();

        IReadOnlyDictionary<string, WinchFox> Foxes
        {
            get
            {
                return foxes;
            }
        }


        public TimeSpan MaxFoxAge { get; set; } = new TimeSpan(1, 0, 0, 0);


        public event EventHandler<DeviceEventArgs> FoxAdded;
        public event EventHandler<DeviceEventArgs> FoxUpdated;
        public event EventHandler<DeviceEventArgs> FoxRemoved;


        internal void ProcessFoxMessage(FoxMessage message)
        {

            bool foxAdded = false;
            bool foxUpdated = false;

            lock (devicesLock)
            {
                if (foxes.ContainsKey(message.Device.Id))
                {
                    foxes[message.Device.Id].UpdateGpsData(message);
                    foxUpdated = true;
                }
                else
                {
                    WinchFox fox = new WinchFox(message);
                    foxes.Add(fox.Device.Id, fox);
                    foxAdded = true;
                }
            }


            if (foxAdded)
            {
                RaiseFoxAdded(foxes[message.Device.Id].Device);
            }

            if (foxUpdated)
            {
                RaiseFoxUpdated(foxes[message.Device.Id].Device);
            }
        }


        private void CleanupTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            List<DeviceInfo> toRemove = new List<DeviceInfo>();
            DateTime now = DateTime.Now;

            lock (devicesLock)
            {
                foreach (var fox in foxes.Values)
                {
                    if (now - fox.LastUpdate > MaxFoxAge)
                    {
                        toRemove.Add(fox.Device);
                    }
                }

                foreach(var fox in toRemove)
                {
                    foxes.Remove(fox.Id);
                }
            }

            foreach(var fox in toRemove)
            {
                RaiseFoxRemoved(fox);
            }
        }


        private void RaiseFoxAdded(DeviceInfo device)
        {
            FoxAdded(this, new DeviceEventArgs(device));
        }


        private void RaiseFoxUpdated(DeviceInfo device)
        {
            FoxUpdated(this, new DeviceEventArgs(device));
        }


        private void RaiseFoxRemoved(DeviceInfo device)
        {
            FoxRemoved(this, new DeviceEventArgs(device));
        }


        internal DeviceManager()
        {
            cleanupTimer.AutoReset = true;
            cleanupTimer.Elapsed += CleanupTimer_Elapsed;
            cleanupTimer.Enabled = true;
        }


    }
}
