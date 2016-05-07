using System;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;

public class RGBHistogram
{
    public float[] rBins;
    public float[] gBins;
    public float[] bBins;
    public int totalPixels;

    public RGBHistogram() { }

    public RGBHistogram(int rStep, int gStep, int bStep) {
        rBins = new float[rStep];
        gBins = new float[gStep];
        bBins = new float[bStep];
    }
}

class ColorHistogramUtility
{
    int rStep, gStep, bStep;

    public ColorHistogramUtility(int numBins)
    {
        rStep = gStep = bStep = numBins;
    }

    // Generates similiarity score for a given query and test histogram.
    public float CompareHistograms(RGBHistogram queryHist, RGBHistogram otherHist, int rStep, int gStep, int bStep)
    {
        float fSum = 0;
        for (int i = 0; i < rStep; i++)
        {
            fSum += Math.Min(queryHist.rBins[i] / queryHist.totalPixels, otherHist.rBins[i] / otherHist.totalPixels);
        }
        for (int i = 0; i < gStep; i++)
        {
            fSum += Math.Min(queryHist.rBins[i] / queryHist.totalPixels, otherHist.rBins[i] / otherHist.totalPixels);
        }
        for (int i = 0; i < bStep; i++)
        {
            fSum += Math.Min(queryHist.rBins[i] / queryHist.totalPixels, otherHist.rBins[i] / otherHist.totalPixels);
        }
        return fSum / otherHist.totalPixels;
    }

    public float GetHistogramDifference(RGBHistogram h1, RGBHistogram h2)
    {
        float diff = 0;
        for (int i = 0; i < rStep; i++)
        {
            diff += Math.Abs(h1.rBins[i] - h2.rBins[i]);
        }
        for (int i = 0; i < gStep; i++)
        {
            diff += Math.Abs(h1.rBins[i] - h2.rBins[i]);
        }
        for (int i = 0; i < bStep; i++)
        {
            diff += Math.Abs(h1.rBins[i] - h2.rBins[i]);
        }
        return diff;
    }

    public RGBHistogram GetColourHistogram(Image<Bgr, Byte> img)
    {
        return GetColourHistogram(img, rStep, gStep, bStep);
    }

    // calculate color histogram for a given image img by uniformly quantizing R, G, and B
    // components into rStep, gStep, and bStep levels, respectively.
    public RGBHistogram GetColourHistogram(Image<Bgr, Byte> img, int rStep, int gStep, int bStep)
    {
        float[] rBins, gBins, bBins;
        rBins = new float[rStep];
        gBins = new float[gStep];
        bBins = new float[bStep];
        double rBoundarySize = 265 / rStep;
        double gBoundarySize = 265 / gStep;
        double bBoundarySize = 265 / bStep;

        for (int x = 0; x < img.Height; x++)
        {
            for (int y = 0; y < img.Width; y++)
            {
                Bgr currPixel = img[x, y];

                // Get the blue bin this pixel belongs in
                int bBin = (int)currPixel.Blue / (int)bBoundarySize;
                int gBin = (int)currPixel.Green / (int)gBoundarySize;
                int rBin = (int)currPixel.Red / (int)rBoundarySize;

                rBins[rBin]++;
                gBins[gBin]++;
                bBins[bBin]++;
            }
        }
        RGBHistogram rgbH = new RGBHistogram();
        rgbH.rBins = rBins;
        rgbH.bBins = bBins;
        rgbH.gBins = gBins;
        rgbH.totalPixels = img.Height * img.Width;
        return rgbH;
    }

    public void Run()
    {
        string dir = Environment.CurrentDirectory + "/skiing/";
        Console.WriteLine("QUERY DIRECTORY - " + dir);
        Console.WriteLine("Enter an input query JPG file:");
        string queryFile = Console.ReadLine() + ".jpg";

        if (!File.Exists(dir + queryFile))
        {
            Console.WriteLine("File was not found at " + dir + queryFile);
            Console.Read();
        }

        Image<Bgr, Byte> queryImg = new Image<Bgr, Byte>(dir + queryFile);

        ColorHistogramUtility hist = new ColorHistogramUtility(6);

        RGBHistogram queryHist = hist.GetColourHistogram(queryImg, rStep, gStep, bStep);
        RGBHistogram otherHist;
        float bestSim = 0;
        string bestMatchFilename = "";
        Image<Bgr, Byte> bestMatchImg = null;
        Image<Bgr, Byte> otherImg;
        string[] otherFiles = Directory.GetFiles(dir);
        foreach (string s in otherFiles)
        {
            if (s.Equals(dir + queryFile))
            {
                Console.WriteLine("Skipping self.");
                continue;
            }
            else
            {
                otherImg = new Image<Bgr, Byte>(s);
                otherHist = hist.GetColourHistogram(otherImg, rStep, gStep, bStep);
                float sim = hist.CompareHistograms(queryHist, otherHist, rStep, gStep, bStep);
                if (sim > bestSim)
                {
                    Console.WriteLine("Found better match: " + s + ": " + sim);
                    bestSim = sim;
                    bestMatchImg = otherImg;
                    bestMatchFilename = s;
                }
            }
        }

        /*
        for (int i = 0; i < queryHist.rBins.Length; i++)
        {
            Console.Write(queryHist.rBins[i] + " ");
        }
        Console.WriteLine("\n");
        for (int i = 0; i < queryHist.gBins.Length; i++)
        {
            Console.Write(queryHist.gBins[i] + " ");
        }
        Console.WriteLine("\n");
        for (int i = 0; i < queryHist.bBins.Length; i++)
        {
            Console.Write(queryHist.bBins[i] + " ");
        }
        Console.WriteLine("\n");

        */
        // Display the original query
        String winQuery = "Query image: " + queryFile; //The name of the window
        CvInvoke.NamedWindow(winQuery); //Create the window using the specific name
        CvInvoke.Imshow(winQuery, queryImg); //Show the image   

        // ======== display an image ========
        String winMatch = "Best Match: " + bestMatchFilename; //The name of the window
        CvInvoke.NamedWindow(winMatch); //Create the window using the specific name
        CvInvoke.Imshow(winMatch, bestMatchImg); //Show the image   
        CvInvoke.WaitKey(0);

        CvInvoke.DestroyWindow(winMatch); //Destroy the window
        CvInvoke.DestroyWindow(winQuery); //Destroy the window

    }
}
