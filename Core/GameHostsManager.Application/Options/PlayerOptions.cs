namespace GameHostsManager.Application.Options
{
    public class PlayerOptions
    {
        public ushort MinPlayer { get; set; } = 2;
        public ushort MaxPlayers { get; set; } = ushort.MaxValue;
    }
}
