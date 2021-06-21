using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateTool : GameTool
{
    override protected void OnClickMachine(Machine machine) {
        machine.Rotate(1);
    }
}
