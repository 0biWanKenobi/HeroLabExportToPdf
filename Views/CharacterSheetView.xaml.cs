using System.Windows;
using System.Windows.Controls;

namespace HeroLabExportToPdf.Views
{
    public partial class CharacterSheetView
    {
        public CharacterSheetView()
        {
            InitializeComponent();
        }

        private void OnFocusLostHandler(object sender, RoutedEventArgs e)
        {
            if (sender is TreeViewItem item) 
                item.IsSelected = false;
        }
    }
}
