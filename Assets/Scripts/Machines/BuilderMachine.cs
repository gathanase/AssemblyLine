using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderMachine : Machine
{
    public Recipe recipe;
    private ArtifactStock stock;

    void Start() {
        stock = new ArtifactStock();
        recipe = gameController.recipeDatabase.recipes[0];
    }

    public override Window CreateInfoWindow() {
        BuilderMachineWindow infoWindow = FindObjectOfType<BuilderMachineWindow>(true);
        infoWindow.Init(this);
        return infoWindow;
    }

    public override void Feed(Artifact artifact) {
        stock.Add(artifact.type, 1);
        Remove(artifact);
    }

    public override void OnTick() {
        if (recipe != null) {
            if (stock.Contains(recipe.inputs)) {
                stock.RemoveAll(recipe.inputs);
                Add(recipe.output, this.direction);
            }
        }
    }
}
