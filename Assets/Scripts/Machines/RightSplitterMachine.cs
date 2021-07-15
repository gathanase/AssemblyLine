using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RightSplitterMachine : AbstractSplitterMachine
{
    public override MachineType GetMachineType() {
        return MachineType.RIGHT_SPLITTER;
    }

    public override HashSet<int> GetRotates() {
        return new HashSet<int>() { -1, 0 };
    }
}
