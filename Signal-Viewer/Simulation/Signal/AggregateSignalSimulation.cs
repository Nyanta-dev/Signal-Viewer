using System.Collections.Generic;

using Signal_Viewer.Type.Signal;

/* Description:
 * Here descripted mechanism calculation  parameters aggregate signal.
 */

namespace Signal_Viewer.Simulation.Signal {
    class AggregateSignalSimulation {
        private AggregateSignal aggregateSignal;
        private double timePoint;

        public double TimeStep { get; set; } = 0.05;

        public delegate void SimulationStepResult(double signalAmplitude, List<(double amplitude, double phase)> signalComponentsParameters);
        private SimulationStepResult simulationStepResult;

        public void RegisterSimulationResultHandler(SimulationStepResult del) {
            simulationStepResult += del;
        }
        public void UnregisterSimulationResultHandler(SimulationStepResult del) {
            simulationStepResult -= del;
        }

        public AggregateSignalSimulation(AggregateSignal aggregateSignal) {
            this.aggregateSignal = aggregateSignal;
        }

        public void SimulationStep() {
            timePoint += TimeStep;
            simulationStepResult?.Invoke(GetSignalAmplitude(timePoint), GetSignalComponentsParameters(timePoint));
        }

        private double GetSignalAmplitude(double timePoint) {
            return aggregateSignal.GetSignalAmplitude(timePoint);
        }
        private List<(double amplitude, double phase)> GetSignalComponentsParameters(double timePoint) {
            List<(double amplitude, double phase)> signalComponentsParameters = new List<(double amplitude, double phase)>();

            foreach (var item in aggregateSignal.SignalComponents) {
                signalComponentsParameters.Add((amplitude: item.Amplitude, phase: item.GetSignalPhase(timePoint)));
            }

            return signalComponentsParameters;
        }
    }
}