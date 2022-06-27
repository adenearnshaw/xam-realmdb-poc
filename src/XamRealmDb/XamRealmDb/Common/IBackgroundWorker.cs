using System;
using System.Threading.Tasks;

namespace XamRealmDb.Common;

// <summary>
/// Current status of background tasks
/// </summary>
public enum BackgroundTaskStatus
{
    None,
    Running,
    Completed,
    Failed
}

/// <summary>
/// Event Args for events raised from background task
/// </summary>
public record BackgroundTaskEventArgs(BackgroundTaskStatus TaskStatus = BackgroundTaskStatus.None, Exception Error = null)
{
    public BackgroundTaskStatus TaskStatus { get; } = TaskStatus;

    /// <summary>
    /// Error raised during a background task.
    /// </summary>
    public Exception Error { get; } = Error;
}

/// <summary>
/// Cross-platform background task runner
/// </summary>
public interface IBackgroundTaskRunner
{
    /// <summary>
    /// Status changed event raised when the status of a background task changes
    /// </summary>
    event EventHandler<BackgroundTaskEventArgs> StatusChanged;

    /// <summary>
    /// Method for injecting a process into a background thread.
    /// </summary>
    void RunInBackground(Func<Task> backgroundTask);
}

/// <summary>
/// Cross-platform implementation for a native background task.
/// </summary>
public class BackgroundTaskRunner : IBackgroundTaskRunner
{
    public event EventHandler<BackgroundTaskEventArgs> StatusChanged;

    public void RunInBackground(Func<Task> backgroundTask)
    {
        _ = Task.Run(async () =>
        {
            try
            {
                OnStatusChanged(BackgroundTaskStatus.Running);
                await backgroundTask();
                OnStatusChanged(BackgroundTaskStatus.Completed);
            }
            catch (Exception ex)
            {
                OnStatusChanged(BackgroundTaskStatus.Failed, ex);
            }
        });
    }

    private void OnStatusChanged(BackgroundTaskStatus status, Exception ex = null)
    {
        StatusChanged?.Invoke(this, new(status, ex));
    }
}