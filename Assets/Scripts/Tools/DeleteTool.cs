public class DeleteTool : GameTool
{
    override protected void OnClickMachine(Machine machine) {
        gameController.Remove(machine);
    }
}
