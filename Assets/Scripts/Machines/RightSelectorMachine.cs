using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSelectorMachine : AbstractSelectorMachine
{
    public override MachineType GetMachineType() {
        return MachineType.RIGHT_SELECTOR;
    }

    public override bool isLeftEnabled()
    {
        return false;
    }

    public override bool isRightEnabled()
    {
        return true;
    }
}
