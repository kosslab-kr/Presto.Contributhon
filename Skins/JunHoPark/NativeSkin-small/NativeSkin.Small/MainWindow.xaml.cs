using Presto.Common;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Navigation;
using PCancelEventArgs = Presto.Common.CancelEventArgs;

namespace NativeSkin.Small
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window, IPrestoWindow
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
        private readonly HomeControl homeControl;
        private readonly PlayListviewControl playListviewControl;

        public MainWindow()
        {
            InitializeComponent();
            //Source = new Uri("pack://application:,,,/NativeSkin.Small;component/Home.xaml");
            homeControl = new HomeControl(this);
            presenter.Content = homeControl;
            playListviewControl = new PlayListviewControl(this);
        }

        public void ShowHome()
        {
            MessageBox.Show("홈 켜라");
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            Handle = new WindowInteropHelper(this).Handle;
        }
    }
}
