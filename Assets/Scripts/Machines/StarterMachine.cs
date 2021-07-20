using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StarterMachine : Machine
{
    public ArtifactType artifactType = ArtifactType.IRON;
    public int quantity = 0;

    public new class Save : Machine.Save {
        public string artifactType;
        public int quantity;
    }

    public override Machine.Save ToSave()
    {
        Save save = new Save();
        base.WriteSave(save);
        save.artifactType = artifactType.ToString();
        save.quantity = quantity;
        return save;
    }

    public override MachineType GetMachineType()
    {
        return MachineType.STARTER;
    }
    
    public override Window CreateInfoWindow() {
        StarterMachineWindow infoWindow = FindObjectOfType<StarterMachineWindow>(true);
        infoWindow.Init(this);
        return infoWindow;
    }

    public override void Feed(Artifact artifact) {
        Remove(artifact);
    }

    public override void OnTick() {
        int cost = gameDatabase.GetInfo(artifactType).cost;
        for (int i = 0; i < quantity; i++) {
            gameController.RemoveMoney(cost);
            Add(artifactType, direction);
        }
    }
}
