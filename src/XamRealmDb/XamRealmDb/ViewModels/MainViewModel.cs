using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Realms;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamRealmDb.Entities;
using XamRealmDb.Managers;

namespace XamRealmDb.ViewModels
{
    [INotifyPropertyChanged]
    public partial class MainViewModel
    {
        private readonly Realm _realm;
        private readonly ItemsManager _itemsManager;
        
        [ObservableProperty]
        private IEnumerable<ItemEntity> _items;

        public MainViewModel(ItemsManager itemsManager)
        {
            _realm = Realm.GetInstance();
            _itemsManager = itemsManager;

            Items = _realm.All<ItemEntity>();
        }

        [ICommand]
        private void OnAppearing()
        {
            _itemsManager.ToggleAutoRefresh();
        }

        [ICommand]
        private async Task UpdateValue()
        {
            await _itemsManager.UpdateItems();
        }


        [ICommand]
        private async Task RefreshValues()
        {
            await _itemsManager.RefreshItems();
        }
    }
}
