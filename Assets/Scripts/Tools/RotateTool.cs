public class RotateTool : GameTool
{
    public override ToolType GetToolType() {
        return ToolType.ROTATE;
    }

    override protected void OnClickMachine(Machine machine) {
        machine.Rotate(1);
    }
}
