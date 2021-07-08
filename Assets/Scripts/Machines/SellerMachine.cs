using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerMachine : Machine
{
    public override MachineType GetMachineType()
    {
        return MachineType.SELLER;
    }
    
    public override Window CreateInfoWindow() {
        return null;
    }

    public override void Feed(Artifact artifact)
    {
        gameController.Sell(artifact);
    }

    public override void OnTick() {}
}
