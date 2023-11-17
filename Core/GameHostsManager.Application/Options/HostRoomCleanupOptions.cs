namespace GameHostsManager.Application.Options
{
    public class HostRoomCleanupOptions
    {
        public int RoomLifetimeInSeconds { get; set; } = 600;

        public int CleanupSize { get; set; } = 4000;
        public int CleanupPeriodInSeconds { get; set; } = 300;
    }
}
