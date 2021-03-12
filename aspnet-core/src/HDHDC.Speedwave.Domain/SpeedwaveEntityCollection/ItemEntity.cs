using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    public class ItemEntity
        : Entity<int>, IMustHaveCreator, IHasCreationTime, IModificationAuditedObject, IDeletionAuditedObject, IHasConcurrencyStamp
    {
        public ItemEntity()
            : base()
        { 
        }        

        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public float ItemPrice { get; set; }
        public string ItemThumbnail { get; set; }
        public int QuantityId { get; set; }
        public int MinRequiredTimeId { get; set; }
        public string NormalizedQuantityLabel { get; set; }
        public QuantityEntity QuantityEntity { get; protected set; }
        public MinRequiredTimeEntity MinRequiredTimeEntity { get; protected set; }
        public ICollection<ItemCategoryEntity> ItemCategoryEntities { get; protected set; }
        public ICollection<ItemStoreBranchEntity> ItemStoreBranchEntities { get; protected set; }

        #region audited_properties
        public Guid CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? LastModifierId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public string ConcurrencyStamp { get; set; }
        #endregion
    }
}
