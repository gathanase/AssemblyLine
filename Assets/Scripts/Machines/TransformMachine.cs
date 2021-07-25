using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class TransformMachine : Machine
{
    public Queue<ArtifactType> queue;
    private Dictionary<ArtifactType, ArtifactType> transformMapping;

    public TransformMachine() {
        queue = new Queue<ArtifactType>();
    }

    void Awake() {
        transformMapping = BuildMapping();
    }

    public new class Save : Machine.Save {
        public List<string> queue;
    }

    public override Machine.Save ToSave()
    {
        Save save = new Save();
        base.WriteSave(save);
        save.queue = queue.ToList().ConvertAll(type => type.ToString());
        return save;
    }

    public override void Init(Machine.Save _save, FactoryFloor floor, GameDatabase gameDatabase)
    {
        Save save = (Save) _save;
        base.Init(save, floor, gameDatabase);
        this.queue.Clear();
        this.queue.Concat(save.queue.ConvertAll(typeStr => ArtifactTypeExtensions.Parse(typeStr)));
    }

    protected abstract Dictionary<ArtifactType, ArtifactType> BuildMapping();

    public override Window CreateInfoWindow() {
        TransformMachineWindow infoWindow = FindObjectOfType<TransformMachineWindow>(true);
        infoWindow.Init(this);
        return infoWindow;
    }

    public override void Feed(Artifact artifact) {
        if (queue.Count < 5) {
            queue.Enqueue(artifact.type);
        }
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
}
