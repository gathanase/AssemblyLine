using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public StarterMachine starterMachineModel;
    public CutterMachine cutterMachineModel;
    public RollerMachine rollerMachineModel;
    public BuilderMachine builderMachineModel;
    public Artifact artifactModel;
    public RecipeDatabase recipeDatabase;

    public Window infoWindow;
    private Dictionary<Vector2Int, Machine> machines;
    private HashSet<Artifact> artifacts;
    private HashSet<Artifact> artifactsToCreate;
    private HashSet<Artifact> artifactsToRemove;

    void Awake() {
        machines = new Dictionary<Vector2Int, Machine>();
        artifacts = new HashSet<Artifact>();
        artifactsToCreate = new HashSet<Artifact>();
        artifactsToRemove = new HashSet<Artifact>();
        recipeDatabase = new RecipeDatabase();
        recipeDatabase.Load();
        infoWindow = null;
    }

    void Start()
    {
        StarterMachine starterMachineA = Instantiate(starterMachineModel);
        starterMachineA.Init(new Vector2Int(0, 0), Direction.NORTH, ArtifactType.GOLD, 0);
        Add(starterMachineA);

        StarterMachine starterMachine = Instantiate(starterMachineModel);
        starterMachine.Init(new Vector2Int(3, 0), Direction.SOUTH, ArtifactType.GOLD, 1);
        Add(starterMachine);

        RollerMachine rollerMachine = Instantiate(rollerMachineModel);
        rollerMachine.Init(new Vector2Int(3, -1), Direction.SOUTH);
        Add(rollerMachine);

        CutterMachine cutterMachine = Instantiate(cutterMachineModel);
        cutterMachine.Init(new Vector2Int(3, -2), Direction.EAST);
        Add(cutterMachine);

        BuilderMachine builderMachine = Instantiate(builderMachineModel);
        builderMachine.Init(new Vector2Int(4, -2), Direction.SOUTH);
        Add(builderMachine);

        InvokeRepeating("OnTick", 1, 1);
    }

    void Update() {
        Vector3 pos3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int mousePos = new Vector2Int(Mathf.RoundToInt(pos3.x), Mathf.RoundToInt(pos3.y));

        if (Input.GetMouseButtonDown(0)) {
            if (infoWindow == null) {
                OnClick(mousePos);
            }
        }
    }

    void OnClick(Vector2Int pos) {
        Machine machine;
        if (machines.TryGetValue(pos, out machine)) {
            machine.CreateInfoWindow();
        } else {
            // RollerMachine rollerMachine = Instantiate(rollerMachineModel);
            // rollerMachine.init(pos, Direction.SOUTH);
            // Add(rollerMachine);
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
}
