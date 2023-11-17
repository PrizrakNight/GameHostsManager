namespace GameHostsManager.Domain
{
    public class HostRoom
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OwnerId { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Password { get; set; }

        public string IpAddress { get; set; } = null!;

        public ushort Port { get; set; }
        public ushort MaxPlayers { get; set; }
        public ushort CurrentPlayers { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
    }
}