using DataArc.OrchestratR.Abstractions;
using EShop.UseCases.Identity.Dtos;

namespace EShop.UseCases.Identity.Contracts.Input
{
    public record CreateUserInRoleInput(UserDto UserDto) : IOrchestratorInput;
}