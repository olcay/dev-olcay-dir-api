using Newtonsoft.Json;
using WebApi.Entities;
using WebApi.Enums;

namespace WebApi.ResourceParameters
{
    public class PetsResourceParameters
    {
        const int maxPageSize = 20;

        public string SearchQuery { get; set; }

        public int CreatedById { get; set; }

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

        public string OrderBy { get; set; } = "Published desc";
        
        public string Fields { get; set; }

        [JsonProperty("PetTypeValue")]
        public PetType? PetType { get; set; }

        public int CityId { get; set; }

        public int RaceId { get; set; }

        [JsonProperty("AgeValue")]
        public PetAge Age { get; set; }

        [JsonProperty("GenderValue")]
        public Gender Gender { get; set; }

        [JsonProperty("SizeValue")]
        public Size Size { get; set; }

        [JsonProperty("FromWhereValue")]
        public FromWhere FromWhere { get; set; }
    }
}
