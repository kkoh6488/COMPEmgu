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
namespace emgucv_image
{
    class emgucv_image
    {
        static void Main(string[] args)
        {
            String win1 = "Test Window"; //The name of the window
            CvInvoke.cvNamedWindow(win1); //Create the window using the specific name

            Image<Bgr, Byte> frame = new Image<Bgr, Byte>("lena.jpg");

            CvInvoke.cvShowImage(win1, frame ); //Show the image   

            CvInvoke.cvWaitKey(0); 
                                    
            CvInvoke.cvDestroyWindow(win1); //Destory the window
            
			
			byte[, ,] data = frame.Data;


            //Note: access the raw image data 
            //http://www.emgu.com/wiki/index.php/Working_with_Images#Getting_or_Setting_Pixels
            for (int i = 0; i < frame.Rows; i++)
            {
                for (int j = 0; j < frame.Cols; j++)
                {
                    //data[i, j, 0] contains the Blue value of the pixel at (i,j)
					//data[i, j, 1] contains the Green value of the pixel at (i,j)
					//data[i, j, 2] contains the Red value of the pixel at (i,j)
                    //Console.WriteLine("pixel at (" + i + ", " + j + ")  Blue = " + data[i, j, 0] + ")  Green = " + data[i, j, 1] + ")  Red = " + data[i, j, 2]);
                }
            }
        }
    }
}
*/