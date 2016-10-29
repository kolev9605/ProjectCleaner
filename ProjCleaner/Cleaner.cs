namespace ProjCleaner
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class Cleaner
    {
        private readonly Dictionary<StrategyType, string[]> foldersAssociatedWithTypes;

        private IList<DirectoryInfo> deletedFolders;
        private ISet<string> foldersToDelete;

        private int foldersDeleted;
        private double sizeBefore;
        private double sizeAfter;

        public Cleaner(string path)
        {
            this.foldersAssociatedWithTypes = new Dictionary<StrategyType, string[]>();
            this.Path = path;
            this.deletedFolders = new List<DirectoryInfo>();
            this.foldersToDelete = new HashSet<string>();

            this.InitDictionary();
        }

        public Cleaner(string path, params StrategyType[] cleaningStrategies)
            : this(path)
        {
            foreach (StrategyType cleaningStrategy in cleaningStrategies)
            {
                foreach (var folderName in this.foldersAssociatedWithTypes[cleaningStrategy])
                {
                    this.foldersToDelete.Add(folderName);
                }
            }
        }

        public string Path { get; private set; }

        public void Clean()
        {
            this.sizeBefore = this.GetDirectorySize(this.Path);
            this.Traverse(this.Path);
            this.sizeAfter = this.GetDirectorySize(this.Path);
        }

        public string Statistics()
        {
            return this.Log();
        }

        private string Log()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(
                    $"Disk capacity saved: {this.sizeBefore - this.sizeAfter}Mb (before: {this.sizeBefore}Mb, after: {this.sizeAfter}Mb)")
                .Append($"{this.foldersDeleted} folders deleted.");

            return builder.ToString();
        }

        private void Traverse(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            foreach (DirectoryInfo di in dirInfo.GetDirectories())
            {
                this.Traverse(di.FullName);

                var folderName = di.Name;
                if (this.ShouldDeleteFolder(folderName))
                {
                    this.deletedFolders.Add(di);
                    this.foldersDeleted++;
                    di.Delete(true);
                }
            }
        }

        private void InitDictionary()
        {
            this.foldersAssociatedWithTypes[StrategyType.VisualStudio] = new string[] { "bin", "obj" };
            this.foldersAssociatedWithTypes[StrategyType.JetBrains] = new string[] { ".idea" };
        }

        private bool ShouldDeleteFolder(string folderName)
        {
            foreach (var pair in this.foldersAssociatedWithTypes)
            {
                if (pair.Value.Contains(folderName))
                {
                    return true;
                }
            }

            return false;
        }

        private double GetDirectorySize(string path)
        {
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            long sizeInBytes = 0;
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                sizeInBytes += fileInfo.Length;
            }

            double sizeInMegabytes = (double)sizeInBytes / 1024 / 1024;

            return Math.Truncate(sizeInMegabytes * 100) / 100;
        }
    }
}