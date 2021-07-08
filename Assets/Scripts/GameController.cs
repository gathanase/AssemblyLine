using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Artifact artifactModel;
    public RecipeDatabase recipeDatabase;
    public MachineDatabase machineDatabase;

    public Text moneyText;
    private long money = 0;

    private InfoTool infoTool;
    private RotateTool rotateTool;
    private BuildTool buildTool;
    private DeleteTool deleteTool;

    public Dictionary<Vector2Int, Machine> machines;
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
        infoTool = FindObjectOfType<InfoTool>();
        rotateTool = FindObjectOfType<RotateTool>();
        buildTool = FindObjectOfType<BuildTool>();
        deleteTool = FindObjectOfType<DeleteTool>();
        SetTool(ToolType.INFO);
    }

    void Start()
    {
        AddMoney(10000);
        Machine starterMachineA = Instantiate(machineDatabase.GetModel(MachineType.STARTER));
        starterMachineA.Init(new Vector2Int(0, 0), Direction.SOUTH);
        Add(starterMachineA);

        Machine starterMachineB = Instantiate(machineDatabase.GetModel(MachineType.STARTER));
        starterMachineB.Init(new Vector2Int(3, 0), Direction.SOUTH);
        Add(starterMachineB);

        Machine rollerMachine = Instantiate(machineDatabase.GetModel(MachineType.ROLLER));
        rollerMachine.Init(new Vector2Int(3, -1), Direction.SOUTH);
        Add(rollerMachine);

        Machine cutterMachine = Instantiate(machineDatabase.GetModel(MachineType.CUTTER));
        cutterMachine.Init(new Vector2Int(3, -2), Direction.EAST);
        Add(cutterMachine);

        Machine crafterMachine = Instantiate(machineDatabase.GetModel(MachineType.CRAFTER));
        crafterMachine.Init(new Vector2Int(4, -2), Direction.SOUTH);
        Add(crafterMachine);
        InvokeRepeating("OnTick", 1, 1);
    }

    public void SetTool(ToolType gameTool) {
        infoTool.gameObject.SetActive(false);
        rotateTool.gameObject.SetActive(false);
        buildTool.gameObject.SetActive(false);
        deleteTool.gameObject.SetActive(false);
        switch (gameTool) {
            case ToolType.INFO:
                infoTool.gameObject.SetActive(true);
                break;
            case ToolType.ROTATE:
                rotateTool.gameObject.SetActive(true);
                break;
            case ToolType.BUILD:
                buildTool.gameObject.SetActive(true);
                break;
            case ToolType.DELETE:
                deleteTool.gameObject.SetActive(true);
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

    public void Add(ArtifactType type, Vector2Int position, Direction direction) {
        Artifact artifact = Instantiate(artifactModel);
        artifact.Init(position, direction, type);
        artifactsToCreate.Add(artifact);
    }

    public void Remove(Artifact artifact) {
        artifactsToRemove.Add(artifact);
        Destroy(artifact.gameObject);
    }

    public void Add(Machine machine) {
        machines.Add(machine.position, machine);
    }

    public void Remove(Machine machine) {
        machines.Remove(machine.position);
        Destroy(machine.gameObject);
    }

    public void AddMoney(long amount) {
        money += amount;
        RefreshMoney();
    }

    public void RemoveMoney(long amount) {
        money -= amount;
        RefreshMoney();
    }

    private void RefreshMoney() {
        moneyText.text = money.ToString("C0");
    }
}
