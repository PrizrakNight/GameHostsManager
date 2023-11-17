namespace GameHostsManager.Application.Contracts.HostRooms
{
    public class HostRoomConnectionInfoContract
    {
        public string IpAddress { get; set; } = null!;

        public ushort Port { get; set; }
    }
}
