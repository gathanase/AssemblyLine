public class InfoTool : GameTool
{
    override protected void OnClickMachine(Machine machine) {
        machine.CreateInfoWindow();
    }
}
