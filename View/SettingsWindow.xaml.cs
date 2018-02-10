using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public ICommand SaveCommand { get; }


        public SettingsWindow()
        {
            SaveCommand = new Prism.Commands.DelegateCommand<bool?>(Save);

            InitializeComponent();
        }

        private void ChooseAppPath(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog { Filter = "Exe file (*.exe)|*.exe" };
            if (fileDialog.ShowDialog() == true)
            {
                AppPath.Text = fileDialog.InitialDirectory + fileDialog.FileName;
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save(bool? result)
        {
            if (result.Value)
            {
                var res = MessageBox.Show(this, "All chanhes saved. Close window?", "Operation complete", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                if (res == MessageBoxResult.Yes)
                {
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Problem", $"Something wrong.. {result}", MessageBoxButton.OK);
                MessageBox.Show($"Result {result}");
            }
        }
    }
}
