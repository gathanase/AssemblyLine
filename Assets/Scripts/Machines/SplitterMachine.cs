using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterMachine : Machine
{
    public int countRight = 1;
    public int countLeft = 1;
    public int countForward = 1;
    private int counter = 0;

    public override MachineType GetMachineType()
    {
        return MachineType.SPLITTER;
    }

    public override Window CreateInfoWindow() {
        return null;
    }

    public override void Feed(Artifact artifact) {
        if (artifact.direction == this.direction) {
            // artifact comes from behind
            int rotate;
            if (counter >= countRight + countForward + countLeft) {
                counter = 0;
            }
            if (counter < countRight) {
                rotate = -1;
            } else if (counter < countRight + countForward) {
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
