using Amazon.DynamoDBv2;

using src.domain.common.interfaces;
using src.domain.infra;

namespace src.domain.infrastructure.database;

public class DatabaseService : IDatabase
{
  private readonly IConfiguration _configuration;
  private readonly DyanamoDB _dynamo;
  private readonly AmazonDynamoDBClient _connection;
  public DatabaseService(IConfiguration config, DyanamoDB dyanamoDB)
  {
    _configuration = config;
    _dynamo = dyanamoDB;
    _connection = _dynamo.Connection;
  }
  public async Task<string> Persist(string PK, string SK, Dictionary<string, object> data)
  {
    data.Add("PK", PK);
    data.Add("SK", SK);

    await _connection.PutItemAsync(
      tableName: _configuration["DynamoDB:TableName"],
      DyanamoDBMapper.Marshall(data)
    );
    return PK;
  }

  public async Task<Dictionary<string, object>> Find(string PK, string SK)
  {
    var key = DyanamoDBMapper.Marshall(new Dictionary<string, object>()
    {
      { "PK", PK },
      { "SK", SK }
    });

    var result = await _connection.GetItemAsync(
      tableName: _configuration["DynamoDB:TableName"],
      key: key
    );
    return DyanamoDBMapper.Unmarshall(result.Item);
  }
}
