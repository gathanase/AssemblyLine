using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiSelectorMachine : AbstractSelectorMachine
{
    public override MachineType GetMachineType() {
        return MachineType.BI_SELECTOR;
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
