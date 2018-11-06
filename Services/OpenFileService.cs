using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Win32;

namespace HeroLabExportToPdf.Services
{
     


    /// <summary>
    /// Service to open files.
    /// </summary>
    public class OpenFileService : IOpenFileService {
        private readonly OpenFileDialog _openFileDialog;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileService"/> class.
        /// </summary>
        public OpenFileService() {
            _openFileDialog = new OpenFileDialog {CheckFileExists = true, CheckPathExists = true};


        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a <see cref="T:System.IO.FileInfo" /> object for the selected file. If multiple files are selected, returns the first selected file.
        /// </summary>
        public FileInfo File => new FileInfo(_openFileDialog.FileName);

        /// <inheritdoc />
        /// <summary>
        /// Gets a collection of <see cref="T:System.IO.FileInfo" /> objects for the selected files.
        /// </summary>
        public IEnumerable<FileInfo> Files {
            get {

                return _openFileDialog.FileNames.Select(name => new FileInfo(name));

            }
        }

        /// <summary>
        /// Gets or sets a filter string that specifies the file types and descriptions to display.
        /// </summary>
        public string Filter {
            get => _openFileDialog.Filter;
            set => _openFileDialog.Filter = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is allows to select multiple files.
        /// </summary>
        public bool Multiselect {
            get => _openFileDialog.Multiselect;
            set => _openFileDialog.Multiselect = value;
        }


        /// <summary>
        ///  Gets or sets the initial directory displayed by the file dialog box.
        /// </summary>
        public string InitialDirectory {
            get => _openFileDialog.InitialDirectory;
            set => _openFileDialog.InitialDirectory = value;
        } 

        /// <summary>
        /// Gets or sets a string shown in the title bar of the file dialog.
        /// </summary> 
        public string Title {
            get => _openFileDialog.Title;
            set => _openFileDialog.Title = value;
        }


        /// <summary>
        /// Determines the filename of the file what will be used.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if a file is selected; otherwise <c>false</c>.
        /// </returns>
        public bool DetermineFile() {
            return _openFileDialog.ShowDialog().GetValueOrDefault();
        }
    }
}
