namespace GameHostsManager.Application.Contracts.HostRooms
{
    /// <summary>
    /// Room Search Filter
    /// </summary>
    public class HostRoomFilterContract
    {
        /// <summary>
        /// if true it will return only public rooms that do not contain a password,
        /// if false, it will return only private rooms with a password,
        /// if null, it will return any rooms
        /// </summary>
        public bool? OnlyPublic { get; set; }

        /// <summary>
        /// Search string by room names and descriptions
        /// </summary>
        public string? SearchString { get; set; }

        /// <summary>
        /// Pagination Information
        /// </summary>
        public PaginationContract Pagination { get; set; } = new();
    }
}
