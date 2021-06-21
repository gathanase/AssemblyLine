using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfoTool : GameTool
{
    override protected void OnClickMachine(Machine machine) {
        machine.CreateInfoWindow();
    }
}
