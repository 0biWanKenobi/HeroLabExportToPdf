﻿using System;
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
        private readonly ISaveFileService _saveFileService;
        private readonly CharacterSheetViewModel _characterSheet;
        private readonly IPdfService _pdfService;

        public MainViewModel(IOpenFileService openFileService, ISaveFileService saveFileService, IPdfService pdfService, CharacterSheetViewModel characterSheet)
        {
            _openFileService = openFileService;
            _saveFileService = saveFileService;
            _pdfService = pdfService;
            _saveFileService.Filter = "Pdf File (*.pdf)|*.pdf";
            _saveFileService.Title = "Salva Scheda Personaggio";
            _saveFileService.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            _saveFileService.DefaultFileName = "output.pdf";
            _characterSheet = characterSheet;
        }

        public MainViewModel()
        {
            
        }

        public void LoadPdf()
        {
            _openFileService.Filter = "Pdf Files (*.pdf)|*.pdf";
            LoadFile();
        }

        public void LoadImage()
        {
            _openFileService.Filter = "Image Files (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
            LoadFile();
        }

        private void LoadFile()
        {
            _openFileService.Multiselect = false;
            var fileChosen = _openFileService.DetermineFile();
            if (fileChosen)
            {
                _pdfService.Init(_openFileService.File.FullName);
                _characterSheet.SetSheetPreview();
                ActivateItem(_characterSheet);
            }
        }

        public void Save()
        {
            foreach (var rectangle in _characterSheet.Rectangles)
            {
                _pdfService.AddField(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, _characterSheet.PdfImage.Width, _characterSheet.PdfImage.Height, rectangle.Text, rectangle.Tooltip, rectangle.FontSize, rectangle.Page);
            }
            
            if(_saveFileService.DetermineFile())
                _pdfService.Save(_saveFileService.File.FullName);
        }
    }
}
