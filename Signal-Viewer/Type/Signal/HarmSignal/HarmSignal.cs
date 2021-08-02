/* Description:
 * This type is a signal component and is a harmonic signal. 
 * It can also be an independent signal if it is the only component of the signal.
 */

namespace Signal_Viewer.Type.Signal.Signal {
    class HarmSignal {
        public double Amplitude { get; private set; }
        public double BeginPhaseRadian { get; private set; }
        public int Frequency { get; private set; }

        public HarmSignal(double amplitude, int frequency, double beginPhaseRadian) {
            Amplitude = amplitude;
            Frequency = frequency;
            BeginPhaseRadian = beginPhaseRadian;
        }
    }
}

/* -+-+-+- end file -+-+-+- */