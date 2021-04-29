using System;
using WebApi.Enums;
using WebApi.Models.Accounts;

namespace WebApi.Models
{
    public class PetFullDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string PetStatusText { get; set; }
        
        public EnumDto<PetType> PetType { get; set; }

        public EnumDto<PetAge> Age { get; set; }

        public EnumDto<Gender> Gender { get; set; }

        public EnumDto<Size> Size { get; set; }

        public EnumDto<FromWhere> FromWhere { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }

        public RaceDto Race { get; set; }

        public EnumDto City { get; set; }
        
        public DateTimeOffset Created { get; set; }
        
        public AccountDto CreatedBy { get; set; }

        public DateTimeOffset? Published { get; set; }
    }
}
