using System;
using System.Collections.Generic;

namespace Festival.DAL.Entities
{
    public record EventEntity : EntityBase
    {

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public StageEntity Stage { get; set; }
        public Guid StageId { get; set; }
        public BandEntity Band { get; set; }
        public Guid BandId { get; set; }

        private sealed class EventWithoutStageAndBandEqualityComparer : IEqualityComparer<EventEntity>
        {
            public bool Equals(EventEntity x, EventEntity y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.StartTime.Equals(y.StartTime) && x.EndTime.Equals(y.EndTime) && x.StageId.Equals(y.StageId) && x.BandId.Equals(y.BandId);
            }

            public int GetHashCode(EventEntity obj)
            {
                return HashCode.Combine(obj.StartTime, obj.EndTime, obj.StageId, obj.BandId);
            }
        }

        public static IEqualityComparer<EventEntity> EventWithoutStageAndBandComparer { get; } = new EventWithoutStageAndBandEqualityComparer();
    }

}
