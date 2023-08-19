namespace Helpers;
using System.Security.Cryptography;
using System.Text;

public static class JWTHelper
{
    public static string GetSignature ()
    {
        DotNetEnv.Env.Load();
        return "TODO: crear jwt";
    }
}