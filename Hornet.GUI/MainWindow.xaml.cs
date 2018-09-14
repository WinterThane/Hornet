using Hornet.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hornet.GUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();          
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Do you want to exit?", "Hornet",
                                        MessageBoxButton.YesNo,
                                        MessageBoxImage.Question,
                                        MessageBoxResult.Yes);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                Application.Current.Shutdown();
            }
        }

        private void CharacterClassesCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectDBTextBlock.Visibility = DBselectComboBox.SelectedItem == null ? Visibility.Visible : Visibility.Hidden;

            var db = new DBConnect();
            List<SpanishMaterial> spanishData;
            spanishData = db.SelectSpanishMaterials();
            MainDataGrid.ItemsSource = spanishData;
        }

        private void MainDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //var db = new Data.DBConnect();
            //DataTable spanishData;
            //spanishData = db.SelectSpanishMaterials();
            
            //var grid = sender as DataGrid;
            //grid.ItemsSource = spanishData;
        }
    }
}
