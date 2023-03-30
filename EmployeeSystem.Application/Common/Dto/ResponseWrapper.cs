namespace EmployeeSystem.Application.Common.Dto
{
    public record ResponseWrapper<T>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / (decimal)PageSize);
        public T? Data { get; set; }
    }
}