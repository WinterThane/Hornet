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
        private DBConnect db = new DBConnect();

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

            var selected = ((ComboBoxItem)DBselectComboBox.SelectedItem).Content.ToString();

            switch (selected)
            {
                case "Swiss materials":
                    MessageBox.Show("Under construction.");
                    break;
                    
                case "Spanish materials":
                    List<SpanishMaterial> spanishData;
                    spanishData = db.SelectSpanishMaterials("");
                    MainDataGrid.ItemsSource = spanishData;

                    List<string> groups = new List<string>();
                    groups = db.SelectSpanishMaterialsGroups();
                    GroupComboBox.ItemsSource = groups;
                    GroupComboBox.IsEnabled = true;
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

            var selected = GroupComboBox.SelectedValue.ToString();
            List<SpanishMaterial> spanishData;
            spanishData = db.SelectSpanishMaterials(selected);
            MainDataGrid.ItemsSource = spanishData;
        }
    }
}
