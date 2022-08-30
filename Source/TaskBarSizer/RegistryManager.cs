//srgjanx

using Microsoft.Win32;
using System;
using System.Reflection;

namespace TaskBarSizer
{
    internal class RegistryManager : IDisposable
    {
        public delegate void RegistyManagerErrorEventHandler(Exception ex, string methodName);
        public event RegistyManagerErrorEventHandler OnRegistyManagerErrorOccurred;

        //Objects:
        private RegistryKey BaseRegistryKey = Microsoft.Win32.Registry.CurrentUser;
        private readonly string RegistryKey64 = "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced";

        ~RegistryManager()
        {
            Dispose(false);
        }

        public string GetRegistryKey
        {
            get
            {
                return RegistryKey64;
            }
        }

        public RegistryKey OpenRegistryKey
        {
            get
            {
                try
                {
                    return BaseRegistryKey.OpenSubKey(GetRegistryKey);
                }
                catch (NullReferenceException ex)
                {
                    throw new NullReferenceException($"Registry key '{GetRegistryKey}' does not exists. Reason: {ex.Message}");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void WriteDword(string key, object value)
        {
            try
            {
                if (value != null)
                {
                    BaseRegistryKey.CreateSubKey(RegistryKey64).SetValue(key, value, RegistryValueKind.DWord);
                }
            }
            catch (Exception ex)
            {
                OnRegistyManagerErrorOccurred?.Invoke(ex, MethodBase.GetCurrentMethod().Name);
            }
        }

        public string Read(string key)
        {
            try
            {
                RegistryKey subKey = OpenRegistryKey;
                if (subKey == null)
                    throw new Exception($"Sub key {RegistryKey64} does not exists.");
                else
                {
                    return subKey.GetValue(key) != null ? subKey.GetValue(key).ToString() : null;
                }
            }
            catch (Exception ex)
            {
                OnRegistyManagerErrorOccurred?.Invoke(ex, MethodBase.GetCurrentMethod().Name);
            }
            return null;

        }

        #region IDisposable Pattern
        // Flag: Has Dispose already been called?
        private bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {

            }

            BaseRegistryKey = null;
            disposed = true;
        }
        #endregion
    }
}