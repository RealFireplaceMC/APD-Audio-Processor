using System;
using System.Linq;
using CSCore;
using CSCore.DSP;
using CSCore.SoundIn;
using CSCore.SoundOut;
using CSCore.Streams;
using CSCore.Streams.Effects;
using Processing;
using Processing.Handlers;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            WasapiGeneral wasapi = new WasapiGeneral();

            wasapi.StartCaptureMicrophone();
            wasapi.ApplyEffect(new DmoEchoEffect(wasapi.wave));
            wasapi.StartOutput();

            FftSize fftsize = CSCore.DSP.FftSize.Fft1024;

            FftProvider fftHandler = new FftProvider(wasapi.sis.WaveFormat.Channels, CSCore.DSP.FftSize.Fft1024);

            float[] fftValues = new float[(int)fftsize];

            var notificationSource = new SingleBlockNotificationStream(wasapi.output.WaveSource.ToSampleSource());

            string gainGraph = "";
            int i;

            while (true){

                gainGraph = "";

                i = 0;

                notificationSource.SingleBlockRead += (s, a) => fftHandler.Add(a.Left, a.Right);
                fftHandler.GetFftData(fftValues);

                foreach (int x in fftValues)
                {
                    foreach (int y in Enumerable.Range(0, x))
                    {
                        /*
                        i += 1;
                        if (i == 1)
                        {
                            gainGraph += "#";
                            i = 0;
                        }
                        */
                        Console.WriteLine(y);
                    }
                    Console.WriteLine(x);
                }
                
                //Console.WriteLine(gainGraph);
            }

            Console.ReadKey();

            wasapi.StopCapture();
            wasapi.StopPlayback();
        }
    }
}
