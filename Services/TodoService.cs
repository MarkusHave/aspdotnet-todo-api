using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Driver;
using TodoApi.Models;

namespace TodoApi.Services
{
  public class TodoService
  {
    private readonly IMongoCollection<TodoItem> _todoItems;


    public TodoService(ITodoDatabaseSettings settings)
    {
      var client = new MongoClient(settings.ConnectionString);
      var db = client.GetDatabase(settings.DatabaseName);

      _todoItems = db.GetCollection<TodoItem>(settings.TodoCollectionName);
    }


    public async Task<List<TodoItem>> GetTodoItems() =>
      await _todoItems.FindAsync(item => true).Result.ToListAsync();


    public async Task<TodoItem> GetTodoItem(string id) =>
      await _todoItems.FindAsync<TodoItem>(item =>
        item.Id == id).Result.FirstOrDefaultAsync();


    public async Task<TodoItem> Create(TodoItem todoItem) =>
      await _todoItems.InsertOneAsync(todoItem).ContinueWith(_ => todoItem);


    public async Task Update(string id, TodoItem todoItem) =>
      await _todoItems.ReplaceOneAsync(todoItem => todoItem.Id == id, todoItem);


    public async Task Remove(string id) =>
      await _todoItems.DeleteOneAsync(item => item.Id == id);
  }
}