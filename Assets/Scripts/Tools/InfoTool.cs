public class InfoTool : GameTool
{
    public override ToolType GetToolType() {
        return ToolType.INFO;
    }

    override protected void OnClickMachine(Machine machine) {
        machine.CreateInfoWindow();
    }
}
