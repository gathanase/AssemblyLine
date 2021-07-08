using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StarterMachine : Machine
{
    public ArtifactType artifactType = ArtifactType.IRON;
    public int quantity = 0;
    private static ArtifactDatabase artifactDatabase = null;

    void Awake() {
        if (artifactDatabase == null) {
            artifactDatabase = FindObjectOfType<ArtifactDatabase>();
        }
    }

    public void Init(Vector2Int position, Direction direction, ArtifactType artifactType, int quantity = 0)
    {
        Init(position, direction);
        this.artifactType = artifactType;
        this.quantity = quantity;
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
        int cost = artifactDatabase.GetInfo(artifactType).cost;
        for (int i = 0; i < quantity; i++) {
            gameController.RemoveMoney(cost);
            Add(artifactType, direction);
        }
    }
}
