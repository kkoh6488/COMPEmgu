using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;

class VideoInverter
{
    public void WriteReverseInvertedFrame(string source, string outputPath)
    {
        Capture file = new Capture(source);
        Mat frame;
        VideoWriter videoOutput = new VideoWriter(outputPath, 30, new Size(480, 640), true);
        Stack<Mat> frameBuffer = new Stack<Mat>();
        while ((frame = file.QueryFrame()) != null)
        {
            Mat inverted = new Mat();
            CvInvoke.BitwiseNot(frame, inverted);
            frameBuffer.Push(inverted);
        }
        while (frameBuffer.Count > 0)
        {
            videoOutput.Write(frameBuffer.Pop());
        }
    }

    public void Run()
    {
        string dir = Environment.CurrentDirectory + "/video/";
        string filename = "competition_1_1_xvid.avi";
        VideoInverter vi = new VideoInverter();
        vi.WriteReverseInvertedFrame(dir + filename, dir + "output.avi");
    }
}


