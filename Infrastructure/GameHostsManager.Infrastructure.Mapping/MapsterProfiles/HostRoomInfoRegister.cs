using GameHostsManager.Application.Contracts.HostRooms;
using GameHostsManager.Domain;
using Mapster;

namespace GameHostsManager.Infrastructure.Mapping.MapsterProfiles
{
    public class HostRoomInfoRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<HostRoom, HostRoomContract>()
                .Ignore(d => d.IsPublic)
                .AfterMapping((s, d) =>
                {
                    d.IsPublic = string.IsNullOrWhiteSpace(s.Password);
                });
        }
    }
}
