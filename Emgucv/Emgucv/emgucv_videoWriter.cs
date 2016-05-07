using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

/*
namespace emgucv_video
{
    class emgucv_video
    {
        static void Main(string[] args)
        {
            String win1 = "Test Window"; //The name of the window
            CvInvoke.cvNamedWindow(win1); //Create the window using the specific name

            Capture file;
            Image<Bgr, Byte> frame;

            file = new Capture("competition_1_1_xvid.avi"); 
            //file = new Capture(); 

            VideoWriter vidoeOuput = new VideoWriter(@"output.avi",0, 30, 720, 480,  true);

            while (1 == 1)
            {
                frame = file.QueryFrame();
                CvInvoke.cvShowImage(win1, frame); //Show the image
                vidoeOuput.WriteFrame(frame);
                if (CvInvoke.cvWaitKey(0) == 27)
                    break;
            }
            vidoeOuput.Dispose();
                        
            CvInvoke.cvDestroyWindow(win1); //Destory the window
            
        }
    }
}
    */

