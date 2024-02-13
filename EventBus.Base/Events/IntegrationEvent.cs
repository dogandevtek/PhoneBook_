using System;

using Newtonsoft.Json;

namespace EventBus.Base.Events
{
    public class IntegrationEvent
    {
        [JsonProperty]
        public Guid Id { get; private set; }

        [JsonProperty]
        public DateTime CreateTime { get; private set; }

        public IntegrationEvent() {
            Id = Guid.NewGuid();
            CreateTime = DateTime.Now;
        }

        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTime createTime) {
            Id = id;
            CreateTime = createTime; 
        }
    }
}
