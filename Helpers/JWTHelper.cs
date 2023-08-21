namespace Helpers;

public static class JWTHelper
{
    public static string GetSignature()
    {
        DotNetEnv.Env.Load();
        string? salt = Environment.GetEnvironmentVariable("JWTSIGNATURE") ?? throw new ArgumentException("No enviroment variable founed ('JWTSIGNATURE')");
        return salt;
    }
}