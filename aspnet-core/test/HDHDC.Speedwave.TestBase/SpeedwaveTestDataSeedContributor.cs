using HDHDC.Speedwave.SpeedwaveEntityCollection;
using HDHDC.Speedwave.SpeedyConsts;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;

namespace HDHDC.Speedwave
{
    public class SpeedwaveTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IRepository<CustomerEntity, int> _customerRepo;
        private readonly IIdentityUserRepository _identityUserRepo;

        public SpeedwaveTestDataSeedContributor(
            IGuidGenerator guidGenerator,
            IRepository<CustomerEntity, int> customerRepository,
            IIdentityUserRepository identityUserRepository)
        {
            _guidGenerator = guidGenerator;
            _customerRepo = customerRepository;
            _identityUserRepo = identityUserRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {

            var user = await _identityUserRepo.InsertAsync(new IdentityUser(
            _guidGenerator.Create(),
            "Customer01",
            "Customer01@test.com")
            { });

            var customer = await _customerRepo.InsertAsync(new CustomerEntity(user.Id)
            {
                Status = EntityStatusConsts.Active
            });
        }
    }
}