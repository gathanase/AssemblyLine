using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerMachine : Machine
{
    public override void Feed(Artifact artifact) {
        artifact.direction = this.direction;
    }

    public override void OnTick() {
    }
}
