using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutterMachine : TransformMachine
{
    protected override Dictionary<ArtifactType, ArtifactType> BuildMapping() {
        Dictionary<ArtifactType, ArtifactType> mapping = new Dictionary<ArtifactType, ArtifactType>();
        mapping.Add(ArtifactType.IRON, ArtifactType.IRON_GEAR);
        mapping.Add(ArtifactType.ALUMINUM, ArtifactType.ALUMINUM_GEAR);
        mapping.Add(ArtifactType.COPPER, ArtifactType.COPPER_GEAR);
        mapping.Add(ArtifactType.DIAMOND, ArtifactType.DIAMOND_GEAR);
        mapping.Add(ArtifactType.GOLD, ArtifactType.GOLD_GEAR);
        return mapping;
    }
}
