using System.IO;
using Microsoft.Win32;

namespace HeroLabExportToPdf.Services
{
    public class BaseFileService<T> : IFileService<T> where T : FileDialog, new()
    {

        protected readonly T _fileDialog;

        public BaseFileService()
        {
            _fileDialog = new T(){CheckFileExists = true, CheckPathExists = true};
        }

        protected BaseFileService(T fileDialog)
        {
            _fileDialog = fileDialog;
        }

        public FileInfo File => new FileInfo(_fileDialog.FileName);
        public string Filter  {
            get => _fileDialog.Filter;
            set => _fileDialog.Filter = value;
        }
        public string InitialDirectory {
            get => _fileDialog.InitialDirectory;
            set => _fileDialog.InitialDirectory = value;
        } 
        public string Title {
            get => _fileDialog.Title;
            set => _fileDialog.Title = value;
        }
        public bool DetermineFile()
        {
            return _fileDialog.ShowDialog().GetValueOrDefault();
        }
    }
}
