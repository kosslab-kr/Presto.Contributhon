using System;

namespace Presto.Plugin.YouTube.Supports
{
    public class BaseViewModel : BaseModel
    {
        public event EventHandler CloseRequested;

        public void RaiseCloseRequested()
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
