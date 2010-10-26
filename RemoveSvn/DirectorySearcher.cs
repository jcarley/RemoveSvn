using System;
using System.IO;

namespace RemoveSvn
{
    public class DirectorySearcher
    {
        public string Pattern { get; set; }
        public string RootDirectory { get; set; }

        public event EventHandler<DirectoryEventArgs> Match;

        public void Search()
        {
            DirectoryInfo directory = new DirectoryInfo(RootDirectory);

            TraverseDirectory(directory);
        }

        private void TraverseDirectory(DirectoryInfo parent)
        {
            DirectoryInfo[] childDirectories = parent.GetDirectories(Pattern, SearchOption.AllDirectories);

            foreach (DirectoryInfo child in childDirectories)
            {
                OnMatch(new DirectoryEventArgs { Directory = child });
            }

        }

        private void OnMatch(DirectoryEventArgs args)
        {
            EventHandler<DirectoryEventArgs> handler = Match;

            if (handler != null)
            {
                handler(this, args);
            }
        }
    }
}
