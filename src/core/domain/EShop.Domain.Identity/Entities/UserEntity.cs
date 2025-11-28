using EShop.Domain.Identity.Events;
using EShop.Domain.SharedKernel.Policies;

namespace EShop.Domain.Identity.Entities
{
    public class UserEntity
    {
        public int UserId { get; }
        public bool EmailConfirmed { get; }
        public bool LockoutEnabled { get; }
        public int AccessFailedCount { get; }

        public UserEntity(int userId,
            bool emailConfirmed,
            bool lockoutEnabled,
            int accessFailedCount)
        {
            UserId = userId;
            EmailConfirmed = emailConfirmed;
            LockoutEnabled = lockoutEnabled;
            AccessFailedCount = accessFailedCount;
        }

        // Domain business rules
        public bool IsActive() => EmailConfirmed && !LockoutEnabled;
        public bool CanLogin() => EmailConfirmed && AccessFailedCount < 5;
        public bool IsLockedOut() => LockoutEnabled;
        public bool EmailNotConfirmed() => !EmailConfirmed;
        public bool NeverLoggedIn() => AccessFailedCount == 0;

        public PolicyResult ValidateForRoleAssignment()
        {
            if (!EmailConfirmed)
                return PolicyResult.Fail("User email not confirmed.", new UserInRoleCreationFailedEvent());

            if (LockoutEnabled)
                return PolicyResult.Fail("User is locked out.", new UserInRoleCreationFailedEvent());

            return PolicyResult.Success(new UserInRoleCreationSuccessEvent());
        }
    }
}