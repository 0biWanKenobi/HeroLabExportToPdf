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
        private readonly OpenFileDialog _openFileDialog;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileService"/> class.
        /// </summary>
        public OpenFileService() {

            _openFileDialog = new OpenFileDialog {CheckFileExists = true, CheckPathExists = true};


        }
        
        /// <summary>
        /// Gets a collection of <see cref="T:System.IO.FileInfo" /> objects for the selected files.
        /// </summary>
        public IEnumerable<FileInfo> Files {
            get {

                return _openFileDialog.FileNames.Select(name => new FileInfo(name));

            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is allows to select multiple files.
        /// </summary>
        public bool Multiselect {
            get => _openFileDialog.Multiselect;
            set => _openFileDialog.Multiselect = value;
        }


    }
}
