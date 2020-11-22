namespace CourseLibrary.API.ResourceParameters
{
    public class AuthorsResourceParameters
    {
        const int maxPageSize = 20;
        private int pageSize = 10;

        public string MainCategory { get; set; }
        public string SearchQuery { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize 
        { 
            get => pageSize; 
            set => pageSize = (value > maxPageSize) ? maxPageSize : value; 
        }
    }
}
