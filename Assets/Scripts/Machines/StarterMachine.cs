using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterMachine : Machine
{
    public ArtifactType artifactType;
    public int quantity;

    public void Init(Vector2Int position, Direction direction, ArtifactType artifactType, int quantity = 0)
    {
        Init(position, direction);
        this.artifactType = artifactType;
        this.quantity = quantity;
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
