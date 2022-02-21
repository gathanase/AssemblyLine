using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriSplitterMachine : AbstractSplitterMachine
{
    public override MachineType GetMachineType() {
        return MachineType.TRI_SPLITTER;
    }

    public override HashSet<Rotation> GetRotations() {
        return new HashSet<Rotation>() { Rotation.LEFT, Rotation.NONE, Rotation.RIGHT };
    }
}
