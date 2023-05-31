using Microsoft.Extensions.Options;

namespace Budgetr.Functions.Services.Cosmos;

internal class CosmosBudgetServiceOptions
{
    public string CosmosEndpoint { get; set; } = "";
    public string CosmosKey { get; set; } = "";
}

internal class CosmosBudgetService : IBudgetRepository
{
    private readonly ILogger<CosmosBudgetService>? _logger;

    private readonly IValidator<Budget> _budgetValidator;

    private readonly CosmosClient _client;

    private const string DATABASE_NAME = "Budgetr";
    private const string CONTAINER_NAME = "budgets";

    private Container Container => _client.GetDatabase(DATABASE_NAME).GetContainer(CONTAINER_NAME);

    public CosmosBudgetService(IOptions<CosmosBudgetServiceOptions> optionsProvider, IValidator<Budget> budgetValidator, ILogger<CosmosBudgetService>? logger = null)
    {
        var options = optionsProvider.Value;

        _client = new(accountEndpoint: options.CosmosEndpoint, authKeyOrResourceToken: options.CosmosKey,
            clientOptions: new CosmosClientOptions
            {
                SerializerOptions = new CosmosSerializationOptions
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            });
        _budgetValidator = budgetValidator;
        _logger = logger;
    }

    public async Task<Budget> CreateAsync(Guid userId, BudgetApiModel entity)
    {
        try
        {
            if (userId == Guid.Empty) throw new ArgumentException("User Id cannot be an empty Guid", nameof(userId));

            var budget = entity.Convert(userId, id: Guid.NewGuid());

            _budgetValidator.ValidateAndThrow(budget);

            return await Container.CreateItemAsync(
                item: budget,
                partitionKey: new PartitionKey(userId.ToString())
            );
        }
        catch (ValidationException ex)
        {
            _logger?.LogError(ex, "Budget did not pass validations.");
            throw;
        }
        catch (ArgumentException ex)
        {
            _logger?.LogError(ex, "User was not logged in.");
            throw;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Unknown exception occurred.");
            throw;
        }
    }

    public async Task DeleteAsync(Guid userId, Guid id)
    {
        await Container.DeleteItemAsync<Budget>(
            id: id.ToString(),
            partitionKey: new PartitionKey(userId.ToString())
        );
    }

    public async Task<Budget> UpdateAsync(Guid userId, Guid id, BudgetApiModel entity)
    {
        try
        {
            var budget = entity.Convert(userId, id);

            _budgetValidator.ValidateAndThrow(budget);

            return await Container.UpsertItemAsync(
                item: budget,
                partitionKey: new PartitionKey(userId.ToString())
            );
        }
        catch (ValidationException ex)
        {
            _logger?.LogError(ex, "Budget did not pass validations.");
            throw;
        }
        catch (ArgumentException ex)
        {
            _logger?.LogError(ex, "User was not logged in.");
            throw;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Unknown exception occurred.");
            throw;
        }
    }

    public async Task<Budget?> GetAsync(Guid id, Guid userId)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty", nameof(id));
        if (userId == Guid.Empty) throw new ArgumentException("User Id cannot be empty", nameof(userId));

        return await Container.ReadItemAsync<Budget?>(id.ToString(), new PartitionKey(userId.ToString()));
    }

    public async Task<IEnumerable<Budget>> GetAsync(IEnumerable<Guid> ids, Guid userId)
    {
        if (!ids.Any()) throw new ArgumentException("Ids cannot be empty", nameof(ids));
        if (userId == Guid.Empty) throw new ArgumentException("User Id cannot be empty", nameof(userId));

        string sql = "SELECT b.id, b.userId, b.saveTimeUtc, b.comment, b.incomes, b.housingLoans, b.autoLoans, b.otherLoans, b.expenses FROM budgets b WHERE b.id IN @ids AND b.userId = @userId";
        var query = new QueryDefinition(sql).WithParameter("@ids", $"('{string.Join("', '", ids)})'").WithParameter("@userId", userId.ToString());
        using var feed = Container.GetItemQueryIterator<Budget>(query);
        List<Budget> results = new();
        while (feed.HasMoreResults)
        {
            FeedResponse<Budget> response = await feed.ReadNextAsync();
            foreach (var item in response)
            {
                results.Add(item);
            }
        }
        return results;
    }

    public async Task<IEnumerable<Budget>> GetAllAsync(Guid userId)
    {
        if (userId == Guid.Empty) throw new ArgumentException("User Id cannot be empty", nameof(userId));

        using var feed = Container.GetItemLinqQueryable<Budget>().Where(b => b.UserId == userId).ToFeedIterator();
        List<Budget> results = new();
        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
            foreach (var item in response)
            {
                results.Add(item);
            }
        }
        return results;
    }

    public async Task<Budget?> GetLatestAsync(Guid userId)
    {
        if (userId == Guid.Empty) throw new ArgumentException("User Id cannot be empty", nameof(userId));

        using var feed = Container.GetItemLinqQueryable<Budget>().Where(b => b.UserId == userId).OrderByDescending(b => b.SaveTimeUtc).ToFeedIterator();
        List<Budget> results = new();
        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
            foreach (var item in response)
            {
                return item;
            }
        }
        return null;
    }
}
