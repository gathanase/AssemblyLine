using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressMachine : TransformMachine
{
    protected override Dictionary<ArtifactType, ArtifactType> BuildMapping() {
        Dictionary<ArtifactType, ArtifactType> mapping = new Dictionary<ArtifactType, ArtifactType>();
        mapping.Add(ArtifactType.IRON, ArtifactType.IRON_PLATE);
        mapping.Add(ArtifactType.ALUMINUM, ArtifactType.ALUMINUM_PLATE);
        mapping.Add(ArtifactType.COPPER, ArtifactType.COPPER_PLATE);
        mapping.Add(ArtifactType.DIAMOND, ArtifactType.DIAMOND_PLATE);
        mapping.Add(ArtifactType.GOLD, ArtifactType.GOLD_PLATE);
        return mapping;
    }
}
