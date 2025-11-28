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

    }
}