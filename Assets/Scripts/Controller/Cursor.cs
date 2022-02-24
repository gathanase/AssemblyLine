using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour, AxisListener
{
    KeyController keyController;
    GameController gameController;
    MachineDatabase machineDatabase;

    public void Start() {
        gameController = FindObjectOfType<GameController>();
        machineDatabase = gameController.gameDatabase.machineDatabase;
        keyController = FindObjectOfType<KeyController>();
        keyController.SetListener(this);
    }

    public void OnVerticalAxis(int vAxis) {
        transform.Translate(0, vAxis, 0);
    }

    public void OnHorizontalAxis(int hAxis) {
        transform.Translate(hAxis, 0, 0);
    }

    private Vector2Int GetPos() {
        Vector3 pos3 = transform.position;
        return new Vector2Int(Mathf.RoundToInt(pos3.x), Mathf.RoundToInt(pos3.y));
    }

    private Machine GetMachine() {
        FactoryFloor factoryFloor = gameController.GetFactoryFloor();
        Machine machine;
        if (factoryFloor.machines.TryGetValue(GetPos(), out machine)) {
            return machine;
        } else {
            return null;
        }
    }

    public void Update() {
        if (Input.GetButtonDown("Rotate")) {
            Rotate();
        }
        if (Input.GetButtonDown("Delete")) {
            Delete();
        }
        if (Input.GetButtonDown("Info")) {
            Info();
        }
    }

    public void Rotate() {
        Machine machine = GetMachine();
        if (machine != null) {
            machine.Rotate(Rotation.LEFT);
        }
    }

    public void Delete() {
        Machine machine = GetMachine();
        if (machine != null) {
            gameController.GetFactoryFloor().Remove(machine);
            gameController.AddMoney(machineDatabase.GetInfo(machine.GetMachineType()).cost * 80 / 100);
        }
    }

    public void Info() {
        Machine machine = GetMachine();
        if (machine != null) {
            keyController.SetListener(machine.CreateInfoWindow());
        }
    }
}
