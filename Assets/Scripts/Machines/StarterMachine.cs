using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StarterMachine : Machine
{
    public ArtifactType artifactType;
    public int quantity;
    public StarterMachineWindow infoWindowModel;

    public void Init(Vector2Int position, Direction direction, ArtifactType artifactType, int quantity = 0)
    {
        Init(position, direction);
        this.artifactType = artifactType;
        this.quantity = quantity;
    }

    public override Window CreateInfoWindow() {
        StarterMachineWindow infoWindow = Instantiate(infoWindowModel, Vector3.zero, Quaternion.identity);
        infoWindow.Init(this, gameController);
        return infoWindow;
    }

    public override void Feed(Artifact artifact) {
        Remove(artifact);
    }

    public override void OnTick() {
        for (int i = 0; i < quantity; i++) {
            Add(artifactType, direction);
        }
    }
}