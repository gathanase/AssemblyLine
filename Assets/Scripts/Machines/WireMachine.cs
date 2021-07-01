using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireMachine : TransformMachine
{
    public override MachineType GetMachineType()
    {
        return MachineType.WIRE_DRAWER;
    }
    
    protected override Dictionary<ArtifactType, ArtifactType> BuildMapping() {
        Dictionary<ArtifactType, ArtifactType> mapping = new Dictionary<ArtifactType, ArtifactType>();
        mapping.Add(ArtifactType.IRON, ArtifactType.IRON_WIRE);
        mapping.Add(ArtifactType.ALUMINUM, ArtifactType.ALUMINUM_WIRE);
        mapping.Add(ArtifactType.COPPER, ArtifactType.COPPER_WIRE);
        mapping.Add(ArtifactType.DIAMOND, ArtifactType.DIAMOND_WIRE);
        mapping.Add(ArtifactType.GOLD, ArtifactType.GOLD_WIRE);
        return mapping;
    }
}
