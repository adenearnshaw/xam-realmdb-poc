using System;
using System.Threading.Tasks;
using XamRealmDb.Api;
using XamRealmDb.Common;

namespace XamRealmDb.Data;
public class RefreshDataJob
{
    private readonly ApiClient _apiClient;
    private readonly ItemRepository _itemRepository;
    private readonly BackgroundTaskRunner _backgroundTaskRunner;

    private bool _isRunning = false;

    public RefreshDataJob(ApiClient apiClient,
                          ItemRepository itemRepository)
    {
        _apiClient = apiClient;
        _itemRepository = itemRepository;
        _backgroundTaskRunner = new BackgroundTaskRunner();
    }


    public async void StartJob()
    {
        _isRunning = true;

        try
        {
            do
            {
                _backgroundTaskRunner.RunInBackground(async () =>
                {
                    var latestValues = await _apiClient.FetchLatestValue();
                    await _itemRepository.AddValuesToDb(latestValues);
                });
                await Task.Delay(5000);
            }
            while (_isRunning);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void StopJob()
    {
        _isRunning = false;
    }
}
