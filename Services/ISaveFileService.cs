using Microsoft.Win32;

namespace HeroLabExportToPdf.Services
{
    public interface ISaveFileService : IFileService<FileDialog>
    {
        string DefaultFileName { get; set; }
    }
}
