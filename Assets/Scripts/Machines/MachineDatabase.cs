using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class MachineDatabase : MonoBehaviour
{
    public Dictionary<MachineType, MachineInfo> machines;
    public List<Machine> machineModelList;
    private Dictionary<MachineType, Machine> machineModelByType;

    public class MachineInfo {
        public MachineType type;
        public string name;
        public int cost;
    }

    [System.Serializable]
    private class MachineJson {
        public string type;
        public string name;
        public int cost;
    }

    [System.Serializable]
    private class MachinesJson {
        public List<MachineJson> machines;
    }

    void Awake() {
        TextAsset file = Resources.Load<TextAsset>("Machines");
        MachinesJson json = JsonUtility.FromJson<MachinesJson>(file.text);
        machines = json.machines.ConvertAll(ToModel).ToDictionary(model => model.type);
        Debug.LogFormat("Loaded {0} machines", machines.Count);

        machineModelByType = new Dictionary<MachineType, Machine>();
        foreach (Machine machine in machineModelList) {
            machineModelByType.Add(machine.GetMachineType(), machine);
        }
        Debug.LogFormat("Loaded {0} machine models", machineModelByType.Count);
    }

    private MachineInfo ToModel(MachineJson json) {
        MachineInfo model = new MachineInfo();
        model.type = MachineTypeExtensions.Parse(json.type);
        model.name = json.name;
        model.cost = json.cost;
        return model;
    }

    public MachineInfo GetInfo(MachineType type) {
        return machines[type];
    }

    public Machine GetModel(MachineType type) {
        return machineModelByType[type];
    }
}
