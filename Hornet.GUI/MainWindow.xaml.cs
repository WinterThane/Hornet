using Hornet.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
        private DBConnect db = new DBConnect();
        private string selectedGroup = "";
        private Brush defaultButtonColor;

        public MainWindow()
        {
            InitializeComponent();
            defaultButtonColor = CO2filter.Background;
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

            var selected = ((ComboBoxItem)DBselectComboBox.SelectedItem).Content.ToString();

            switch (selected)
            {
                case "Swiss materials":
                    SpanishDataGrid.Visibility = Visibility.Hidden;
                    SwissDataTabControl.Visibility = Visibility.Visible;                 
                    break;
                    
                case "Spanish materials":
                    List<SpanishMaterial> spanishData;
                    spanishData = db.SelectSpanishMaterials("", true);
                    SpanishDataGrid.ItemsSource = spanishData;
                    SwissDataTabControl.Visibility = Visibility.Hidden;
                    SpanishDataGrid.Visibility = Visibility.Visible;

                    List<string> groups = new List<string>();
                    groups = db.SelectSpanishMaterialsGroups();
                    GroupComboBox.ItemsSource = groups;
                    GroupComboBox.IsEnabled = true;
                    SelectGroupTextBlock.Foreground = new SolidColorBrush(Colors.Black);
                    CO2filter.IsEnabled = true;
                    CO2filter.Background = defaultButtonColor;
                    break;

                case "German materilas":
                    MessageBox.Show("Under construction.");
                    break;

                case "Alex's materilas":
                    MessageBox.Show("Under construction.");
                    break;

                case "Alina's materilas":
                    MessageBox.Show("Under construction.");
                    break;
            }
        }

        private void GroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectGroupTextBlock.Visibility = GroupComboBox.SelectedItem == null ? Visibility.Visible : Visibility.Hidden;

            selectedGroup = GroupComboBox.SelectedValue.ToString();
            List<SpanishMaterial> spanishData;
            spanishData = db.SelectSpanishMaterials(selectedGroup, true);
            SpanishDataGrid.ItemsSource = spanishData;
            CO2filter.Background = defaultButtonColor;
        }

        private void CO2filter_Click(object sender, RoutedEventArgs e)
        {
            if(CO2filter.Background == defaultButtonColor)
            {
                List<SpanishMaterial> spanishData;
                spanishData = db.SelectSpanishMaterials(selectedGroup, false);
                SpanishDataGrid.ItemsSource = spanishData;
                CO2filter.Background = Brushes.LightGreen;
            }
            else
            {
                List<SpanishMaterial> spanishData;
                spanishData = db.SelectSpanishMaterials(selectedGroup, true);
                SpanishDataGrid.ItemsSource = spanishData;
                CO2filter.Background = defaultButtonColor;
            }

            
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
