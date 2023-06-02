using System.Reflection;

namespace API.Utilities;

public static class DirectoryHelper
{
    private static IWebHostEnvironment _environment;

    public static void Configure(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    internal static string GetProjectFolderPath()
    {
        string baseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
            throw new DirectoryNotFoundException("Could not find the project's base directory.");

        if (_environment.IsProduction())
        {
            return baseDirectory;
        }

        string path = Directory.GetParent(baseDirectory)?.Parent?.Parent?.FullName ??
            throw new DirectoryNotFoundException("Could not find project folder.");

        return path;
    }
}
