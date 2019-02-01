using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Caliburn.Micro;
using HeroLabExportToPdf.Entities;
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

        #region guard properties
        public bool CanExportTemplate => _characterSheet.PdfImage.Image != null;
        public bool CanImportTemplate => _characterSheet.PdfImage.Image != null;
        public bool CanSavePdf => _characterSheet.PdfImage.Image != null;

        public bool CanLoadPdf => true;
        public bool CanLoadImage => true;
        #endregion

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
            _openFileService.FileName = null;
            _openFileService.Multiselect = false;
            LoadFile();
        }

        public void LoadImage()
        {
            _openFileService.Filter = "Image Files (*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
            _openFileService.Multiselect = true;
            LoadFile();
        }

        private void LoadFile()
        {
            if (!_openFileService.DetermineFile()) return;

            if(_openFileService.File.Extension.ToLower().Equals(".pdf"))
                _pdfService.Init(_openFileService.File.FullName);
            else
                _pdfService.Init( _openFileService.Files
                    .OrderBy(f => f.Name)
                    .Select(f => f.OpenRead())
                    .ToArray()
                );
            _characterSheet.SetSheetPreview();
            NotifyOfPropertyChange(() => CanExportTemplate);
            NotifyOfPropertyChange(() => CanImportTemplate);
            NotifyOfPropertyChange(() => CanSavePdf);
            //_characterSheet.UpdateFormFields();
            ActivateItem(_characterSheet);
        }

        public void SavePdf()
        {
            foreach (var field in _characterSheet.FormFields)
            {
                _pdfService.AddField(field.X, field.Y, field.Width, field.Height, _characterSheet.PdfImage.Width, _characterSheet.PdfImage.Height, field.Text, field.Tooltip, field.FontSize, field.Page);
            }
            
            if(_saveFileService.DetermineFile())
                _pdfService.Save(_saveFileService.File.FullName);
        }


        

        public void ExportTemplate()
        {

            var doc = new XDocument();
            var serializer = new XmlSerializer(typeof(FormTemplate), new XmlRootAttribute("Character"));
            using (var writer = doc.CreateWriter())
            {
                var pageInfo = new PageSize{Height = _characterSheet.PdfImage.Height, Width = _characterSheet.PdfImage.Width};
                var formFields = _characterSheet.FormFields.Select(field =>
                    new FormField
                    {
                        Id = field.Id,
                        Height = field.Height,
                        Width = field.Width,
                        X = field.X,
                        Y = field.Y,
                        FontSize = field.FontSize,
                        Font = field.FontFamily.ToString(),
                        Page = field.Page,
                        Type = (int)field.Type
                    }
                );

                var formTemplate = new FormTemplate {FormFields = formFields.ToList(), PageSize = pageInfo};
                serializer.Serialize(writer, formTemplate);
            }

            _saveFileService.DefaultFileName = "template.tpl";
            _saveFileService.Filter = "Template File (*.tpl) | *.tpl";
            if(_saveFileService.DetermineFile())
            doc.Save(_saveFileService.File.FullName);
        }


        
        public void ImportTemplate()
        {
            _openFileService.Multiselect = false;
            _openFileService.FileName = null;
            _openFileService.Filter = "Template File (*.tpl) | *.tpl";
            if (!_openFileService.DetermineFile()) return;

            var deserializer = new XmlSerializer(typeof(FormTemplate), new XmlRootAttribute("Character"));
            using (var reader = XmlReader.Create(_openFileService.File.FullName))
            {
                var formTemplate = deserializer.Deserialize(reader) as FormTemplate;
                if (formTemplate?.FormFields == null) return;

                var scaleX = _characterSheet.PdfImage.Width / formTemplate.PageSize.Width;
                var scaleY = _characterSheet.PdfImage.Height / formTemplate.PageSize.Height;
                
                    foreach (var formField in formTemplate.FormFields)
                    {
                        formField.X *= scaleX;
                        formField.Y *= scaleY;
                        formField.Width *= scaleX;
                        formField.Height *= scaleY;

                        _pdfService.AddField(formField.X, formField.Y, formField.Width,
                            formField.Height, _characterSheet.PdfImage.Width, _characterSheet.PdfImage.Height,
                            null, null, formField.FontSize, formField.Page);
                    }
                _characterSheet.UpdateFormFields(formTemplate.FormFields);
            }
        }
    }
}
