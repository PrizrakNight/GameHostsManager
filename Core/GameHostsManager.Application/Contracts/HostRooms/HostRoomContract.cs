namespace GameHostsManager.Application.Contracts.HostRooms
{
    public class HostRoomContract
    {
        public Guid Id { get; set; }

        public bool IsPublic { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }

        public ushort CurrentPlayers { get; set; }
        public ushort MaxPlayers { get; set; }
    }
}
