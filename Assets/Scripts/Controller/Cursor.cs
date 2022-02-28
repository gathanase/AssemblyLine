using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour, AxisListener
{
    KeyController keyController;
    GameController gameController;
    MachineDatabase machineDatabase;
    BuildWindow buildWindow;
    Dictionary<MachineType, Machine> lastInfoMachine;
    MachineType lastBuiltMachine;

    public void Start() {
        buildWindow = FindObjectOfType<BuildWindow>(true);
        gameController = FindObjectOfType<GameController>();
        machineDatabase = gameController.gameDatabase.machineDatabase;
        keyController = FindObjectOfType<KeyController>();
        keyController.SetListener(this);
        lastInfoMachine = new Dictionary<MachineType, Machine>();
        lastBuiltMachine = MachineType.STARTER;
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
        Machine machine;
        if (gameController.GetFactoryFloor().machines.TryGetValue(GetPos(), out machine)) {
            return machine;
        } else {
            return null;
        }
    }

    public bool IsModified() {
        return Input.GetButton("Modified");
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
        if (Input.GetButtonDown("Build")) {
            Build();
        }
    }

    public void Rotate() {
        Machine machine = GetMachine();
        if (machine != null) {
            machine.Rotate(IsModified() ? Rotation.RIGHT : Rotation.LEFT);
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
            if (IsModified()) {
                machine.Copy(lastInfoMachine[machine.GetMachineType()]);
            } else {
                keyController.SetListener(machine.CreateInfoWindow());
                lastInfoMachine[machine.GetMachineType()] = machine;
            }
        }
    }

    public void Build() {
        Machine machine = GetMachine();
        if (machine == null) {
            if (IsModified()) {
                Build(lastBuiltMachine);
            } else {
                buildWindow.Init();
            }
        }
    }

    public void Build(MachineType machineType) {
        Machine machine = GetMachine();
        if (machine == null) {
            FactoryFloor factoryFloor = gameController.GetFactoryFloor();
            machine = Instantiate(machineDatabase.GetModel(machineType));
            machine.Init(factoryFloor, GetPos(), Direction.SOUTH);
            gameController.RemoveMoney(machine.GetInfo().cost);
            factoryFloor.Add(machine);
            lastBuiltMachine = machineType;
        }
    }
}
