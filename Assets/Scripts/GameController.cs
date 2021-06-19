using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour, IPointerDownHandler
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
    private GameTool gameTool;

    void Awake() {
        machines = new Dictionary<Vector2Int, Machine>();
        artifacts = new HashSet<Artifact>();
        artifactsToCreate = new HashSet<Artifact>();
        artifactsToRemove = new HashSet<Artifact>();
        recipeDatabase = new RecipeDatabase();
        recipeDatabase.Load();
        infoWindow = null;
        gameTool = GameTool.INFO;
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

    public void OnPointerDown(PointerEventData eventData) {
        Debug.LogFormat("Read {0} {1}", this.GetInstanceID(), gameTool);
        Vector3 pos3 = Camera.main.ScreenToWorldPoint(eventData.position);
        Vector2Int mousePos = new Vector2Int(Mathf.RoundToInt(pos3.x), Mathf.RoundToInt(pos3.y));
        OnClick(mousePos);
        // if (infoWindow == null) {
        //     OnClick(mousePos);
        // }
    }

    public void SetTool(GameTool gameTool) {
        Debug.Log("Select tool " + gameTool);
        this.gameTool = gameTool;
    }

    void OnClick(Vector2Int pos) {
        Machine machine;
        Debug.LogFormat("Read {0} {1}", this.GetInstanceID(), gameTool);
        switch (gameTool) {
            case GameTool.INFO:
                if (machines.TryGetValue(pos, out machine)) {
                    machine.CreateInfoWindow();
                }
                break;
            case GameTool.BUILD:
                if (! machines.TryGetValue(pos, out machine)) {
                    RollerMachine rollerMachine = Instantiate(rollerMachineModel);
                    rollerMachine.Init(pos, Direction.SOUTH);
                    Add(rollerMachine);
                }
                break;
            case GameTool.ROTATE:
                if (machines.TryGetValue(pos, out machine)) {
                    machine.Rotate(1);
                }
                break;
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
