using System;
using WebApi.Entities;

namespace WebApi.Models
{
    public class ItemDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public ItemType ItemType { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreationDate { get; set; }
    }
}
