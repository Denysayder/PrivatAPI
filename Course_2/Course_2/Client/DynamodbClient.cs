using System;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Course_2.Extensions;
using Course_2.Model;

namespace Course_2.Client
{
    public class DynamodbClient : IDynamodbClient
    {
        public string _tableName;

        private readonly IAmazonDynamoDB _dynamoDb;
        public DynamodbClient(IAmazonDynamoDB dynamoDb)
        {
            _dynamoDb = dynamoDb;
            _tableName = Constants.TableName;
        }

        public async Task<citydbrepository> citydbrepository(string ID)
        {
            var item = new GetItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "ID", new AttributeValue{S=ID} },
                }
            };
            var response = await _dynamoDb.GetItemAsync(item);
            var result = response.Item.ToClass<citydbrepository>();
            return result;
        }

        public async Task PostDepartmentInfo(citydbrepository Citydbrepository)
        {
            var item = new PutItemRequest
            {
                TableName = _tableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    { "ID", new AttributeValue{S= Citydbrepository.ID} },
                    { "Time", new AttributeValue{S= Citydbrepository.Time} },
                    { "ccy", new AttributeValue{S= Citydbrepository.ccy} },
                    { "base_ccy", new AttributeValue{S=Citydbrepository.base_ccy} },
                    { "buy", new AttributeValue{S=Citydbrepository.buy} },
                    { "sale", new AttributeValue{S=Citydbrepository.sale} },

                }

            };
            await _dynamoDb.PutItemAsync(item);
        }

        public async Task<List<citydbrepository>> GetAll()
        {
            var result = new List<citydbrepository>();


            var request = new ScanRequest
            {
                TableName = Constants.TableName
            };
            var response = await _dynamoDb.ScanAsync(request);
            if (response.Items == null || response.Items.Count == 0)
                return null;
            foreach (Dictionary<string, AttributeValue> item in response.Items)
            {
                result.Add(item.ToClass<citydbrepository>());
            }
            return result;
        }

        public async Task DeleteArchiveInfo(string ID, string Time)
        {
            var item = new Amazon.DynamoDBv2.Model.DeleteItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "ID", new AttributeValue{S= ID} },
                    { "Time", new AttributeValue{S=Time} }
                }

            };
            await _dynamoDb.DeleteItemAsync(item);
        }

        public async Task UpdateCurrencyAsync(string ID, string Time, string NewBuy, string NewSale)
        {
            var request = new UpdateItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "ID", new AttributeValue { S = ID } },
                    { "Time", new AttributeValue { S = Time } }
                },
                AttributeUpdates = new Dictionary<string, AttributeValueUpdate>
                {
                    ["buy"] = new AttributeValueUpdate()
                    {
                        Action = AttributeAction.PUT,
                        Value = new AttributeValue { S = NewBuy }
                    },

                    ["sale"] = new AttributeValueUpdate()
                    {
                        Action = AttributeAction.PUT,
                        Value = new AttributeValue { S = NewSale }
                    }
                }
            }; await _dynamoDb.UpdateItemAsync(request);
        }
    }
}

