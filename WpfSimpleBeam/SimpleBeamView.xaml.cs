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
    /// Logique d'interaction pour SimpleBeamView.xaml
    /// </summary>
    public partial class SimpleBeamView : Window
    {
        SimpleBeamComponents load;
        SimpleBeam beam;
        PointCollection pt;
        Point X0, X1, X2;

        Line dashedLine;
        Polygon Arrow, ShearForceLeft, ShearForceRight;
        Polygon BML;//, BMR;
        double SFLine = 0;
        //double beamY;
        double ForceDiagramY;
        double MomentDiagramY;

        double mX, mY;
        double dx = 0;
        double dy = 0;

        int top;
        int left;
        int h, w, n;

        double leftMargin;
        double rightMargin;

        Label lblLoad;

        public SimpleBeamView(SimpleBeam beam)
        {
            InitializeComponent();

            this.beam = beam;
            beamSketch();
        }

        void beamSketch()
        {
            load = new SimpleBeamComponents(h, w, top, left, h);
            //W = beam.NLoads[0];
            //left = 110;//
            //top = 50;//
            dial.Height = Height;
            dial.Width = Width;
            //path = new System.Windows.Shapes.Path[1];//[sundial.ha.size()];

            leftMargin = 30;
            rightMargin = 40;

            load.BeamY = 100; // Y ordinate
            ForceDiagramY = 260; // Y Force diagram ordinate
            MomentDiagramY = 420; // Y Moment diagram ordinate

            X0 = new Point(leftMargin, load.BeamY);
            X1 = new Point(leftMargin, ForceDiagramY);
            X2 = new Point(leftMargin, MomentDiagramY);

            // Coordinate axes
            Line Yline = new Line();
            Yline.Stroke = System.Windows.Media.Brushes.Black;// LightSteelBlue;
            Yline.StrokeThickness = 1;
            //RotateTransform R = new RotateTransform(-mlib.deg(alpha), Width / 2, Height / 2);
            Yline.X1 = leftMargin;
            Yline.Y1 = 0;
            Yline.X2 = leftMargin;
            Yline.Y2 = dial.Height;
            dial.Children.Add(Yline);

            Line Xline = new Line();
            Xline.Stroke = System.Windows.Media.Brushes.Black;
            Xline.StrokeThickness = 1;
            Xline.X1 = X0.X;
            Xline.Y1 = 100;
            Xline.X2 = X0.X + dial.Width - rightMargin;
            Xline.Y2 = 100;
            //Xline.RenderTransform = R;
            dial.Children.Add(Xline);

            Line Xline2 = new Line();
            Xline2.Stroke = System.Windows.Media.Brushes.Black;
            Xline2.StrokeThickness = 1;
            Xline2.X1 = X1.X;
            Xline2.Y1 = X1.Y;
            Xline2.X2 = X1.X + dial.Width - rightMargin;
            Xline2.Y2 = X1.Y;
            //Xline.RenderTransform = R;
            dial.Children.Add(Xline2);

            Line Xline3 = new Line();
            Xline3.Stroke = System.Windows.Media.Brushes.Black;
            Xline3.StrokeThickness = 1;
            Xline3.X1 = X2.X;
            Xline3.Y1 = X2.Y;
            Xline3.X2 = X2.X + dial.Width - rightMargin;
            Xline3.Y2 = X2.Y;
            //Xline.RenderTransform = R;
            dial.Children.Add(Xline3);

            // beam
            SolidColorBrush brownBrush = new SolidColorBrush();
            brownBrush.Color = Colors.Brown;

            PointCollection BC = new PointCollection();
            BC.Add(new Point(leftMargin, 90));
            BC.Add(new Point(dial.Width - rightMargin, 90));
            BC.Add(new Point(dial.Width - rightMargin, 110));
            BC.Add(new Point(leftMargin, 110));
            polygon(BC, brownBrush, 0.0);

            // Resistance left
            arrow(pt, leftMargin, load.BeamY, 0);

            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;

            // Resitance right
            pt = new PointCollection();
            arrow(pt, dial.Width - rightMargin, load.BeamY, 0);

            // Vertical dashed line 
            dashedLine = new Line();
            DoubleCollection dashes = new DoubleCollection();
            dashedLine.Stroke = System.Windows.Media.Brushes.Black;
            dashedLine.StrokeDashArray = dashes;// "1";// 1 0 1 "
            dashes.Add(2);
            dashes.Add(1);
            dashedLine.StrokeDashArray = dashes;
            dashedLine.StrokeThickness = 4;
            dashedLine.Stroke = Brushes.SteelBlue;
            dashedLine.X1 = 190;
            dashedLine.Y1 = 70;
            dashedLine.X2 = 190;
            dashedLine.Y2 = 140;
            dial.Children.Add(dashedLine);

            // Close application
            Button btnExit = new Button();
            btnExit.Width = 80;
            btnExit.Height = 20;
            btnExit.Margin = new Thickness(15, 15, 10, 0);
            btnExit.Name = "btnExit";// +column.ToString();
            btnExit.Content = "Exit";
            Canvas.SetLeft(btnExit, 380.0);
            Canvas.SetTop(btnExit, 520.0);

            dial.Children.Add(btnExit);

            // define rupture diagram change. 
            SFLine = dx + left;

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {

                // Shear force diagram
                ShearForceLeft = new Polygon();
                SolidColorBrush SFBrush = new SolidColorBrush();
                SFBrush.Color = Colors.LightGray;

                //left Shear force diagram
                ShearForceLeft.Points.Add(new Point(leftMargin, ForceDiagramY - 15 * 3)); // chang 50 with load proportionnal
                ShearForceLeft.Points.Add(new Point(SFLine, ForceDiagramY - 15 * 3));
                ShearForceLeft.Points.Add(new Point(SFLine, ForceDiagramY));
                ShearForceLeft.Points.Add(new Point(leftMargin, ForceDiagramY));
                polygon(ShearForceLeft.Points, SFBrush, 2);

                //right shear force diagram
                ShearForceRight = new Polygon();
                ShearForceRight.Points.Add(new Point(dx + left, ForceDiagramY)); // DO Y ORDINATE PROPORTIONNEL TO LOAD AND R2
                ShearForceRight.Points.Add(new Point(600 - rightMargin, ForceDiagramY));
                ShearForceRight.Points.Add(new Point(600 - rightMargin, ForceDiagramY + 5 * 3));
                ShearForceRight.Points.Add(new Point(left + dx, ForceDiagramY + 5 * 3));
                polygon(ShearForceRight.Points, SFBrush, 2);

                // Bending Moment diagram left
                BML = new Polygon();
                BML.Points.Add(new Point(leftMargin, MomentDiagramY));
                //BML.Points.Add(new Point(SFLine, MomentDiagramY - 15 * 3));//(mX * 15 / 3)));
                //BML.Points.Add(new Point(SFLine, MomentDiagramY));
                //polygon(BML.Points, SFBrush, 2);

                //BML = new Polygon();
                //BML.Points.Add(new Point(SFLine, MomentDiagramY));
                BML.Points.Add(new Point(SFLine, MomentDiagramY - 15 * 3));
                BML.Points.Add(new Point(600 - rightMargin, MomentDiagramY));
                polygon(BML.Points, SFBrush, 2);
            }
            //wpfLoad load = new wpfLoad(h, w, top, left, n);
            load = new SimpleBeamComponents(h, w, top, left, h);
            btnExit.Click += btnExit_Click;//new RoutedEventHandler(btnExit_Click);

        }

        public void Draw(Point m)
        {
            dial.Children.Clear();
            beamSketch();
            mX = m.X;
            mY = m.Y;

            //beamSketch();

            top = 20;// 200;
            left = (int)((beam.LoadPos * (Width - leftMargin - rightMargin) / beam.Length)) + (int)leftMargin;//100
            h = 60;
            w = 20;
            n = 7; // PointCollection

            //       wpfLoad load = new wpfLoad(h, w, top, left, n);
            //load = new SimpleBeamComponents(h, w, top, left, h);
            //load.Build(); // called by constructor 

            Arrow = new Polygon();
            Arrow = load.LoadArrow;// load.Arrow;


            // Set mouse position to drag
            Arrow.SetValue(Canvas.LeftProperty, dx);// + leftMargin);
            Arrow.SetValue(Canvas.TopProperty, dy);// - top - h - 10);

            labels();

            dial.Children.Add(Arrow);

            //Arrow.MouseLeftButtonDown += Canvas_MouseDown;
        }

        void labels()
        {
            lblLoad = new Label();
            lblLoad.Content = Convert.ToString(beam.Load) + "kg";
            lblLoad.Height = 25;
            lblLoad.Width = 145;
            Canvas.SetLeft(lblLoad, dx + lblLoad.Width);// - 5)// + left - leftMargin);// / 2 + leftMargin / 2);// - leftMargin * 2);// - leftMargin - 40); //10
            Canvas.SetTop(lblLoad, dy);
            //lblLoad.HorizontalContentAlignment = HorizontalAlignment.Center;
            dial.Children.Add(lblLoad);

            Label lblR1 = new Label();
            //double x = (dx + left - leftMargin) * beam.Length / (Width - leftMargin - rightMargin);//??
            double x = (dx + left - leftMargin) * beam.Length / (Width - leftMargin - rightMargin);//??
            double R = beam.Load * (1 - (x / beam.Length));
            lblR1.Content = string.Format("{0:f0}Kg", R);
            lblR1.Height = 25;
            lblR1.Width = 145;
            Canvas.SetLeft(lblR1, leftMargin);
            Canvas.SetTop(lblR1, dy + 150);
            //lblLoad.HorizontalContentAlignment = HorizontalAlignment.Center;
            dial.Children.Add(lblR1);

            Label lblR2 = new Label();
            double R2 = beam.Load - R;
            lblR2.Content = string.Format("{0:f0}Kg", R2);
            lblR2.Height = 25;
            lblR2.Width = 145;
            Canvas.SetLeft(lblR2, Width - rightMargin - leftMargin);
            Canvas.SetTop(lblR2, dy + 150);
            //lblLoad.HorizontalContentAlignment = HorizontalAlignment.Center;
            dial.Children.Add(lblR2);
        }

        new private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            load = new SimpleBeamComponents(h, w, top, left, h);
            this.Content = dial;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (mY > top && mY < top + h && mX >= leftMargin && mX <= Width - rightMargin)//dashedLine.X1
            {
                dx = mX - left;
                Draw(e.GetPosition(dial));
            }
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (mY > top - 20 && mY < top + h && mX >= leftMargin && mX <= Width - rightMargin)//dashedLine.X1) // test bounderies
                {
                    dx = mX - left;
                }
                Draw(e.GetPosition(dial));
            }
        }
        /*
        public void OnMouseRightButtonDown(object sender, MouseEventArgs e)
        {
            if (mY > top && mY < top + h + 10 && mX > leftMargin && mX < Width - rightMargin)//dashedLine.X1
            {
                dx = mX - left;
                Draw(e.GetPosition(dial));
            }
        }
        */
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("closing");
            this.Close();
        }

        void arrow(PointCollection pt, double leftMargin, double beamY, double thickness)
        {
            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;

            pt = new PointCollection();

            pt = new PointCollection();
            pt.Add(new Point(leftMargin, beamY + 10));
            pt.Add(new Point(leftMargin - 5, beamY + 30));
            pt.Add(new Point(leftMargin - 2, beamY + 30));
            pt.Add(new Point(leftMargin - 2, beamY + 60));
            pt.Add(new Point(leftMargin + 2, beamY + 60));
            pt.Add(new Point(leftMargin + 2, beamY + 30));
            pt.Add(new Point(leftMargin + 5, beamY + 30));
            polygon(pt, redBrush, thickness);
        }

        private void polygon(PointCollection p, SolidColorBrush col, double thickness)
        {
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Colors.Black;

            // Create a Polygon  
            Polygon brownPolygon = new Polygon();
            brownPolygon.Stroke = brush;
            brownPolygon.StrokeThickness = thickness;// .25f;//

            brownPolygon.Fill = col;

            brownPolygon.Points = p;

            // Add Polygon to the dial  
            dial.Children.Add(brownPolygon);
        }
    }
}