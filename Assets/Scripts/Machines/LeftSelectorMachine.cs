using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSelectorMachine : AbstractSelectorMachine
{
    public override MachineType GetMachineType() {
        return MachineType.LEFT_SELECTOR;
    }

    public override bool isLeftEnabled()
    {
        return true;
    }

    public override bool isRightEnabled()
    {
        return false;
    }
}
