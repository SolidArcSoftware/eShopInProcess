using DataArc.Abstractions;
using DataArc.OrchestratR;
using DataArc.Extension.Query;

using Eshop.Persistence.Models.Identity;
using EShop.Persistence.Contexts.Identity;

using EShop.UseCases.Identity.Contracts.Input;
using EShop.UseCases.Identity.Contracts.Output;
using EShop.UseCases.Identity.Dtos;

using Microsoft.AspNetCore.Identity;

namespace EShop.Orchestration.Identity.Orchestrators
{
    public class GetUserInRoleOrchestrator : Orchestrator<GetUserInRoleInput, GetUserInRoleOutput>
    {
        private readonly IAsyncDatabaseQueryBuilder _asyncDatabaseQueryBuilder;

        public GetUserInRoleOrchestrator(IAsyncDatabaseQueryBuilder asyncDatabaseQueryBuilder)
        {
            _asyncDatabaseQueryBuilder = asyncDatabaseQueryBuilder;
        }

        public override async Task<GetUserInRoleOutput> ExecuteAsync(GetUserInRoleInput input, GetUserInRoleOutput output)
        {
            var usersInRole = await _asyncDatabaseQueryBuilder
                .UseQueryContext<EShopIdentityContext, EShopUser>(u => u.Id == input.UserId)
                .Join<EShopIdentityContext, IdentityUserRole<int>>(
                    bag => bag.Get<EShopUser>()!.Id,
                    ur => ur.UserId
                )
                .Join<EShopIdentityContext, IdentityRole<int>>(
                    bag => bag.Get<IdentityUserRole<int>>()!.RoleId,
                    r => r.Id
                )
                .Select(bag => new UserDto
                {
                    UserId = bag.Get<EShopUser>()!.Id,
                    AccessFailedCount = bag.Get<EShopUser>()!.AccessFailedCount,
                    EmailConfirmed = bag.Get<EShopUser>()!.EmailConfirmed,
                    LockoutEnabled = bag.Get<EShopUser>()!.LockoutEnabled,
                    RoleId = bag.Get<IdentityRole<int>>()!.Id
                })
                .ToListAsync();

            if (usersInRole == null || usersInRole.Count == 0)
                return new GetUserInRoleOutput();

            output.UserDto = usersInRole.First();
            return output;
        }
    }
}