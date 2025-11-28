using EShop.Modules.Identity.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace EShopInProcess.Integration.Tests.Modules
{
    public class EShop_Identity_Module_Tests
    {
        [SetUp]
        public void SetUp()
        {
            var serviceProvider = new ServiceCollection()
                .AddIdentityModule() // Add the Identity module
                .BuildServiceProvider();
        }

        // Add your tests here
    }
}