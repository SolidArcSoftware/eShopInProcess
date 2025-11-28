using EShop.Domain.Identity.Entities;
using EShop.Features.Cataloging.Interfaces;
using EShop.UseCases.Identity.Contracts.Input;
using EShop.UseCases.Identity.Dtos;
using EShop.UseCases.Identity.Ports;

namespace EShop.Features.Identity.Services
{
    public class UserService(IUserPort userPort) : IUserService
    {
        public async Task<UserDto> CreateUserInRoleAsync(UserDto userDto)
        {
            // QUERY: Get user in role
            var userInRoleInput = new GetUserInRoleInput(userDto.UserId, userDto.RoleId);
            var result = await userPort.GetUserInRoleAsync(userInRoleInput);

            // If result has a user → user already in role
            if (result.UserDto is not null)
                return new UserDto(); // user already assigned

            // DOMAIN: run policies
            var userEntity = new UserEntity(
                userDto.UserId,
                userDto.EmailConfirmed,
                userDto.LockoutEnabled,
                userDto.AccessFailedCount
            );

            var policyResult = userEntity.ValidateForRoleAssignment();
            if (!policyResult.IsSuccess)
                return new UserDto(); // or expose the policyResult.Message

            // COMMAND: orchestrate assigning user to role
            var input = new CreateUserInRoleInput(userDto);
            var userResult = await userPort.CreateUserInRoleAsync(input);

            return userResult.UserDto ?? new();
        }
    }
}