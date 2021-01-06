using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System.Threading.Tasks;

namespace BeamStrenght
{
    public class SimpleBeam
    {
        public string Type;
        public double Length;
        public double Load; // Concentrated loads
        public double LoadPos;
        public double[] Reactions;// not to count here
        public double[] ReactionsPos;

        // SimpleBeam positionning
        //public int ViewWidth;
        //public int ViewHeight;
        //public int ViewLeftMargin;

        //public double BeamY;
        //public int ShearForceY;
        //public int BendingMomentY;

        public SimpleBeam()
        {
            //this.ViewWidth = 600;
            //this.ViewHeight = 600;
            //this.ViewLeftMargin = 30;

            //this.BeamY = 100;
            //this.ShearForceY = 260;
            //this.BendingMomentY = 420;

        }

        public SimpleBeam(string type, string len, string load, string loadPos, string reac, string reactPos)
        {
            //ViewWidth = 700;
            //ViewHeight = 700;
            //ViewLeftMargin = 50;

            string[] s;

            this.Type = type;

            this.Length = Convert.ToDouble(len);

            // split parameters
            this.Load = new double();
            this.Load = Convert.ToDouble(load);

            this.LoadPos = new double();
            this.LoadPos = Convert.ToDouble(loadPos);

            s = reactPos.Split(' ');
            this.Reactions = new double[s.Length];

            for (int i = 0; i < s.Length; i++)
                this.Reactions[i] = Convert.ToDouble(s[i]);

            s = reactPos.Split(' ');
            this.ReactionsPos = new double[s.Length];

            for (int i = 0; i < s.Length; i++)
                this.ReactionsPos[i] = Convert.ToDouble(s[i]);
        }
    }
}
