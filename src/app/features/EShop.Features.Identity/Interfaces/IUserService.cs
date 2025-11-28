using EShop.UseCases.Identity.Dtos;

namespace EShop.Features.Cataloging.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateUserInRoleAsync(UserDto userDto);
    }
}