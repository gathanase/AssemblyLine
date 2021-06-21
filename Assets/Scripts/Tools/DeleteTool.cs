using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeleteTool : GameTool
{
    override protected void OnClickMachine(Machine machine) {
        gameController.Remove(machine);
    }
}
