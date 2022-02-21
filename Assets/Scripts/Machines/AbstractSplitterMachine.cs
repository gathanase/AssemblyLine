using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractSplitterMachine : Machine
{
    public Dictionary<Rotation, int> countsByRotation;
    private int counter = 0;

    public AbstractSplitterMachine() {
        countsByRotation = new Dictionary<Rotation, int>();
        countsByRotation[Rotation.LEFT] = 0;
        countsByRotation[Rotation.NONE] = 0;
        countsByRotation[Rotation.RIGHT] = 0;
        foreach (Rotation rotation in GetRotations()) {
            countsByRotation[rotation] = 1;
        }
    }

    public new class Save : Machine.Save {
        public int countsRight;
        public int countsForward;
        public int countsLeft;
        public int counter;
    } 

    public override Machine.Save ToSave()
    {
        Save save = new Save();
        base.WriteSave(save);
        save.counter = counter;
        save.countsRight = countsByRotation[Rotation.RIGHT];
        save.countsForward = countsByRotation[Rotation.NONE];
        save.countsLeft = countsByRotation[Rotation.LEFT];
        return save;
    }

    public override void Init(Machine.Save _save, FactoryFloor floor, GameDatabase gameDatabase)
    {
        Save save = (Save) _save;
        base.Init(save, floor, gameDatabase);
        this.countsByRotation[Rotation.RIGHT] = save.countsRight;
        this.countsByRotation[Rotation.NONE] = save.countsForward;
        this.countsByRotation[Rotation.LEFT] = save.countsLeft;
        this.counter = save.counter;
    }

    public abstract HashSet<Rotation> GetRotations();

    public override Window CreateInfoWindow() {
        SplitterMachineWindow infoWindow = FindObjectOfType<SplitterMachineWindow>(true);
        infoWindow.Init(this);
        return infoWindow;
    }

    public override void Feed(Artifact artifact) {
        if (artifact.direction == this.direction) {
            // artifact comes from behind
            Rotation rotation;
            if (counter >= countsByRotation.Values.Sum()) {
                counter = 0;
            }
            if (counter < countsByRotation[Rotation.RIGHT]) {
                rotation = Rotation.RIGHT;
            } else if (counter < countsByRotation[Rotation.RIGHT] + countsByRotation[Rotation.NONE]) {
                rotation = Rotation.NONE;
            } else {
                rotation = Rotation.LEFT;
            }
            artifact.direction = this.direction.Rotate(rotation);
            counter ++;
        } else {
            Remove(artifact);
        }
    }

    public override void OnTick() {
    }
}
