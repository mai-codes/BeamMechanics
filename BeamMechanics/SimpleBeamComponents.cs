using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using BeamStrenght;

namespace WpfSimpleBeam
{ 
    public class SimpleBeamComponents : SimpleBeam
    {       
        public Polygon LoadArrow, R1Arrow, R2Arrow; //ConcentratedLoad = former Arrow

        int height, width;
        int top, left;
        int n;

        public double BeamY;
        public int ShearForceY;
        public int BendingMomentY;

        public SimpleBeamComponents()
        {
            //Build();
        }

        public SimpleBeamComponents(int H, int W, int T, int L, int N)
        {
            this.height = H;//100;
            this.width = W;// = 200;
            this.top = T;// = 20;
            this.left = L;//60;
            this.n = N;// Number of points = 7 here 

            this.BeamY = 100;
            this.ShearForceY = 260;
            this.BendingMomentY = 420;

            Builder();
        }

        public void Builder()
        {
            //Add the Polygon Element
            this.LoadArrow = new Polygon();

            this.LoadArrow.Fill = System.Windows.Media.Brushes.DarkBlue;// LightSeaGreen;

            this.LoadArrow.Points.Add(new Point(left - 3, top));
            this.LoadArrow.Points.Add(new Point(left - 3, top + height - 12));
            this.LoadArrow.Points.Add(new Point(left - 10, top + height - 12));
            this.LoadArrow.Points.Add(new Point(left, top + height + 10));
            this.LoadArrow.Points.Add(new Point(left + 10, top + height - 12));
            this.LoadArrow.Points.Add(new Point(left + 3, top + height - 12));
            this.LoadArrow.Points.Add(new Point(left + 3, top));
        }
    }
}
