using Mapster;
using UserService.Application.DTO;
using UserService.Application.Extensions;
using UserService.Persistence.Models;

namespace UserService.Application.Mapping;

public class MappingConfig
{
    public static void Configure()
    {
        TypeAdapterConfig.GlobalSettings.NewConfig<UserModel, UserResponse>()
            .Map(dest => dest.Role, src => src.Role.GetDescription());
    }
}