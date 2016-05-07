using System;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ConsoleApplication1
{
    public class RGBHistogram
    {
        public double[] rBins;
        public double[] gBins;
        public double[] bBins;

        public RGBHistogram() { }

        public RGBHistogram(int rStep, int gStep, int bStep) {
            rBins = new double[rStep];
            gBins = new double[gStep];
            bBins = new double[bStep];
        }
    }

    class lab05_ColorHistogram
    {
        static void Main(string[] args)
        {
            Image<Bgr, Byte> frame = new Image<Bgr, Byte>("lena.jpg");

            int rStep, gStep, bStep;
            rStep = gStep = bStep = 10;
            RGBHistogram hist = colorHistogram(frame, rStep, gStep, bStep);
			
            for (int i = 0; i < hist.rBins.Length; i++)
            {
                Console.Write(hist.rBins[i] + " ");
            }
            Console.WriteLine("\n");
            for (int i = 0; i < hist.gBins.Length; i++)
            {
                Console.Write(hist.gBins[i] + " ");
            }
            Console.WriteLine("\n");
            for (int i = 0; i < hist.bBins.Length; i++)
            {
                Console.Write(hist.bBins[i] + " ");
            }
            Console.WriteLine("\n");

            string path = Environment.CurrentDirectory;
            Console.WriteLine("Curr dir: " + path);
            string[] files = Directory.GetFiles(path);
            foreach (string f in files)
            {

            }

            // ======== display an image ========
            String win1 = "Test Window"; //The name of the window
            CvInvoke.NamedWindow(win1); //Create the window using the specific name
            //CvInvoke.Imshow(win1, frame); //Show the image   
            CvInvoke.WaitKey(0);
            CvInvoke.DestroyWindow(win1); //Destory the window

        }

        RGBHistogram CompareHistograms(RGBHistogram h1, RGBHistogram h2, int rStep, int gStep, int bStep)
        {
            RGBHistogram result = new RGBHistogram(rStep, gStep, bStep);
            for (int i = 0; i < rStep; i++)
            {
                result.rBins[i] = Math.Min(h1.rBins[i], h2.rBins[i]);
            }
            for (int i = 0; i < gStep; i++)
            {
                result.gBins[i] = Math.Min(h1.gBins[i], h2.gBins[i]);
            }
            for (int i = 0; i < bStep; i++)
            {
                result.bBins[i] = Math.Min(h1.bBins[i], h2.bBins[i]);
            }
            return result;
        }
		
		// calculate color histogram for a given image img by uniformly quantizing R, G, and B
		// components into rStep, gStep, and bStep levels, respectively.
		static RGBHistogram colorHistogram(Image<Bgr, Byte> img, int rStep, int gStep, int bStep)
		{
            //double[] histogram = null;
            double[] rBins, gBins, bBins;
            rBins = new double[rStep];
            gBins = new double[gStep];
            bBins = new double[bStep];
            double rBoundarySize = 265 / rStep;
            double gBoundarySize = 265 / gStep;
            double bBoundarySize = 265 / bStep;

            for (int x = 0; x < img.Height; x++)
            {
                for (int y = 0; y < img.Width; y++)
                {
                    Bgr currPixel = img[x, y];

                    // Get the blue bin this pixel belongs in
                    int bBin = (int) currPixel.Blue / (int) bBoundarySize;
                    int gBin = (int) currPixel.Green / (int) gBoundarySize;
                    int rBin = (int) currPixel.Red / (int) rBoundarySize;

                    rBins[rBin]++;
                    gBins[gBin]++;
                    bBins[bBin]++;
                }
            }
            RGBHistogram rgbH = new RGBHistogram();
            rgbH.rBins = rBins;
            rgbH.bBins = bBins;
            rgbH.gBins = gBins;
            return rgbH;
            //return histogram;
		}
    }
}
