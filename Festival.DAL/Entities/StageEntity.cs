using System.Collections.Generic;
using Nemesis.Essentials.Design;

namespace Festival.DAL.Entities
{
    public record StageEntity: EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public ICollection<EventEntity> Events { get; set; } = new ValueCollection<EventEntity>(EventEntity.EventWithoutStageAndBandComparer);
    }
}
