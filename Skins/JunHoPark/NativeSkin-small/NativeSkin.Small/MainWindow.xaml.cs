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
        private readonly Home homeControl;
        private readonly PlayListView playListviewControl;
        private readonly ArtistDetail artistDetail;

        public MainWindow()
        {
            InitializeComponent();
            homeControl = new Home(this);
            playListviewControl = new PlayListView(this);
            artistDetail = new ArtistDetail(this);

            presenter.Content = homeControl;

        }

        public void ListViewControl()
        {
            presenter.Content = playListviewControl;
        }

        public void HomeControl() {
            presenter.Content = homeControl;
        }

        public void ArtistDetailControl()
        {
            presenter.Content = artistDetail;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            Handle = new WindowInteropHelper(this).Handle;
        }
    }
}
