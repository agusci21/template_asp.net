
using System.Security.Cryptography;
using System.Text;

namespace Helpers;

public static class EncriptPasswordHelper
{

    public static string Encript(string input)
    {   
        DotNetEnv.Env.Load();
        string? salt = Environment.GetEnvironmentVariable("salt") ?? throw new ArgumentException("No enviroment variable founed ('SALT')");
        byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
        byte[] hash = SHA256.HashData(bytes);
        return Convert.ToBase64String(hash);
    }

    public static bool Validate(string plainTextInput, string hashedInput)
    {
        string newHashedPin = Encript(plainTextInput);
        return newHashedPin.Equals(hashedInput); 
    }

}