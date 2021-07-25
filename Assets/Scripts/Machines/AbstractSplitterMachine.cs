using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractSplitterMachine : Machine
{
    public Dictionary<int, int> countsByRotate;  // key: -1, 0, 1 is the rotation of the artifact direction
    private int counter = 0;

    public AbstractSplitterMachine() {
        countsByRotate = new Dictionary<int, int>();
        countsByRotate[-1] = 0;
        countsByRotate[0] = 0;
        countsByRotate[1] = 0;
        foreach (int rotate in GetRotates()) {
            countsByRotate[rotate] = 1;
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
        save.countsRight = countsByRotate[-1];
        save.countsForward = countsByRotate[0];
        save.countsLeft = countsByRotate[1];
        return save;
    }

    public override void Init(Machine.Save _save, FactoryFloor floor, GameDatabase gameDatabase)
    {
        Save save = (Save) _save;
        base.Init(save, floor, gameDatabase);
        this.countsByRotate[-1] = save.countsRight;
        this.countsByRotate[0] = save.countsForward;
        this.countsByRotate[1] = save.countsLeft;
        this.counter = save.counter;
    }

    public abstract HashSet<int> GetRotates();

    public override Window CreateInfoWindow() {
        SplitterMachineWindow infoWindow = FindObjectOfType<SplitterMachineWindow>(true);
        infoWindow.Init(this);
        return infoWindow;
    }

    public override void Feed(Artifact artifact) {
        if (artifact.direction == this.direction) {
            // artifact comes from behind
            int rotate;
            if (counter >= countsByRotate.Values.Sum()) {
                counter = 0;
            }
            if (counter < countsByRotate[-1]) {
                rotate = -1;
            } else if (counter < countsByRotate[-1] + countsByRotate[0]) {
                rotate = 0;
            } else {
                rotate = 1;
            }
            artifact.direction = this.direction.Rotate(rotate);
            counter ++;
        } else {
            Remove(artifact);
        }
    }

    public override void OnTick() {
    }
}
