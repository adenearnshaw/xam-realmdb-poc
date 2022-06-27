using Shiny.Jobs;
using System;
using System.Threading.Tasks;
using XamRealmDb.Api;
using XamRealmDb.Data;

namespace XamRealmDb.Managers;

public class ItemsManager
{
    private readonly ApiClient _apiClient;
    private readonly ItemRepository _itemRepository;
    private readonly RefreshDataJob _refreshDataJob;

    public bool IsAutoRefreshEnabled { get; private set; }

    public ItemsManager(ApiClient apiClient,
                        ItemRepository itemRepository,
                        RefreshDataJob refreshDataJob)
    {
        _apiClient = apiClient;
        _itemRepository = itemRepository;
        _refreshDataJob = refreshDataJob;
    }

    public async Task RefreshItems()
    {
        try
        {
            var latestValues = await _apiClient.FetchLatestValue();
            await _itemRepository.AddValuesToDb(latestValues);
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    public async Task UpdateItems()
    {
        try
        {
            await _apiClient.UpdateLatestValue();
            //await RefreshItems();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void ToggleAutoRefresh()
    {
        IsAutoRefreshEnabled = !IsAutoRefreshEnabled;

        if (IsAutoRefreshEnabled)
        {
            _refreshDataJob.StartJob();
        }
        else
        {
            _refreshDataJob?.StopJob();
        }
    }

}
