using Realms;
using System;

namespace XamRealmDb.Entities;

public class ItemEntity : RealmObject
{
    [PrimaryKey]
    public string Id { get; set; }

    public string Value { get; set; }

    public bool IsAppValue =>
        Value != null && Value.StartsWith("APP");

    public static ItemEntity Create(string value)
        => new ItemEntity
        {
            Id = Guid.NewGuid().ToString(),
            Value = value,
        };
}
