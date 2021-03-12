using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.UtilityServices
{
    public class Md5Hasher : IMd5Hasher, ITransientDependency
    {
        public string GenerateBase16Hash(byte[] buffer)
        {
            var hash = Hash(buffer);
            return BitConverter.ToString(hash).Replace("-", "");
        }

        public string GenerateBase64Hash(byte[] buffer)
        {
            var hash = Hash(buffer);
            return Convert.ToBase64String(hash);
        }

        protected byte[] Hash(byte[] buffer)
        {
            byte[] hash = null;
            using (var md5 = MD5.Create()) { hash = md5.ComputeHash(buffer); }
            return hash;
        }
    }
}
