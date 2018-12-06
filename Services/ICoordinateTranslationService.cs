namespace HeroLabExportToPdf.Services
{
    public interface ICoordinateTranslationService
    {
        void Translate<TViewModel>(TViewModel viewModel, string childElementName, double origX,
            double origY, out double x, out double y);
    }
}
