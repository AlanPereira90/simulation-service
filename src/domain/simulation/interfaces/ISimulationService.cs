using simulation_service.src.domain.simulation.entities;

namespace simulation_service.src.domain.simulation.interfaces;

public interface ISimulationService
{
  string CreateSimulation(Simulation simulation);
  string CancellSimulation(Guid id, string UserId);
  string ProposeSimulation(Guid id, string UserId);
  string FinishSimulation(Guid id, string UserId);

}