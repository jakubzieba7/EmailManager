using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace EmailManager.Models.Repositories
{
    public static class KnownFolders
    {
        public static Guid Contacts = new Guid("{56784854-C6CB-462B-8169-88E350ACB882}");
        public static Guid Desktop = new Guid("{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}");
        public static Guid Documents = new Guid("{FDD39AD0-238F-46AF-ADB4-6C85480369C7}");
        public static Guid Downloads = new Guid("{374DE290-123F-4565-9164-39C4925E467B}");
        public static Guid Favorites = new Guid("{1777F761-68AD-4D8A-87BD-30B759FA33DD}");
        public static Guid Links = new Guid("{BFB9D5E0-C6A9-404C-B2B2-AE6DB6AF4968}");
        public static Guid Music = new Guid("{4BD8D571-6D19-48D3-BE97-422220080E43}");
        public static Guid Pictures = new Guid("{33E28130-4E1E-4676-835A-98395C3BC3BB}");
        public static Guid SavedGames = new Guid("{4C5C32FF-BB9D-43B0-B5B4-2D72E54EAAA4}");
        public static Guid SavedSearches = new Guid("{7D1D3A04-DEBB-4115-95CF-2F29DA2920DA}");
        public static Guid Videos = new Guid("{18989B1D-99B5-455B-841C-AB7C74E4DDFC}");

        static Dictionary<string, Guid> Map { get; } = new Dictionary<string, Guid> {
        { nameof(Contacts), Contacts },
        { nameof(Desktop), Desktop },
        { nameof(Documents), Documents },
        { nameof(Downloads), Downloads },
        { nameof(Favorites), Favorites },
        { nameof(Links), Links },
        { nameof(Music), Music },
        { nameof(Pictures), Pictures },
        { nameof(SavedGames), SavedGames },
        { nameof(SavedSearches), SavedSearches },
        { nameof(Videos), Videos },
        };

        public static string GetPath(string knownFolder, KnownFolderFlags flags = KnownFolderFlags.DontVerify, bool defaultUser = false) =>
            Map.TryGetValue(knownFolder, out var knownFolderId) ? GetPath(knownFolderId, flags, defaultUser) : ThrowUnknownFolder();

        public static string GetPath(Guid knownFolderId, KnownFolderFlags flags = KnownFolderFlags.DontVerify, bool defaultUser = false)
        {
            if (SHGetKnownFolderPath(knownFolderId, (uint)flags, new IntPtr(defaultUser ? -1 : 0), out var outPath) >= 0)
            {
                string path = Marshal.PtrToStringUni(outPath);
                Marshal.FreeCoTaskMem(outPath);
                return path;
            }
            return ThrowUnknownFolder();
        }

        //[DoesNotReturn]
        static string ThrowUnknownFolder() =>
            throw new NotSupportedException("Unable to retrieve the path for known folder. It may not be available on this system.");

        [DllImport("Shell32.dll")]
        private static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr ppszPath);
    }

}

