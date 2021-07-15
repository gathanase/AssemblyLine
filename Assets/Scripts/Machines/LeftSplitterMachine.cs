using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeftSplitterMachine : AbstractSplitterMachine
{
    public override MachineType GetMachineType() {
        return MachineType.LEFT_SPLITTER;
    }

    public override HashSet<int> GetRotates() {
        return new HashSet<int>() { 0, 1 };
    }
}
