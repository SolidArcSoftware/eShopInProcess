using DataArc.OrchestratR.Abstractions;
using DataArc.OrchestratR;

using EShop.UseCases.Identity.Contracts.Input;
using EShop.UseCases.Identity.Contracts.Output;
using EShop.UseCases.Identity.Ports;
using EShop.Orchestration.Identity.Orchestrators;

namespace EShop.Modules.Identity.Adapters
{
    public class UserAdapter(IOrchestratorHandler orchestratorHandler) : IUserPort
    {
        public async Task<CreateUserInRoleOutput> CreateUserInRoleAsync(CreateUserInRoleInput createUserInRoleInput) 
            => await orchestratorHandler.OrchestrateAsync<CreateUserInRoleOrchestrator, CreateUserInRoleOutput>(createUserInRoleInput, new CreateUserInRoleOutput());

        public async Task<GetUserInRoleOutput> GetUserInRoleAsync(GetUserInRoleInput getUserInRoleInput) 
            => await orchestratorHandler.OrchestrateAsync<GetUserInRoleOrchestrator, GetUserInRoleOutput>(new GetUserInRoleInput(getUserInRoleInput.UserId, getUserInRoleInput.RoleId), new GetUserInRoleOutput());
    }
}