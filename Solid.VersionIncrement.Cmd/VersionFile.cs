using System;
using System.IO;
using System.Text;

namespace Solid.VersionIncrement.Cmd
{
    /// <summary>
    /// Represents a .Net AssemblyVersionInfo file
    /// </summary>
    public class VersionFile
    {
        #region Depenedncies

        private readonly string _filePath;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new version of <see cref="VersionFile"/>
        /// </summary>
        public VersionFile(string filePath)
        {
            _filePath = filePath;
        }

        #endregion

        /// <summary>
        /// Gets the version number as defined by the content of the file
        /// </summary>
        /// <returns>The version as defined in the file</returns>
        public Version GetVersion()
        {
            string fileContents = File.ReadAllText(_filePath);

            // Find the current version
            string currentVersion = FindCurrentVersion(fileContents, "[assembly: AssemblyFileVersion(\"", "\")]");

            if (string.IsNullOrWhiteSpace(currentVersion))
            {
                string message = $"The version number could not be determined from the file {_filePath}";
                throw new Exception(message);
            }
            return Version.Parse(currentVersion);
        }

        /// <summary>
        /// Sets the version number as defined in the version file
        /// </summary>
        /// <param name="newVersionNumber">The new version to be set</param>
        public void SetVersion(Version newVersionNumber)
        {
            string fileContents;
            if (!File.Exists(_filePath))
            {
                fileContents = BuildVersionFileContents(newVersionNumber);
            }
            else
            {
                fileContents = File.ReadAllText(_filePath);
                // Find the current version
                var currentVersion = FindCurrentVersion(fileContents, "[assembly: AssemblyFileVersion(\"", "\")]");
                fileContents = fileContents.Replace(currentVersion, newVersionNumber.ToString());
            }

            File.WriteAllText(_filePath, fileContents);
        }

        private string BuildVersionFileContents(Version versionNumber)
        {
            var fileContent = new StringBuilder();
            fileContent.AppendFormat("[assembly: System.Reflection.AssemblyVersion({0})]", versionNumber);
            fileContent.AppendLine();
            fileContent.AppendFormat("[assembly: System.Reflection.AssemblyFileVersion({0})]", versionNumber);
            fileContent.AppendLine();
            return fileContent.ToString();
        }

        private string FindCurrentVersion(string fileContents, string prefix, string suffix)
        {
            int startIndex = fileContents.IndexOf(prefix, System.StringComparison.Ordinal);
            int endIndex = fileContents.IndexOf(suffix, startIndex + 1, System.StringComparison.Ordinal);
            startIndex += prefix.Length;

            return fileContents.Substring(startIndex, endIndex - startIndex);
        }
    }
}
