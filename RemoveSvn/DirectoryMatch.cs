using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace RemoveSvn
{
    public class DirectoryMatch
    {
        public DirectoryInfo Directory { get; set; }

        public DirectoryMatch(DirectoryInfo directory)
        {
            Directory = directory;
        }

        public void Remove()
        {
            FileSystem.DeleteDirectory(Directory.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
        }

        public override string ToString()
        {
            return Directory.FullName;
        }
    }
}
