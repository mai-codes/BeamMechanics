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
using System.Windows.Shapes;
using BeamMechanics;
namespace WpfSimpleBeam
{
    /// <summary>
    /// Logique d'interaction pour SimpleBeamControl.xaml
    /// </summary>
    public partial class SimpleBeamControl : Window
    {
        Canvas canvas;
        SimpleBeam beam;

        TextBox txtType;
        TextBox txtLength;
        TextBox txtLoads;
        TextBox txtLoadsPos;
        TextBox txtReaction;
        TextBox txtReactionPos;

        public SimpleBeamControl()
        {
            InitializeComponent();
        }

        public SimpleBeamControl(string model)
        {

            ModelInstall(model);
            this.Show();
        }

        void BeamInit()
        {
            beam = new SimpleBeam(txtType.Text, txtLength.Text, txtLoads.Text, txtLoadsPos.Text, txtReaction.Text, txtReactionPos.Text);
            //MessageBox.Show(Convert.ToString(beam.LoadsPos[0]));

            calculus();
        }

        void calculus()
        {
            for (int i = 0; i < beam.Reactions.Length; i++)
            //{
            //  if (i == 0)
            //    beam.Reactions[i] = beam.NLoads[i] * (1 - beam.LoadsPos[i]/ beam.Length);//Weights[i] * (1 - WInitPos[i] / BL);
            //else
            //  beam.Reactions[i] = beam.LoadsPos[i - 1] * beam.NLoads[i - 1] / beam.Length;// WInitPos[i - 1] * Weights[i - 1] / BL;
            //}            for (int i = 0; i < beam.Reactions.Length; i++)
            {
                if (i == 0)
                    beam.Reactions[i] = beam.Load * (1 - beam.LoadPos / beam.Length);//Weights[i] * (1 - WInitPos[i] / BL);
                else
                    beam.Reactions[i] = beam.LoadPos * beam.Load / beam.Length;// WInitPos[i - 1] * Weights[i - 1] / BL;
            }
        }

