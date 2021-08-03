using System;
using System.Collections.Generic;

/* Description:
 * The type of aggregate signal, which consists of a set of harmonic signals.
 * It also contains information about the spectrum signal.
 */

namespace Signal_Viewer.Type.Signal {
    class AggregateSignal {
        private int countSignalComponents;
        private List<HarmSignal> signalComponents;
        private Dictionary<int, double> spectrumSignal;

        public AggregateSignal() {
            signalComponents = new List<HarmSignal>();
            spectrumSignal = new Dictionary<int, double>();
        }

        public void AddSignalComponent(HarmSignal signalComponent) {
            signalComponents.Add(signalComponent);
            SetSpectrumSignal(signalComponent);
            countSignalComponents++;
        }
        public void RemSignalComponent(HarmSignal signalComponent) { 
            signalComponents.Remove(signalComponent);
            ResetSpectrumSignal();
            countSignalComponents--;
        }

        private void ResetSpectrumSignal() {
            if (signalComponents == null) {
                throw new Exception("The signal components you are trying to process is null.");
            }

            foreach (HarmSignal item in signalComponents) {
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
            if (signalComponents == null) {
                throw new Exception("The signal components you are trying to process is null.");
            }

            double result = 0;
            foreach (HarmSignal item in signalComponents) {
                result += item.Amplitude * Math.Sin((item.Frequency * timePoint) + item.BeginPhaseRadian);
            }
            return result;
        }
    }
}