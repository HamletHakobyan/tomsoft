// Guids.cs
// MUST match guids.h
using System;

namespace ThomasLevesque.PasteBinExtension
{
    static class GuidList
    {
        public const string GuidPasteBinExtensionPkgString = "ad0949ff-a16f-4f56-8685-bbd089f64b6f";
        public const string GuidPasteBinExtensionCmdSetString = "1946e753-e633-4ea9-8e4d-63740efe3cfd";

        public static readonly Guid GuidPasteBinExtensionCmdSet = new Guid(GuidPasteBinExtensionCmdSetString);
    }
}