using System.Reflection;

namespace API.Utilities;

public static class DirectoryHelper
{
    internal static string GetProjectFolderPath()
    {
        string baseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
            throw new DirectoryNotFoundException("Could not find the project's base directory.");

        string path = Directory.GetParent(baseDirectory)?.Parent?.Parent?.FullName ??
            throw new DirectoryNotFoundException("Could not find project folder.");

        return path;
    }
}
