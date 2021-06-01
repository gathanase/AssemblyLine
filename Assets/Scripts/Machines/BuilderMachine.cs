using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderMachine : Machine
{
    public Recipe recipe;
    private ArtifactStock stock;

    void Start() {
        stock = new ArtifactStock();
    }

    public override void Feed(Artifact artifact) {
        stock.Add(artifact.type, 1);
    }

    public override void OnTick() {
        if (stock.Contains(recipe.inputs)) {
            stock.RemoveAll(recipe.inputs);
            Add(recipe.output, this.direction);
        }
    }
}
