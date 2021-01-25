using JoostIT.WinchHunt.WinchHuntNet.Data;
using JoostIT.WinchHunt.WinchHuntNet.HunterMessaging;
using JoostIT.WinchHunt.WinchHuntNet.LoraMessaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet
{

    /// <summary>
    /// Manages devices. Note that instances of this class are not completely thread safe
    /// </summary>
    public class DeviceManager
    {
        private const int CleanupTimerInterval = 1000;
        private System.Timers.Timer cleanupTimer = new System.Timers.Timer(CleanupTimerInterval);

        private readonly Object devicesLock = new object();
        private Dictionary<string, WinchFox> foxes = new Dictionary<string, WinchFox>();

        /// <summary>
        /// Gets the hunter device to which the system is connected, which receives LoRa data sent by other devices
        /// </summary>
        public WinchHunter Hunter { get; private set; }

        /// <summary>
        /// Gets known foxes
        /// </summary>
        public IReadOnlyDictionary<string, WinchFox> Foxes
        {
            get
            {
                return foxes;
            }
        }

        /// <summary>
        /// The maximum timespan that a Fox can be inactive, before being removed from the Foxes collection.
        /// </summary>
        public TimeSpan MaxFoxAge { get; set; } = new TimeSpan(1, 0, 0, 0);

        /// <summary>
        /// Gets raised when a new fox is added to the collection
        /// </summary>
        public event EventHandler<DeviceEventArgs> FoxAdded;
        
        /// <summary>
        /// Gets raised when a already known fox is updated
        /// </summary>
        public event EventHandler<DeviceEventArgs> FoxUpdated;

        /// <summary>
        /// Gets raised when a fox is removed from the collection
        /// </summary>
        public event EventHandler<DeviceEventArgs> FoxRemoved;

        /// <summary>
        /// Gets raiseed when a heartbeat is received from the hunter
        /// </summary>
        public event EventHandler<DeviceEventArgs> HunterHeartbeatReceived;

        /// <summary>
        /// Processes a heartbeat message
        /// </summary>
        /// <param name="packet"></param>
        internal void ProcessHeartbeatMessage(HeartbeatPacket packet)
        {
            if(Hunter == null)
            {
                Hunter = new WinchHunter(packet);
            }
            else
            {
                Hunter.UpdateData(packet);
            }

            RaiseHunterHeartbeatReceived(Hunter.Device);
        }


        internal void ProcessFoxMessage(FoxMessage message)
        {

            bool foxAdded = false;
            bool foxUpdated = false;

            lock (devicesLock)
            {
                if (foxes.ContainsKey(message.Device.Id))
                {
                    foxes[message.Device.Id].Update(message);
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
            FoxAdded?.Invoke(this, new DeviceEventArgs(device));
        }


        private void RaiseFoxUpdated(DeviceInfo device)
        {
            FoxUpdated?.Invoke(this, new DeviceEventArgs(device));
        }


        private void RaiseFoxRemoved(DeviceInfo device)
        {
            FoxRemoved?.Invoke(this, new DeviceEventArgs(device));
        }


        private void RaiseHunterHeartbeatReceived(DeviceInfo device)
        {
            HunterHeartbeatReceived?.Invoke(this, new DeviceEventArgs(device));
        }


        internal DeviceManager()
        {
            cleanupTimer.AutoReset = true;
            cleanupTimer.Elapsed += CleanupTimer_Elapsed;
            cleanupTimer.Enabled = true;
        }


    }
}
