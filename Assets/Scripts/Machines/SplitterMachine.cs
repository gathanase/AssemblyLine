using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SplitterMachine : Machine
{
    public Dictionary<int, int> countsByRotate;  // key: -1, 0, 1 is the rotation of the artifact direction
    private int counter = 0;

    public override MachineType GetMachineType()
    {
        return MachineType.SPLITTER;
    }

    void Start() {
        countsByRotate = new Dictionary<int, int>();
        countsByRotate[-1] = 1;
        countsByRotate[0] = 1;
        countsByRotate[1] = 1;        
    }

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
