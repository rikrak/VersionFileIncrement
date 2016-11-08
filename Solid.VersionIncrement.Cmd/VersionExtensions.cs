using System;

namespace Solid.VersionIncrement.Cmd
{
    /// <summary>
    /// Extension methods for the <see cref="Version"/> class
    /// </summary>
    public static class VersionExtensions
    {
        /// <summary>
        /// Increments the revision number
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static Version IncrementRevisionNumber(this Version original)
        {
            var revision = original.Revision + 1;
            return new Version(original.Major, original.Minor, original.Build, revision);
        }

        /// <summary>
        /// Increments the Build number
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static Version IncrementBuildNumber(this Version original)
        {
            var build = original.Build + 1;
            return new Version(original.Major, original.Minor, build, original.Revision);
        }
    }
}
