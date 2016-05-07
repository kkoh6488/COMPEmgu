using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class HistogramShotDetector
{

    public void Run()
    {
        string dir = Environment.CurrentDirectory + "/shotdetection/";
        string[] filenames = {
            //"abc.avi",                    // Threshold = 70,000
            "shot_detection.mp4",           // Threshold = 100,000
        };

        // Create a colour histogram generator with 4 bins per channel
        ColorHistogramUtility ch = new ColorHistogramUtility(4);

        foreach (string s in filenames)
        {
            GenerateShots(dir + s, ch);
        }
    }

    void GenerateShots(string filepath, ColorHistogramUtility ch)
    {
        Capture file = new Capture(filepath);
        Mat frame;
        RGBHistogram prevHist = null;
        float threshold = 200000;
        int frameId = 0;
        string filename = filepath.Substring(0, filepath.Length - 4);
        if (!Directory.Exists(filename + "/shots/"))
        {
            Directory.CreateDirectory(filename + "/shots/");
        }
        else
        {
            DirectoryInfo di = new DirectoryInfo(filename + "/shots/");
            foreach (FileInfo f in di.GetFiles())
            {
                f.Delete();
            }
        }
        while ((frame = file.QueryFrame()) != null)
        {
            RGBHistogram currHist = ch.GetColourHistogram(frame.ToImage<Bgr, Byte>());
            if (prevHist != null)
            {
                //Console.WriteLine("Comparing frame " + frameId + " and " + (frameId - 1));
                float diff = ch.GetHistogramDifference(currHist, prevHist);
                if (diff > threshold)
                {
                    Console.WriteLine("Detected shot at frame " + frameId + ". Difference was " + diff);
                    frame.Save(filename + "/shots/" + frameId + ".jpg");
                }
            }
            frameId++;
            prevHist = currHist;
            currHist = null;
            if (frameId % 200 == 0)
            {
                GC.Collect();
            } 
        }
        GC.Collect();
        Console.ReadKey();
    }
}
