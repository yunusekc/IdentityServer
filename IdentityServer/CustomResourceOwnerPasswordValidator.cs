using Dapper;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using System.Data.SqlClient;
namespace IdentityServer
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserRepository _userRepository;

        public CustomResourceOwnerPasswordValidator(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _userRepository.GetUserByUsernameAsync(context.UserName);

            if (user == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid credentials");
                return;
            }

            context.Result = new GrantValidationResult(
                subject: user.Id.ToString(),
                authenticationMethod: "password",
                claims: new List<System.Security.Claims.Claim>
                {
                new System.Security.Claims.Claim("name", user.FirstName),
                new System.Security.Claims.Claim("email", user.Email)
                }
            );
        }
    }

    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<User?> GetUserByUsernameAsync(string Email)
        {
            const string sql = "SELECT * FROM Users WHERE Email = @Email";

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email });
            }
        }
    }
}
