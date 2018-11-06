using Caliburn.Micro;
using HeroLabExportToPdf.Services;

namespace HeroLabExportToPdf.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// A simple example of a view-model.  
    /// </summary>
    public class MainViewModel : Conductor<object>
    {

        private readonly IOpenFileService _openFileService;

        public MainViewModel(IOpenFileService openFileService)
        {
            _openFileService = openFileService;
        }

        public MainViewModel()
        {
            
        }

        public void LoadPdf()
        {



            var fileChosen = _openFileService.DetermineFile();
            if (fileChosen)
            {
                ActivateItem(new CharacterSheetViewModel(_openFileService.File.FullName));
            }
            
        }
    }
}
