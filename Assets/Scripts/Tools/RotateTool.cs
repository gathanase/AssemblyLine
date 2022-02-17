using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTool : GameTool
{
    public GameObject arrowModel;
    public Dictionary<Vector2Int, GameObject> arrows;

    override public ToolType GetToolType() {
        return ToolType.ROTATE;
    }

    public override void Awake()
    {
        base.Awake();
        arrows = new Dictionary<Vector2Int, GameObject>();
    }

    public override void SetActive(bool active)
    {
        base.SetActive(active);
        foreach (GameObject arrow in arrows.Values) {
            Destroy(arrow);
        }
        arrows.Clear();

        if (active) {
            foreach (KeyValuePair<Vector2Int, Machine> pair in GetFactoryFloor().machines) {
                Transform transform = pair.Value.transform;
                GameObject arrow = Instantiate(arrowModel, transform.position, transform.rotation);
                arrows.Add(pair.Key, arrow);
            }
        }
    }

    override protected void OnClickMachine(Machine machine) {
        machine.Rotate(1);
        arrows[machine.position].transform.rotation = machine.transform.rotation;
    }
}