        void ModelInstall(string modelOfBeam)
        {
            // bloc labels separation
            //int hSep = 10;
            int vSep = 10;
            int lblHeight = 25;

            canvas = new Canvas();

            //Draw fields: beam type
            Label beam = new Label();
            beam.Height = 25;
            beam.Width = 145;

            // Set Canvas position   
            Canvas.SetLeft(beam, 10); //10
            Canvas.SetTop(beam, 10);
            beam.Content = modelOfBeam + " of kind?";// "Type of beam: ";
            beam.HorizontalContentAlignment = HorizontalAlignment.Right;
            canvas.Children.Add(beam);

            txtType = new TextBox();
            txtType.Height = 20;
            txtType.Width = 120;
            txtType.Text = "A1";

            Canvas.SetLeft(txtType, 160);
            Canvas.SetTop(txtType, 10);
            canvas.Children.Add(txtType);

            //Draw fields 2: length of beam
            Label beamLength = new Label();
            beamLength.Height = 25;
            beamLength.Width = 145;

            // Set Canvas position   
            Canvas.SetLeft(beamLength, 10);
            Canvas.SetTop(beamLength, lblHeight + vSep);//beam.Height + vSep);
            beamLength.Content = "Length of beam: ";
            beamLength.HorizontalContentAlignment = HorizontalAlignment.Right;

            txtLength = new TextBox();
            txtLength.Height = 20;
            txtLength.Width = 120;
            txtLength.Text = "120";

            Canvas.SetLeft(txtLength, 160);
            Canvas.SetTop(txtLength, lblHeight + vSep);//beam.Height + vSep);
            canvas.Children.Add(txtLength);
            canvas.Children.Add(beamLength);

            //Draw fields 3: number of loads
            Label loads = new Label();
            loads.Height = 25;
            loads.Width = 145;

            // Set Canvas position   
            Canvas.SetLeft(loads, 10);
            Canvas.SetTop(loads, lblHeight * 2 + vSep);//beam.Height + beamLength.Height + vSep);
            loads.Content = "Number of loads: ";
            loads.HorizontalContentAlignment = HorizontalAlignment.Right;

            txtLoads = new TextBox();
            txtLoads.Height = 20;
            txtLoads.Width = 120;
            txtLoads.Text = "2000";// "120";

            Canvas.SetLeft(txtLoads, 160);
            Canvas.SetTop(txtLoads, lblHeight * 2 + vSep);//beam.Height + beamLength.Height + vSep);
            canvas.Children.Add(txtLoads);
            canvas.Children.Add(loads);

            //Draw fields 4: loads position
            Label loadsPos = new Label();
            loadsPos.Height = 25;
            loadsPos.Width = 145;

            // Set Canvas position   
            Canvas.SetLeft(loadsPos, 10);
            Canvas.SetTop(loadsPos, lblHeight * 3 + vSep);//beam.Height + beamLength.Height + loads.Height + vSep);
            loadsPos.Content = "Loads Positions: ";
            loadsPos.HorizontalContentAlignment = HorizontalAlignment.Right;

            txtLoadsPos = new TextBox();
            txtLoadsPos.Height = 20;
            txtLoadsPos.Width = 120;
            txtLoadsPos.Text = "30";

            Canvas.SetLeft(txtLoadsPos, 160);
            Canvas.SetTop(txtLoadsPos, lblHeight * 3 + vSep);//beam.Height + beamLength.Height + loads.Height + vSep);
            canvas.Children.Add(txtLoadsPos);
            canvas.Children.Add(loadsPos);

            //Draw fields 5 /reactions
            Label reaction = new Label();
            reaction.Height = 25;
            reaction.Width = 145;

            // Set Canvas position   
            Canvas.SetLeft(reaction, 10);
            Canvas.SetTop(reaction, lblHeight * 4 + vSep);//beam.Height + beamLength.Height + loads.Height + loadsPos.Height + vSep);
            reaction.Content = "Reactions: ";
            reaction.HorizontalContentAlignment = HorizontalAlignment.Right;

            txtReaction = new TextBox();
            txtReaction.Height = 20;
            txtReaction.Width = 120;
            txtReaction.Text = "";

            Canvas.SetLeft(txtReaction, 160);
            Canvas.SetTop(txtReaction, lblHeight * 4 + vSep);//beam.Height + beamLength.Height + loads.Height + loadsPos.Height + vSep);
            canvas.Children.Add(txtReaction);
            canvas.Children.Add(reaction);

            //Draw fields 6 /reactions position
            Label reactionPos = new Label();
            reactionPos.Height = 25;
            reactionPos.Width = 145;

            // Set Canvas position   
            Canvas.SetLeft(reactionPos, 10);
            Canvas.SetTop(reactionPos, lblHeight * 5 + vSep);//beam.Height + beamLength.Height + loads.Height + reaction.Height + reactionPos.Height + vSep);
            reactionPos.Content = "Reactions Position: ";
            reactionPos.HorizontalContentAlignment = HorizontalAlignment.Right;

            txtReactionPos = new TextBox();
            txtReactionPos.Height = 20;
            txtReactionPos.Width = 120;
            txtReactionPos.Text = "0 120";

            Canvas.SetLeft(txtReactionPos, 160);
            Canvas.SetTop(txtReactionPos, lblHeight * 5 + vSep);//beam.Height + beamLength.Height + loads.Height + loadsPos.Height + reaction.Height + vSep);
            canvas.Children.Add(txtReactionPos);
            canvas.Children.Add(reactionPos);

            // Button sketch beam model
            Button btnSketch = new Button();
            btnSketch.Height = 20;
            btnSketch.Width = 80;
            Canvas.SetLeft(btnSketch, 100);
            Canvas.SetTop(btnSketch, 300);
            btnSketch.Content = "Sketch";
            canvas.Children.Add(btnSketch);

            btnSketch.Click += new RoutedEventHandler(btnSketch_Click);

            // btn close
            Button btnClose = new Button();
            btnClose.Height = 20;
            btnClose.Width = 80;
            Canvas.SetLeft(btnClose, 300);
            Canvas.SetTop(btnClose, 300);
            btnClose.Content = "Close";
            canvas.Children.Add(btnClose);

            btnClose.Click += new RoutedEventHandler(btnClose_Click);

            this.Content = canvas;
        }

        private void btnSketch_Click(object sender, RoutedEventArgs e)
        {

            BeamInit();

            SimpleBeamView draw = new SimpleBeamView(beam);
            draw.Show();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}