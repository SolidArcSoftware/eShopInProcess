using DataArc.OrchestratR.Abstractions;
using EShop.UseCases.Identity.Dtos;

namespace EShop.UseCases.Identity.Contracts.Output
{
    public class GetUserInRoleOutput : IOrchestratorOutput
    {
        public UserDto? UserDto { get; set; }
    }
}