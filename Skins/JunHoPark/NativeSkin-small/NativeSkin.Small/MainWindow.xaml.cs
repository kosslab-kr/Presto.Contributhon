using Presto.Common;
using System;
using System.Collections.Generic;
using System.Windows.Interop;
using System.Windows.Navigation;
using PCancelEventArgs = Presto.Common.CancelEventArgs;

namespace NativeSkin.Small
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : NavigationWindow, IPrestoWindow
    {
        #region IPrestoWindow
        public IntPtr Handle { get; private set; }

        event EventHandler<PCancelEventArgs> IPrestoWindow.Closing
        {
            add => _closingEvents.Add(value);
            remove => _closingEvents.Remove(value);
        }

        event EventHandler<EventArgs> IPrestoWindow.Loaded
        {
            add => _loadedEvents.Add(value);
            remove => _loadedEvents.Remove(value);
        }

        List<EventHandler<PCancelEventArgs>> _closingEvents = new List<EventHandler<PCancelEventArgs>>();
        List<EventHandler<EventArgs>> _loadedEvents = new List<EventHandler<EventArgs>>();
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            Source = new Uri("pack://application:,,,/NativeSkin.Small;component/Home.xaml");
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            Handle = new WindowInteropHelper(this).Handle;
        }
    }
}
