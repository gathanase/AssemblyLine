using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriSplitterMachine : AbstractSplitterMachine
{
    public override MachineType GetMachineType() {
        return MachineType.TRI_SPLITTER;
    }

    public override HashSet<int> GetRotates() {
        return new HashSet<int>() { -1, 0, 1 };
    }
}
