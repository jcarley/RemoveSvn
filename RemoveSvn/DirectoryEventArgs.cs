using System;
using System.IO;

namespace RemoveSvn
{
    public class DirectoryEventArgs : EventArgs
    {
        public DirectoryInfo Directory { get; set; }
    }
}
