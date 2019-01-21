using System.IO;
using Microsoft.Win32;

namespace HeroLabExportToPdf.Services
{
    public class BaseFileService<T> : IFileService<T> where T : FileDialog, new()
    {

        protected readonly T FileDialog;

        public BaseFileService()
        {
            FileDialog = new T(){CheckFileExists = true, CheckPathExists = true};
        }

        protected BaseFileService(T fileDialog)
        {
            FileDialog = fileDialog;
        }

        public FileInfo File => new FileInfo(FileDialog.FileName);
        public string Filter  {
            get => FileDialog.Filter;
            set => FileDialog.Filter = value;
        }
        public string InitialDirectory {
            get => FileDialog.InitialDirectory;
            set => FileDialog.InitialDirectory = value;
        } 
        public string Title {
            get => FileDialog.Title;
            set => FileDialog.Title = value;
        }
        public bool DetermineFile()
        {
            return FileDialog.ShowDialog().GetValueOrDefault();
        }
    }
}
