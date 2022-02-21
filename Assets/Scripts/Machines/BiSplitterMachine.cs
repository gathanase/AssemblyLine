using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BiSplitterMachine : AbstractSplitterMachine
{
    public override MachineType GetMachineType() {
        return MachineType.BI_SPLITTER;
    }

    public override HashSet<Rotation> GetRotations() {
        return new HashSet<Rotation>() { Rotation.LEFT, Rotation.RIGHT };
    }
}
