namespace GameHostsManager.Application.Contracts.HostRooms
{
    public class CreateHostRoomContract
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Password { get; set; }

        public string IpAddress { get; set; } = null!;

        public ushort Port { get; set; }
        public ushort MaxPlayers { get; set; }
    }
}
