using System;
using WebApi.Models.Accounts;

namespace WebApi.Models
{
    public class PetDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string PetStatusText { get; set; }
        
        public string PetTypeText { get; set; }

        public string AgeText { get; set; }

        public string GenderText { get; set; }

        public string SizeText { get; set; }

        public string FromWhereText { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }

        public string RaceName { get; set; }

        public string CityText { get; set; }
        
        public DateTimeOffset Created { get; set; }
        
        public AccountDto CreatedBy { get; set; }

        public DateTimeOffset? Published { get; set; }
    }
}
