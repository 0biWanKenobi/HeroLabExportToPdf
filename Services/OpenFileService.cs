using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Win32;

namespace HeroLabExportToPdf.Services
{
     


    /// <inheritdoc />
    /// <summary>
    /// Service to open files.
    /// </summary>
    public class OpenFileService : BaseFileService<OpenFileDialog>, IOpenFileService {
        
        /// <summary>
        /// Gets a collection of <see cref="T:System.IO.FileInfo" /> objects for the selected files.
        /// </summary>
        public IEnumerable<FileInfo> Files {
            get {

                return FileDialog.FileNames.Select(name => new FileInfo(name));

            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is allows to select multiple files.
        /// </summary>
        public bool Multiselect {
            get => FileDialog.Multiselect;
            set => FileDialog.Multiselect = value;
        }

        public string FileName
        {
            get => FileDialog.FileName;
            set => FileDialog.FileName = value;
        }
    }
}
