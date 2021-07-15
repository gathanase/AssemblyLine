using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SplitterMachine : AbstractSplitterMachine
{
    public override MachineType GetMachineType() {
        return MachineType.SPLITTER;
    }

    public override HashSet<int> GetRotates() {
        return new HashSet<int>() { -1, 1 };
    }
}
