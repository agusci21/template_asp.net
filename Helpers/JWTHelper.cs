namespace Helpers;

public static class JWTHelper
{
    public static string GetSignature()
    {
        string? salt = Environment.GetEnvironmentVariable("jwtsignature") ?? throw new ArgumentException("No enviroment variable founed ('JWTSIGNATURE')");
        return salt;
    }
}