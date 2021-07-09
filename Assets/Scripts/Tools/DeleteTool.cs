public class DeleteTool : GameTool
{
    private MachineDatabase machineDatabase;

    public new void Awake() {
        base.Awake();
        machineDatabase = FindObjectOfType<MachineDatabase>(true);
    }

    override protected void OnClickMachine(Machine machine) {
        gameController.Remove(machine);
        gameController.AddMoney(machineDatabase.GetInfo(machine.GetMachineType()).cost * 80 / 100);
    }
}
