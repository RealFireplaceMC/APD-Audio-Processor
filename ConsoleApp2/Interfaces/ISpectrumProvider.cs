using System;
using System.Collections.Generic;
using System.Text;

namespace Processing
{
    interface ISpectrumProvider
    {
        bool GetFftData(float[] fftBuffer, object context);
        int GetFftBandIndex(float frequency);
    }
}
