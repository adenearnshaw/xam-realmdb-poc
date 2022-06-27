namespace XamRealmDb.Api;

public class DataUpdaterWorker : IHostedService
{
    private const int IntervalMilliseconds = 5000;
    private readonly DataService _dataService;

    private Timer _timer;

    public DataUpdaterWorker(DataService dataService)
    {
        _dataService = dataService;
    }


    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(UpdateValue, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(IntervalMilliseconds));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    private void UpdateValue(object state)
    {
        _dataService.UpdateValue();
    }
}
