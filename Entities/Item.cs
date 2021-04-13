using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Item
    {
        public Item()
        {
            CreationDate = DateTimeOffset.UtcNow;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public ItemType ItemType { get; set; }

        [MaxLength(1500)]
        public string Description { get; set; }

        [Required]
        public DateTimeOffset CreationDate { get; set; }

        [ForeignKey("CreatedById")]
        public Account CreatedBy { get; set; }

        public int CreatedById { get; set; }

        public IList<ItemTag> ItemTags { get; set; }
            = new List<ItemTag>();
    }

    public enum ItemType
    {
        Generic,
        Game,
        Book,
        Movie,
        
    }
}