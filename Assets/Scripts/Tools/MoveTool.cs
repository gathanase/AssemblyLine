using UnityEngine;

public class MoveTool : GameTool
{
    Machine machine = null;
    
    public override ToolType GetToolType() {
        return ToolType.MOVE;
    }

    public override void OnActivate() {
        machine = null;
    }

    override protected void OnClickEmpty(Vector2Int pos) {
        if (machine != null) {
            Vector2Int oldPos = machine.position;
            GetFactoryFloor().machines.Remove(oldPos);
            machine.Init(GetFactoryFloor(), pos, machine.direction);
            GetFactoryFloor().machines.Add(pos, machine);
            machine.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            machine = null;
        }
    }

    override protected void OnClickMachine(Machine machine) {
        this.machine = machine;
        this.machine.GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }
}
