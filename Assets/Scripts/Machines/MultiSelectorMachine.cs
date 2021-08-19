using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSelectorMachine : AbstractSelectorMachine
{
    public override MachineType GetMachineType() {
        return MachineType.MULTI_SELECTOR;
    }

    public override bool isLeftEnabled()
    {
        return true;
    }

    public override bool isRightEnabled()
    {
        return true;
    }

}
