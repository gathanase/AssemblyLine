using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class TransformMachine : Machine
{
    public Queue<ArtifactType> queue = new Queue<ArtifactType>();
    private Dictionary<ArtifactType, ArtifactType> transformMapping;

    void Awake() {
        transformMapping = BuildMapping();
    }

    protected abstract Dictionary<ArtifactType, ArtifactType> BuildMapping();

    public override void Feed(Artifact artifact) {
        queue.Enqueue(artifact.type);
        Remove(artifact);
    }

    public override void OnTick() {
        if (queue.Count > 0) {
            ArtifactType type = queue.Dequeue();
            if (transformMapping.TryGetValue(type, out ArtifactType newType)) {
                Add(newType, direction);
            }
        }
    }

    public override Window CreateInfoWindow() {
        return null;
        //Window infoWindow = Instantiate(infoWindowModel, Vector3.zero, Quaternion.identity);
        //infoWindow.Init(this, gameController);
        //return infoWindow;
    }
}
