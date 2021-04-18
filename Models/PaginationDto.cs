namespace WebApi.Models
{
    public class PaginationDto
    {
        public int TotalCount { get; private set; }
        public int PageSize { get; private set; }
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }

        public PaginationDto(int totalCount, int pageSize, int currentPage, int totalPages)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = totalPages;
        }
    }
}