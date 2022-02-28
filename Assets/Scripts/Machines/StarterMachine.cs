using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StarterMachine : Machine
{
    public ArtifactType artifactType;
    public int quantity;

    public StarterMachine() {
        artifactType = ArtifactType.ALUMINUM;
        quantity = 0;
    }

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

    public override void Init(Machine.Save _save, FactoryFloor floor, GameDatabase gameDatabase)
    {
        Save save = (Save) _save;
        base.Init(save, floor, gameDatabase);
        this.artifactType = ArtifactTypeExtensions.Parse(save.artifactType);
        this.quantity = save.quantity;
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

    public override void Copy(Machine other)
    {
        StarterMachine other2 = (StarterMachine) other;
        this.artifactType = other2.artifactType;
        this.quantity = other2.quantity;
    }

    public override void OnTick() {
        int cost = gameDatabase.GetInfo(artifactType).cost;
        for (int i = 0; i < quantity; i++) {
            gameController.RemoveMoney(cost);
            Add(artifactType, direction);
        }
    }
}
