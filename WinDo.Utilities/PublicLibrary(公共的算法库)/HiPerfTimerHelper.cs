
using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading;

namespace WinDo.Utilities
{
    /// <summary>
    /// HiPerfTimerHelper
    /// 高性能计时器  
    /// <example>
    /// HiPerfTimerHelper perfTimer = new HiPerfTimerHelper();
    /// perfTimer.Start();
    /// ;执行一些相关的操作
    /// perfTimer.Stop();
    /// pt.Duration;//显示操作所耗费的时间
    /// </example>
    /// </summary>
    public class HiPerfTimerHelper
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);  

		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceFrequency(out long lpFrequency);
		
		private long startTime, stopTime;
		private long freq;
		
        
        public HiPerfTimerHelper()
		{
            startTime = 0;
            stopTime  = 0;

            if (QueryPerformanceFrequency(out freq) == false)
            {                
                throw new Win32Exception(); 
            }
		}
		
		/// <summary>
        /// Start the timer
		/// </summary>
		public void Start()
		{
            // lets do the waiting threads there work
            Thread.Sleep(0);  

			QueryPerformanceCounter(out startTime);
		}
		
		/// <summary>
        /// Stop the timer
		/// </summary> 
		public void Stop()
		{
		    QueryPerformanceCounter(out stopTime);
		}
		
        /// <summary>
        /// Returns the duration of the timer (in seconds)
        /// </summary>
        public double Duration
        {
        	get
        	{
            	return (double)(stopTime - startTime) / (double) freq;
            }
        }
    }
}
