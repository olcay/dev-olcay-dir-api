using System.Collections.Generic;
using WebApi.Entities;

namespace WebApi.Models
{
    public class ItemForCreationDto : ItemForManipulationDto
    {
        public ItemType ItemType { get; set; }

        public ICollection<TagForCreationDto> Tags { get; set; }
          = new List<TagForCreationDto>();

    }
}
