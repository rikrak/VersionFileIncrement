using System.Linq;

namespace Solid.VersionIncrement.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "SharedAssemblyInfo.cs";
            if (args.Length >= 1)
            {
                fileName = args.First();
            }

            var versionFile = new VersionFile(fileName);
            var currentVersion = versionFile.GetVersion();
            versionFile.SetVersion(currentVersion.IncrementBuildNumber());
        }
    }
}