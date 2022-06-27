using System;
using Xamarin.Forms;

namespace XamRealmDb.Common;

public class WeakEvent<T> where T : EventArgs
{
    private readonly WeakEventManager _eventManager = new();

    public event EventHandler<T> Event
    {
        add => _eventManager.AddEventHandler<T>(value);
        remove => _eventManager.RemoveEventHandler<T>(value);
    }

    public void RaiseEvent(object sender, T eventArgs)
    {
        _eventManager.HandleEvent(sender, eventArgs, nameof(Event));
    }
}
