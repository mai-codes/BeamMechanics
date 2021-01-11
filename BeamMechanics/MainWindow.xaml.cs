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

namespace WpfSimpleBeam
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string modelOfBeam;
        TextBox txtModel;

        public MainWindow()
        {
            InitializeComponent();

            modelOfBeamDefinition();
        }

        void modelOfBeamDefinition()
        {
            Canvas canvas = new Canvas();

            //Draw fields: type of beam
            Label beam = new Label();
            beam.Height = 25;
            beam.Width = 120;

            // Set Canvas position   
            Canvas.SetLeft(beam, 10);
            Canvas.SetTop(beam, 10);
            beam.Content = "Type of beam: ";

            txtModel = new TextBox();
            txtModel.Height = 20;
            txtModel.Width = 120;
            txtModel.Text = "simple beam";

            Canvas.SetLeft(txtModel, 140);
            Canvas.SetTop(txtModel, 10);
            canvas.Children.Add(txtModel);
            canvas.Children.Add(beam);

            // Button sketch beam model
            Button btnInstantiate = new Button();
            btnInstantiate.Height = 20;
            btnInstantiate.Width = 80;
            Canvas.SetLeft(btnInstantiate, 100);
            Canvas.SetTop(btnInstantiate, 300);
            btnInstantiate.Content = "Beam install";
            canvas.Children.Add(btnInstantiate);
            btnInstantiate.Click += new RoutedEventHandler(btnInstantiate_Click);

            // btn close
            Button btnClose = new Button();
            btnClose.Height = 20;
            btnClose.Width = 80;
            Canvas.SetLeft(btnClose, 300);
            Canvas.SetTop(btnClose, 300);
            btnClose.Content = "Close";
            canvas.Children.Add(btnClose);
            btnClose.Click += new RoutedEventHandler(btnClose_Click);

            // show the canvas and content
            this.Content = canvas;
        }

        private void btnInstantiate_Click(object sender, RoutedEventArgs e)
        {
            modelOfBeam = txtModel.Text;
            SimpleBeamControl beamInstall = new SimpleBeamControl(modelOfBeam);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
