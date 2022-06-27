using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamRealmDb.Api;
using XamRealmDb.Entities;

namespace XamRealmDb.Data;

public class ItemRepository
{
    public async Task AddValueToDb(DataObjectDto value)
    {
        try
        {
            var realm = Realm.GetInstance();
            await realm.WriteAsync(() =>
            {
                realm.Add(new ItemEntity { Id = value.Id, Value = value.Value }, update: true);
            });
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task AddValuesToDb(IEnumerable<DataObjectDto> values)
    {
        try
        {
            var realm = Realm.GetInstance();
            await realm.WriteAsync(() =>
            {
                realm.Add(values.Select(value => new ItemEntity { Id = value.Id, Value = value.Value }), update: true);
            });
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
