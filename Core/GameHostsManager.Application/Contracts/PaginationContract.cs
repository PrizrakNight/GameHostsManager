namespace GameHostsManager.Application.Contracts
{
    /// <summary>
    /// A contract describing pagination
    /// </summary>
    public class PaginationContract
    {
        /// <summary>
        /// Page number
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; } = 25;

        public int Skip => (Page - 1) * PageSize;
        public int Take => PageSize;
    }
}
