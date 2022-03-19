using Newtonsoft.Json;

namespace Division2ReconService.Data
{
    /// <summary>
    /// Seed Data
    /// </summary>
    public class SeedData
    {
        private readonly Division2ReconDbContext _dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public SeedData(Division2ReconDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Seed Initial Data
        /// </summary>
        /// <returns></returns>
        public async Task SeedInitialDataAsync()
        {
            await SeedCustomersAsync();

            await SeedMachinesAsync();
        }

        /// <summary>
        ///  Seed Customer data
        /// </summary>
        /// <returns></returns>
        public async Task SeedCustomersAsync()
        {
            if (_dbContext.Customers.Any()) return;

            var jsonData = File.ReadAllText(@"Customer.json");
            var customers = JsonConvert.DeserializeObject<List<Customer>>(jsonData);
            if (customers == null || customers.Count <= 0) return;

            _dbContext.Customers.AddRange(customers);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        ///  Seed Machine data
        /// </summary>
        /// <returns></returns>
        public async Task SeedMachinesAsync()
        {
            if (_dbContext.Machines.Any()) return;

            var jsonData = File.ReadAllText(@"Machine.json");
            var machines = JsonConvert.DeserializeObject<List<Machine>>(jsonData);
            if (machines == null || machines.Count <= 0) return;

            _dbContext.Machines.AddRange(machines);
            await _dbContext.SaveChangesAsync();
        }
    }
}
