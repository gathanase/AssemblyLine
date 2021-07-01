using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildTool : GameTool
{
    private MachineDatabase machineDatabase;
    private MachineType machineType;

    public void Awake() {
        base.Awake();
        machineDatabase = FindObjectOfType<MachineDatabase>(true);
    }

    public void SetMachineType(MachineType machineType) {
        this.machineType = machineType;
    }

    override protected void OnClickEmpty(Vector2Int pos) {
        Debug.Log("Build " + machineType);
        Machine machine = Instantiate(machineDatabase.GetModel(machineType));
        machine.Init(pos, Direction.SOUTH);
        gameController.Add(machine);
    }
}
