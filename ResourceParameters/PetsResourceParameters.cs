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

        public string OrderBy { get; set; } = "PublishDate desc";
        
        public string Fields { get; set; }

        public int PetTypeId { get; set; }

        public int CityId { get; set; }

        public int RaceId { get; set; }

        public int AgeId { get; set; }

        public int GenderId { get; set; }

        public int SizeId { get; set; }

        public int FromWhereId { get; set; }
    }
}
