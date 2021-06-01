using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceMachine : TransformMachine
{
    protected override Dictionary<ArtifactType, ArtifactType> BuildMapping() {
        Dictionary<ArtifactType, ArtifactType> mapping = new Dictionary<ArtifactType, ArtifactType>();
        mapping.Add(ArtifactType.IRON, ArtifactType.IRON_LIQUID);
        mapping.Add(ArtifactType.ALUMINUM, ArtifactType.ALUMINUM_LIQUID);
        mapping.Add(ArtifactType.COPPER, ArtifactType.COPPER_LIQUID);
        mapping.Add(ArtifactType.DIAMOND, ArtifactType.DIAMOND_LIQUID);
        mapping.Add(ArtifactType.GOLD, ArtifactType.GOLD_LIQUID);
        return mapping;
    }
}
