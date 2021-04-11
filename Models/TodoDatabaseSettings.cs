namespace TodoApi.Models
{
  public class TodoDatabaseSettings : ITodoDatabaseSettings
  {
    public string TodoCollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
  }

  public interface ITodoDatabaseSettings
  {
    string TodoCollectionName { get; set; }
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
  }
}