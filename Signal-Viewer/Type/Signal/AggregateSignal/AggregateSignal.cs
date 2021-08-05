using System;
using System.Collections.Generic;

/* Description:
 * The type of aggregate signal, which consists of a set of harmonic signals.
 * It also contains information about the spectrum signal.
 */

namespace Signal_Viewer.Type.Signal {
    class AggregateSignal {
        public List<HarmSignal> SignalComponents;
        private Dictionary<int, double> spectrumSignal;

        public AggregateSignal() {
            SignalComponents = new List<HarmSignal>();
            spectrumSignal = new Dictionary<int, double>();
        }

        public void AddSignalComponent(HarmSignal signalComponent) {
            SignalComponents.Add(signalComponent);
            SetSpectrumSignal(signalComponent);
        }
        public void RemSignalComponent(HarmSignal signalComponent) { 
            SignalComponents.Remove(signalComponent);
            ResetSpectrumSignal();
        }

        private void ResetSpectrumSignal() {
            if (SignalComponents == null) {
                throw new Exception("The signal components you are trying to process is null.");
            }

            foreach (HarmSignal item in SignalComponents) {
                if (spectrumSignal.ContainsKey(item.Frequency)) {
                    spectrumSignal[item.Frequency] = item.Amplitude + spectrumSignal.GetValueOrDefault(item.Frequency);
                } else {
                    spectrumSignal.Add(item.Frequency, item.Amplitude);
                }
            }
        }
        private void SetSpectrumSignal(HarmSignal signalComponent) {
            if (spectrumSignal == null) {
                throw new Exception("The signal components you are trying to process is null.");
            }

            if (spectrumSignal.ContainsKey(signalComponent.Frequency)) {
                spectrumSignal[signalComponent.Frequency] = signalComponent.Amplitude + spectrumSignal.GetValueOrDefault(signalComponent.Frequency);
            } else {
                spectrumSignal.Add(signalComponent.Frequency, signalComponent.Amplitude);
            }
        }
        public double GetSpectrumComponentAmplitude(int frequencyNumber) {
            return spectrumSignal.GetValueOrDefault(frequencyNumber);
        }

        public double GetSignalAmplitude(double timePoint) {
            if (SignalComponents == null) {
                throw new Exception("The signal components you are trying to process is null.");
            }

            double result = 0;
            foreach (HarmSignal item in SignalComponents) {
                result += item.Amplitude * Math.Sin((item.Frequency * timePoint) + item.BeginPhaseRadian);
            }
            return result;
        }
    }
}