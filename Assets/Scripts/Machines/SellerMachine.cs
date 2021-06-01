using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellerMachine : Machine
{
    public override void Feed(Artifact artifact)
    {
        gameController.Sell(artifact);
    }
    public override void OnTick()
    {
        throw new System.NotImplementedException();
    }
}
