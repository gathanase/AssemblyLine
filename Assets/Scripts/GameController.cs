using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public StarterMachine starterMachineModel;
    public CutterMachine cutterMachineModel;
    public RollerMachine rollerMachineModel;
    public Artifact artifactModel;

    private Dictionary<Vector2Int, Machine> machines;
    private HashSet<Artifact> artifacts;
    private HashSet<Artifact> artifactsToCreate;
    private HashSet<Artifact> artifactsToRemove;

    void Awake() {
        machines = new Dictionary<Vector2Int, Machine>();
        artifacts = new HashSet<Artifact>();
        artifactsToCreate = new HashSet<Artifact>();
        artifactsToRemove = new HashSet<Artifact>();
    }

    void Start()
    {
        StarterMachine starterMachine = Instantiate(starterMachineModel);
        starterMachine.Init(new Vector2Int(3, 0), Direction.SOUTH, ArtifactType.GOLD, 2);
        Add(starterMachine);

        RollerMachine rollerMachine = Instantiate(rollerMachineModel);
        rollerMachine.Init(new Vector2Int(3, -1), Direction.SOUTH);
        Add(rollerMachine);

        CutterMachine cutterMachine = Instantiate(cutterMachineModel);
        cutterMachine.Init(new Vector2Int(3, -2), Direction.EAST);
        Add(cutterMachine);

        InvokeRepeating("OnTick", 1, 1);
    }

    void Update() {
        Vector3 pos3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int mousePos = new Vector2Int(Mathf.RoundToInt(pos3.x), Mathf.RoundToInt(pos3.y));

        if (Input.GetMouseButtonDown(0)) {
            OnClick(mousePos);
        }
    }

    void OnClick(Vector2Int pos) {
        Machine machine;
        if (machines.TryGetValue(pos, out machine)) {
            machine.Rotate(1);
        } else {
            RollerMachine rollerMachine = Instantiate(rollerMachineModel);
            rollerMachine.init(pos, Direction.SOUTH);
            Add(rollerMachine);
        }
    }

    void OnTick() {
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

    public void Remove(Artifact artifact) {
        artifactsToRemove.Add(artifact);
        Destroy(artifact.gameObject);
    }

    public void Add(ArtifactType type, Vector2Int position, Direction direction) {
        Artifact artifact = Instantiate(artifactModel);
        artifact.Init(position, direction, type);
        artifactsToCreate.Add(artifact);
    }

    public void Add(Machine machine) {
        machines.Add(machine.position, machine);
    }

    public void Sell(Artifact artifact) {
        Debug.Log("Sell artifact " + artifact.type);
    }

    public void OnButton() {
        Debug.Log("GREG BUTTON");
    }
}
