using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;

namespace HeroLabExportToPdf.Services
{
    /// <summary>
    /// Interface for the Open File service.
    /// </summary>
    public interface IOpenFileService : IFileService<FileDialog>
    {
        

        /// <summary>
        /// Gets a collection of <see cref="FileInfo"/> objects for the selected files.
        /// </summary>
        IEnumerable<FileInfo> Files { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is allows to select multiple files.
        /// </summary>
        bool Multiselect { get; set; }
        
    }
}
