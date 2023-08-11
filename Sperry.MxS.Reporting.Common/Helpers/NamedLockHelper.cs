using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sperry.MxS.Core.Common.Helpers
{
    public class NamedLockHelper
    {
        public int LockCount { get; set; }

        private static object dictionaryLock = new object();

        public void AddupLock()
        {
            LockCount++;
        }

        public void ReduceLock()
        {
            LockCount--;
        }

        private static ConcurrentDictionary<string, NamedLockHelper> locker = new ConcurrentDictionary<string, NamedLockHelper>();

        public static NamedLockHelper GetOrAddLocker(string name)
        {
            lock (dictionaryLock)
            {
                var locked = locker.GetOrAdd(name, new NamedLockHelper());
                locked.AddupLock();
                return locked;
            }
        }

        public static bool IsLockerExists(string name)
        {
            if (locker.ContainsKey(name))
            {
                return true;
            }

            return false;
        }

        public static bool ReleaseLocker(string name, NamedLockHelper lockToRelease)
        {
            lock (dictionaryLock)
            {
                lockToRelease.ReduceLock();
                if (lockToRelease.LockCount < 1)
                {
                    NamedLockHelper value;
                    return locker.TryRemove(name, out value);
                }
                return false;
            }
        }
    }
}
