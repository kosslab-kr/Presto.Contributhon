using Presto.Plugin.YouTube.Supports;
using System;

namespace Presto.Plugin.YouTube.ViewModels
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
