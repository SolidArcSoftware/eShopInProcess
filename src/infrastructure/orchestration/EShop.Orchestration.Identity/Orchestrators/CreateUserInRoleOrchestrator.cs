using DataArc.Abstractions;
using DataArc.OrchestratR;

using Eshop.Persistence.Models.Identity;
using EShop.Persistence.Contexts.Identity;

using EShop.UseCases.Identity.Contracts.Input;
using EShop.UseCases.Identity.Contracts.Output;
using EShop.UseCases.Identity.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EShop.Orchestration.Identity.Orchestrators
{
    public class CreateUserInRoleOrchestrator : Orchestrator<CreateUserInRoleInput, CreateUserInRoleOutput>
    {
        readonly IAsyncDatabaseCommandBuilder _asyncDatabaseCommandBuilder;
        readonly IAsyncDatabaseQueryBuilder _asyncDatabaseQueryBuilder;
        readonly ILogger<CreateUserInRoleOrchestrator> _logger;

        public CreateUserInRoleOrchestrator()
        {
        }

        public CreateUserInRoleOrchestrator(
            IAsyncDatabaseCommandBuilder asyncDatabaseCommandBuilder, 
            IAsyncDatabaseQueryBuilder asyncDatabaseQueryBuilder,
            ILogger<CreateUserInRoleOrchestrator> logger
            )
        {
            _asyncDatabaseCommandBuilder = asyncDatabaseCommandBuilder;
            _asyncDatabaseQueryBuilder = asyncDatabaseQueryBuilder;
            _logger = logger;
        }

        public override async Task<CreateUserInRoleOutput> ExecuteAsync(CreateUserInRoleInput input, CreateUserInRoleOutput output)
        {
            try
            {
                var user = new EShopUser
                {
                    UserName = input.UserDto.UserName,
                    NormalizedUserName = input.UserDto.NormalizedUserName,
                    Email = input.UserDto.NormalizedEmail,
                    NormalizedEmail = input.UserDto.NormalizedEmail,
                    EmailConfirmed = input.UserDto.EmailConfirmed,
                    SecurityStamp = input.UserDto.SecurityStamp,
                    PasswordHash = input.UserDto.PasswordHash,
                    LockoutEnabled = input.UserDto.LockoutEnabled,
                    AccessFailedCount = input.UserDto.AccessFailedCount
                };

                var createUserCmd = await _asyncDatabaseCommandBuilder
                    .UseCommandContext<EShopIdentityContext>()
                    .Add(user)
                    .BuildAsync();

                var userResult = await createUserCmd.ExecuteAsync();

                if (!userResult.Success)
                {
                    _logger.LogError("Failed creating user: {Errors}", string.Join(", ", userResult.Errors));
                    return output;
                }

                var newUserId = user.Id;

                var assignRoleCmd = await _asyncDatabaseCommandBuilder
                    .UseCommandContext<EShopIdentityContext>()
                    .Add(new IdentityUserRole<int>
                    {
                        UserId = newUserId,
                        RoleId = input.UserDto.RoleId
                    })
                    .BuildAsync();

                var roleResult = await assignRoleCmd.ExecuteAsync();

                if (!roleResult.Success)
                {
                    _logger.LogError("Failed assigning role: {Errors}", string.Join(", ", roleResult.Errors));
                    return output;
                }

                output.UserDto = new UserDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    NormalizedUserName = user.NormalizedUserName,
                    NormalizedEmail = user.NormalizedEmail,
                    EmailConfirmed = user.EmailConfirmed,
                    SecurityStamp = user.SecurityStamp,
                    PasswordHash = user.PasswordHash,
                    LockoutEnabled = user.LockoutEnabled,
                    AccessFailedCount = user.AccessFailedCount,
                    RoleId = input.UserDto.RoleId
                };

                return output;
            }
            catch
            {
                throw;
            }
        }
    }
}