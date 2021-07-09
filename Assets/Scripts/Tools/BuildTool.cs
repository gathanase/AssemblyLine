using UnityEngine;

public class BuildTool : GameTool
{
    private MachineDatabase machineDatabase;
    private MachineType machineType;

    public override ToolType GetToolType() {
        return ToolType.BUILD;
    }

    public new void Awake() {
        base.Awake();
        machineDatabase = FindObjectOfType<MachineDatabase>(true);
    }

    public void SetMachineType(MachineType machineType) {
        this.machineType = machineType;
    }

    override protected void OnClickEmpty(Vector2Int pos) {
        Machine machine = Instantiate(machineDatabase.GetModel(machineType));
        machine.Init(pos, Direction.SOUTH);
        gameController.RemoveMoney(machine.GetInfo().cost);
        gameController.Add(machine);
    }
}
