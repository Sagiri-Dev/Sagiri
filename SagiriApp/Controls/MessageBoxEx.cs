using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace SagiriApp.Controls
{
    internal class MessageBoxEx : IDisposable
    {
        #region Property

        private System.Threading.Timer _Timer { get; set; }

        private string _Text = default!;
        private string _Caption = default!;
        private int _Interval = default!;
        private bool disposedValue;

        #endregion Property

        #region DLL's

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        #endregion DLL's

        #region Constructor 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text"> body message </param>
        /// <param name="caption"> window title </param>
        /// <param name="interval"> closing time </param>
        public MessageBoxEx(string text, string caption, int interval)
        {
            _Text = text;
            _Caption = caption;
            _Interval = interval;
        }

        ~MessageBoxEx() => Dispose(disposing: false);

        #endregion Constructor

        public void Show()
        {
            this._Timer = new System.Threading.Timer((state) =>
            {
                var window = FindWindow(null, _Caption);

                if (window != IntPtr.Zero)
                    SendMessage(window, 0x0010, IntPtr.Zero, IntPtr.Zero);

                this._Timer.Dispose();
            }, null, _Interval, Timeout.Infinite);

            MessageBox.Show(_Text, _Caption);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _Timer = null;
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
