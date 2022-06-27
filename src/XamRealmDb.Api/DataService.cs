namespace XamRealmDb.Api;

public class DataService
{
    private readonly ILogger<DataService> logger;

    public List<DataObject> Values { get; set; } = new();

    public DataService(ILogger<DataService> logger)
    {
        this.logger = logger;
        UpdateValue();
    }

    public void UpdateValue(string source = "API")
    {
        var latestValue = new DataObject($"{source}::{DateTime.UtcNow.TimeOfDay.ToString(@"hh\:mm\:ss")}");
        Values.Add(latestValue);
        logger.LogInformation("{0} updated = {1}", nameof(latestValue), latestValue.Value);
    }
}

public class DataObject
{
    public string Id { get; set; }
    public string Value { get; set; }

    public DataObject()
    {

    }

    public DataObject(string value)
    {
        Id = Guid.NewGuid().ToString();
        Value = value;
    }
}
