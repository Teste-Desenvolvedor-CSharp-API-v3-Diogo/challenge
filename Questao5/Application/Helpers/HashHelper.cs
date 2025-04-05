﻿using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Questao5.Application.Helpers
{
    public static class HashHelper
    {
        public static string ComputeHash(object obj)
        {
            var json = JsonSerializer.Serialize(obj);
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(json);
            var hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}
