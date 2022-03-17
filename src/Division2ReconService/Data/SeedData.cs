using Newtonsoft.Json;

namespace Division2ReconService.Data
{
    public class SeedData
    {
        private readonly Division2ReconDbContext _dbContext;
        public SeedData(Division2ReconDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SeedInitialDataAsync()
        {
            await SeedCustomersAsync();
        }
        public async Task SeedCustomersAsync()
        {
            if (_dbContext.Customers.Any()) return;

            var jsonData = File.ReadAllText(@"Customer.json");
            var customers = JsonConvert.DeserializeObject<List<Customer>>(jsonData);
            if (customers == null || customers.Count <= 0) return;

            _dbContext.AddRange(customers);
            await _dbContext.SaveChangesAsync();
        }
    }
}
