namespace GameHostsManager.Application.Dto
{
    public class HostRoomFilterDto
    {
        public bool? OnlyPublic { get; set; }
        public bool? OldFirsts { get; set; }

        public Guid[]? HostInfoIds { get; set; }
        public Guid[]? OwnerIds { get; set; }

        public string? SearchString { get; set; }
        public string? Password { get; set; }
        public string? IpAddress { get; set; }

        public ushort? Port { get; set; }

        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
