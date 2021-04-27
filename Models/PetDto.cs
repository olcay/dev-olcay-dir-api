using System;

namespace WebApi.Models
{
    public class PetDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public string PetType { get; set; }

        public string Age { get; set; }

        public string Gender { get; set; }

        public string Size { get; set; }

        public string FromWhere { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }

        public string Race { get; set; }

        public string City { get; set; }
        
        public DateTimeOffset CreationDate { get; set; }
        
        public string CreatedBy { get; set; }

        public bool FoundHome { get; set; }

        public DateTimeOffset PublishDate { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsPublished { get; set; }
    }
}
