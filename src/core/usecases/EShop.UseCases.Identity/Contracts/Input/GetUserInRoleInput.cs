using DataArc.OrchestratR.Abstractions;

namespace EShop.UseCases.Identity.Contracts.Input
{
    public record GetUserInRoleInput(int UserId, int RoleId) : IOrchestratorInput;
}