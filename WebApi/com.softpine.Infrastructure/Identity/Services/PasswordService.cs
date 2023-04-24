using Microsoft.Extensions.Options;
using com.softpine.muvany.core.Interfaces;
using System.Security.Cryptography;
using com.softpine.muvany.infrastructure.Identity.Options;
using com.softpine.muvany.models.Constants;
using com.softpine.muvany.models.Interfaces;

namespace com.softpine.muvany.infrastructure.Identity.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class PasswordService : IPasswordService
    {
        private readonly PasswordOptions _options;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public PasswordService(IOptions<PasswordOptions> options)
        {
            _options = options.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public bool Check(string hash, string password)
        {
            var parts = hash.Split('.');
            if (parts.Length != 3)
            {
                throw new FormatException(ApiConstants.Messages.UnexpectedHashFormatError);
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using (var algorithm = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations
                ))
            {
                var keyToCheck = algorithm.GetBytes(_options.KeySize);
                return keyToCheck.SequenceEqual(key);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Hash(string password)
        {
            //PBKDF2 implementation
            using (var algorithm = new Rfc2898DeriveBytes(
                password,
                _options.SaltSize,
                _options.Iterations
                ))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(_options.KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{_options.Iterations}.{salt}.{key}";
            }
        }
    }
}
