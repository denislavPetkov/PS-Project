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

using System.Drawing;
using System.Reflection;
using System.Data.Entity;

using WindowsFormsApp1;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
            this.SizeChanged += Window_SizeChanged;
            this.LocationChanged += Window_LocationChanged;

            this.DataContext = new OptionsViewModel(new OptionContext());
        }

        private void OptionsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewmodel = (OptionsViewModel)this.DataContext;
            viewmodel.SelectedOption = optionsListView.SelectedItem as Option;
            viewmodel.OpenWindow();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetInitialPos();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RealodPos();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            RealodPos();
        }

        void SetInitialPos()
        {
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Left = 0;
            this.Top = System.Windows.SystemParameters.WorkArea.Bottom - this.Height;
        }

        void RealodPos()
        {
            if (this.OwnedWindows.Count == 0)
            {
                return;
            }

            Window openedWindow = this.OwnedWindows[0];
            openedWindow.Left = this.Width + this.Left - 20;
            openedWindow.Width = System.Windows.SystemParameters.WorkArea.Right - this.Width;

            openedWindow.Top = this.Top;
            openedWindow.Height = this.Height;
        }
    }


    }
