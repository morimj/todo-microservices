using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using SharedKernel.Filters;
using TaskService.Domain.Entities;
using TaskService.Domain.Interfaces;
using TaskService.Infrastructure.Configurations;
using TaskService.Infrastructure.Persistence.Documents;

public class TaskRepository : ITaskRepository
{
    private readonly IMongoCollection<TodoTaskDocument> _collection;
    public TaskRepository(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["MongoDbSettings:ConnectionString"]);
        var database = client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);
        _collection = database.GetCollection<TodoTaskDocument>(configuration["MongoDbSettings:CollectionName"]);
    }

    public async Task<TodoTask?> AddAsync(TodoTask task)
    {
        try
        {
            var doc = new TodoTaskDocument
            {
                Id = Guid.NewGuid(),
                TaskTitle = task.TaskTitle,
                IsCompleted = task.IsCompleted,
                UserId = task.UserId,
                CreatedAt = task.CreatedAt
            };

            await _collection.InsertOneAsync(doc);

            return doc.ToEntity();
        }
        catch
        {
            return null;
            throw;
        }
    }

    public async Task<List<TodoTask>?> GetAllAsync(TodoTaskFilter? todoTaskFilter = null)
    {
        var query = _collection.Find(new BsonDocument());

        if (todoTaskFilter != null)
        {
            if (todoTaskFilter.IsCompleted.HasValue)
                query = _collection.Find(t => t.IsCompleted == todoTaskFilter.IsCompleted);

            if (!string.IsNullOrWhiteSpace(todoTaskFilter.TitleContains))
                query = _collection.Find(t => t.TaskTitle.Contains(todoTaskFilter.TitleContains));

            if (todoTaskFilter.UserId.HasValue && todoTaskFilter.UserId != default)
                query = _collection.Find(t => t.Id == todoTaskFilter.UserId);
        }

        var docs = await query.ToListAsync();

        return docs.Select(doc => doc.ToEntity()).ToList();
    }

    public async Task<TodoTask?> GetByIdAsync(Guid id)
    {
        var doc = await _collection.Find(t => t.Id == id).FirstOrDefaultAsync();

        return doc == null ? null : doc.ToEntity();
    }

    public async Task SaveChangesAsync() => await Task.CompletedTask;

    public async Task UpdateAsync(TodoTask task)
    {
        var filter = Builders<TodoTaskDocument>.Filter.Eq(x => x.Id, task.Id);

        var document = TodoTaskMapper.ToDocument(task);

        await _collection.ReplaceOneAsync(filter, document);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _collection.DeleteOneAsync(Builders<TodoTaskDocument>.Filter.Eq(x => x.Id, id));
    }
}