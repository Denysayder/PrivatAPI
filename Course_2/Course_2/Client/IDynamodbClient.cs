using System;
using Course_2.Model;

namespace Course_2.Client
{
    public interface IDynamodbClient
    {
        public Task<citydbrepository> citydbrepository(string ID);
        public Task PostDepartmentInfo(citydbrepository Citydbrepository);
        public Task<List<citydbrepository>> GetAll();
        public Task DeleteArchiveInfo(string ID, string Time);
        public Task UpdateCurrencyAsync(string ID, string Time, string NewBuy, string NewSale);
    }
}

