using System;
using System.Collections.Generic;
using System.Text;
using CSCore;
using CSCore.SoundIn;
using CSCore.SoundOut;
using CSCore.Streams;
using CSCore.Streams.Effects;
using Processing.Exceptions;

namespace Processing
{
    class WasapiGeneral
    {
        public WasapiCapture capture;
        public WasapiOut output;
        public SoundInSource sis;
        public IWaveSource wave;
        public ISampleSource sample;

        private bool isCaptureAvailable;
        private bool isOutputAvailable;

        public WasapiGeneral()
        {
            output = new WasapiOut();

            isCaptureAvailable = false;
            isOutputAvailable = false;
        }

        public void StartOutput()
        {
            if (!isCaptureAvailable)
            {
                throw new WasapiNotInitializedException("This instance has no WasapiCapture object initialized. Try using StartLoopback() or StartCaptureMicrophone().");
            }
            else
            {
                isOutputAvailable = true;

                output.Initialize(wave);
                output.Play();
            }
        }

        public void StartCaptureMicrophone()
        {
            if (isCaptureAvailable)
                throw new WasapiAlreadyInitializedException("This instance has already has a WasapiCapture associated");
            else
            {
                isCaptureAvailable = true;
                capture = new WasapiCapture();

                capture.Initialize();
                capture.Start();
                wave = new SoundInSource(capture);
                sis = new SoundInSource(capture);
            }

        }
        
        public void StartCapturePlayback()
        {
            if (isCaptureAvailable)
                throw new WasapiAlreadyInitializedException("This instance has already has a WasapiCapture associated");
            else
            {
                isCaptureAvailable = true;
                capture = new WasapiLoopbackCapture();

                capture.Initialize();
                capture.Start();
                wave = new SoundInSource(capture);

            }
        }

        public void StopCapture()
        {
            if (isCaptureAvailable)
            {
                capture.Stop();
            }
            else
            {
                throw new WasapiNotInitializedException("This instance isn't currently active.");
            }
        }

        public void StopPlayback()
        {
            if (isOutputAvailable)
            {
                output.Stop();
            }
            else
            {
                throw new WasapiNotInitializedException("This instance isn't currently active.");
            }
        }

        public void ApplyEffect(object effect)
        {
            if (effect is DmoChorusEffect)
                wave = new DmoChorusEffect(wave);
            else if (effect is DmoCompressorEffect)
                wave = new DmoCompressorEffect(wave);
            else if (effect is DmoDistortionEffect)
                wave = new DmoDistortionEffect(wave);
            else if (effect is DmoEchoEffect)
                wave = new DmoEchoEffect(wave);
            else if (effect is DmoFlangerEffect)
                wave = new DmoFlangerEffect(wave);
            else if (effect is DmoGargleEffect)
                wave = new DmoGargleEffect(wave);
            else if (effect is DmoWavesReverbEffect)
                wave = new DmoWavesReverbEffect(wave);
            else
                throw new InvalidTypeException(effect.GetType().Name + " is not a valid effect");
        }
    }
}
