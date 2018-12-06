using Microsoft.Win32;

namespace HeroLabExportToPdf.Services
{
    public class SaveFileService : BaseFileService<SaveFileDialog>, ISaveFileService
    {

        public SaveFileService() : base (new SaveFileDialog{CheckFileExists = true, CheckPathExists = true, ValidateNames = false})
        {}

        public string DefaultFileName
        {
            get => _fileDialog.FileName;
            set => _fileDialog.FileName = value;
        }
    }
}
