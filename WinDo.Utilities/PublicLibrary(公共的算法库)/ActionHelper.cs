using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinDo.Utilities.PublicLibrary
{
    /// <summary>
    /// 执行方法帮助类
    /// </summary>
    public static class ActionHelper
    {
        /// <summary>
        /// 延迟
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static Task Delay(int milliseconds)
        {
            //Console.WriteLine("Delay" + Thread.CurrentThread.ManagedThreadId);
            var tcs = new TaskCompletionSource<object>();
            new System.Threading.Timer(_ => tcs.SetResult(null)).Change(milliseconds, -1);
            return tcs.Task;
        }
        /// <summary>
        /// 节流 throttle 一段时间内只执行一次
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="throttle"></param>
        /// <returns></returns>
        public static EventHandler CreateThrottledEventHandler(EventHandler handler, TimeSpan throttle)
        {
            bool throttling = false;
            return (s, e) =>
            {
                if (throttling) return;
                throttling = true;
                handler(s, e);
                Task.Factory.StartNew(() => Thread.Sleep(throttle)).ContinueWith(_ => throttling = false);
            };
        }
        /// <summary>
        /// 防抖 固定时间内时间没有继续调用，则调用一次
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public static EventHandler CreateDebounceEventHandler(EventHandler handler, TimeSpan delay)
        {
            var last = 0;
            return (s, e) =>
            {
                var current = Interlocked.Increment(ref last);
                Task.Factory.StartNew(() => Thread.Sleep(delay)).ContinueWith(task =>
                {
                    if (current == last) handler(s, e);
                    task.Dispose();
                });
            };
        }
        /// <summary>
        /// 防抖 固定时间内时间没有继续调用，则调用一次
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static Action<T> Debounce<T>(this Action<T> func, int milliseconds = 100)
        {
            var last = 0;
            return arg =>
            {
                var current = Interlocked.Increment(ref last);
                Task.Factory.StartNew(() => Thread.Sleep(milliseconds)).ContinueWith(task =>
                {
                    if (current == last) func(arg);
                    task.Dispose();
                });
            };
        }

    }
}
