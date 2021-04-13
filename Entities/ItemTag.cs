using System;

namespace WebApi.Entities
{
    public class ItemTag
    {
        public ItemTag()
        {
            LinkCreated = DateTimeOffset.UtcNow;
        }

        public Guid ItemId { get; set; }

        public Item Item { get; set; }

        public Guid TagId { get; set; }

        public Tag Tag { get; set; }

        public DateTimeOffset LinkCreated { get; set; }
    }
}