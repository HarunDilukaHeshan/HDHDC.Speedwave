using HDHDC.Speedwave.SpeedwaveEntityCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace HDHDC.Speedwave.Dependencies
{
    public class DeliveryScheduleSelector : ITransientDependency
    {
        public DeliveryScheduleSelector(
            CitySelector citySelector, 
            StoreBranchSelector storeBranchSelector, 
            IRepository<ItemStoreBranchEntity> itemStoreBranchRepository)
        {
            CitySelector = citySelector;
            StoreBranchSelector = storeBranchSelector;
            ItemStoreBranchRepository = itemStoreBranchRepository;
        }

        protected CitySelector CitySelector { get; }
        protected StoreBranchSelector StoreBranchSelector { get; }
        protected IRepository<ItemStoreBranchEntity> ItemStoreBranchRepository { get; }

        public async Task<IList<DeliveryScheduleEntity>> GetDeliverySchedules(int cityId, int[] itemsList)
        {
            var cityEs = await CitySelector.GetCitiesAsync(cityId);
            var branchEs = await StoreBranchSelector.GetStoreBranches(cityEs);
            var itemBranchesList = new List<Tuple<StoreBranchEntity, List<int>>>();

            foreach(var branchE in branchEs)
            {
                var b = Tuple.Create(branchE, new List<int>());
                foreach(var itemId in itemsList)
                {
                    var itemBranchE = await ItemStoreBranchRepository.FindAsync(e => e.StoreBranchID == branchE.Id && e.ItemID == itemId);
                    if (itemBranchE != null) b.Item2.Add(itemId);
                }

                itemBranchesList.Add(b);
            }

            itemBranchesList.OrderBy(e => e.Item2.Count);

            throw new NotImplementedException();
        }
    }
}
