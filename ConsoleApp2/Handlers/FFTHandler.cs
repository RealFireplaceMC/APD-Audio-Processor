using CSCore.DSP;
using System;
using System.Collections.Generic;
using System.Text;

namespace Processing.Handlers
{
    public class FFTHandler : FftProvider, ISpectrumProvider
    {
        private bool isInitialized;
        private readonly int sampleRate;
        private readonly List<object> contexts = new List<object>();

        private FftSize fftSize;

        public FFTHandler(int channels, int sampleRate, FftSize fftSize) : base(channels, fftSize)
        {
            if (sampleRate <= 0)
                throw new ArgumentOutOfRangeException("sampleRate");
            this.sampleRate = sampleRate;
        }

        public int GetFftBandIndex(float frequency)
        {
            int fftSize = (int)FftSize;
            double f = sampleRate / 2.0;
            // ReSharper disable once PossibleLossOfFraction
            return (int)((frequency / f) * (fftSize / 2));
        }

        public bool GetFftData(float[] fftResultBuffer, object context)
        {
            if (contexts.Contains(context))
                return false;

            contexts.Add(context);
            GetFftData(fftResultBuffer);
            return true;
        }

        public override void Add(float[] samples, int count)
        {
            base.Add(samples, count);
            if (count > 0)
                contexts.Clear();
        }

        public override void Add(float left, float right)
        {
            base.Add(left, right);
            contexts.Clear();
        }
    }        
}
