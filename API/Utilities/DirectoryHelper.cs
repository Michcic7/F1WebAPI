using System.Reflection;

namespace API.Utilities;

public static class DirectoryHelper
{
    internal static string GetProjectFolderPath()
    {
        string baseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        return Directory.GetParent(baseDirectory).Parent.Parent.FullName;
    }
}
