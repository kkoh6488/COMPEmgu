namespace Emgucv
{
    enum Lab
    {
        SixVideoInverter,
        SevenHistogram,
        EightInstagram,
        NineShotDetection,
    }

    public class EmgucvTutorials
    {
        public static void Main(string[] args)
        {        
            Lab lab = Lab.NineShotDetection;
            switch(lab)
            {
                case Lab.SixVideoInverter:
                    VideoInverter vi = new VideoInverter();
                    vi.Run();
                    break;
                case Lab.SevenHistogram:
                    ColorHistogramUtility ch = new ColorHistogramUtility(4);
                    ch.Run();
                    break;
                case Lab.EightInstagram:

                    break;
                case Lab.NineShotDetection:
                    HistogramShotDetector hsd = new HistogramShotDetector();
                    hsd.Run();
                    break;
            }
        }    
    }
}
