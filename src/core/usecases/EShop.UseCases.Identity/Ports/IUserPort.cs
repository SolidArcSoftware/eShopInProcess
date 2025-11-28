using EShop.UseCases.Identity.Contracts.Input;
using EShop.UseCases.Identity.Contracts.Output;

namespace EShop.UseCases.Identity.Ports
{
    public interface IUserPort
    {
        Task<CreateUserInRoleOutput> CreateUserInRoleAsync(CreateUserInRoleInput createUserInRoleInput);
        Task<GetUserInRoleOutput> GetUserInRoleAsync(GetUserInRoleInput getUserInRoleInput);
    }
}