using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FactoryFloor
{
    private GameController gameController;
    public Dictionary<Vector2Int, Machine> machines;
    private HashSet<Artifact> artifacts;
    private HashSet<Artifact> artifactsToCreate;
    private HashSet<Artifact> artifactsToRemove;

    public FactoryFloor(GameController gameController) {
        this.gameController = gameController;
        machines = new Dictionary<Vector2Int, Machine>();
        artifacts = new HashSet<Artifact>();
        artifactsToCreate = new HashSet<Artifact>();
        artifactsToRemove = new HashSet<Artifact>();
    }

    public class Save {
        public List<Machine.Save> machines;
        public List<Artifact.Save> artifacts;
    }

    public Save ToSave() {
        Save save = new Save();
        save.machines = machines.Values.ToList().ConvertAll(machine => machine.ToSave());
        save.artifacts = artifacts.ToList().ConvertAll(artifact => artifact.ToSave());
        return save;
    }

    public void OnTick() {
        foreach (Machine machine in machines.Values) {
            machine.OnTick();
        }
        artifacts.ExceptWith(artifactsToRemove);
        artifactsToRemove.Clear();

        foreach (Artifact artifact in artifacts) {
            artifact.OnTick();
            if (machines.TryGetValue(artifact.position, out Machine machine)) {
                machine.Feed(artifact);
            } else {
                Remove(artifact);
            }
        }
        artifacts.ExceptWith(artifactsToRemove);
        artifacts.UnionWith(artifactsToCreate);
        artifactsToRemove.Clear();
        artifactsToCreate.Clear();
    }

    public void Add(Artifact artifact) {
        artifactsToCreate.Add(artifact);
    }

    public void Remove(Artifact artifact) {
        artifactsToRemove.Add(artifact);
        gameController.ZDestroy(artifact.gameObject);
    }

    public void Add(Machine machine) {
        machines.Add(machine.position, machine);
    }

    public void Remove(Machine machine) {
        machines.Remove(machine.position);
        gameController.ZDestroy(machine.gameObject);
    }
}
