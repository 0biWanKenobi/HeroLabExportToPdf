using System.Collections;
using System.Windows.Media.Imaging;

namespace HeroLabExportToPdf.Services
{
    public interface IPdfService
    {
        IEnumerable GetFields(double wpfW, double wpfH);
        BitmapImage GetImagePreview();

        void Init(string pdfFileName);

        void RemoveField(string fieldName);

        void AddField(double ulx, double uly, double width, double height, double winW, double winH,
            string text, string label, double fontSize);

        void Save(string fileName);
    }
}
