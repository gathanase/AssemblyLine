public class RotateTool : GameTool
{
    override protected void OnClickMachine(Machine machine) {
        machine.Rotate(1);
    }
}
