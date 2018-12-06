using System.IO;
using Microsoft.Win32;

namespace HeroLabExportToPdf.Services
{
    public interface IFileService<T> where T : FileDialog
    {
        /// <summary>
        /// Gets a <see cref="FileInfo"/> object for the selected file. If multiple files are selected, returns the first selected file.
        /// </summary>
        FileInfo File { get; }

        /// <summary>
        /// Gets or sets a filter string that specifies the file types and descriptions to display.
        /// </summary>
        string Filter { get; set; }

        /// <summary>
        ///  Gets or sets the initial directory displayed by the file dialog box.
        /// </summary>
        string InitialDirectory { get; set; }

        /// <summary>
        /// Gets or sets a string shown in the title bar of the file dialog.
        /// </summary> 
        string Title { get; set; }


        /// <summary>
        /// Determines the filename of the file what will be used.
        /// </summary>
        /// <returns><c>true</c> if a file is selected; otherwise <c>false</c>.</returns>
        bool DetermineFile();
    }
}
