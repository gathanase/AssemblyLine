using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrafterMachine : Machine
{
    public Recipe recipe;
    private ArtifactStock stock;

    void Start() {
        stock = new ArtifactStock();
        recipe = gameDatabase.GetRecipes()[0];
    }

    public override MachineType GetMachineType()
    {
        return MachineType.CRAFTER;
    }

    public new class Save : Machine.Save {
        public string output;
        public Dictionary<string, int> stock;
    }

    public override Machine.Save ToSave()
    {
        Save save = new Save();
        base.WriteSave(save);
        save.output = recipe.output.ToString();
        List<ArtifactType> types = stock.GetArtifactTypes();
        save.stock = types.ToDictionary(type => type.ToString(), type => stock.GetCount(type));
        return save;
    }

    public override Window CreateInfoWindow() {
        CrafterMachineWindow infoWindow = FindObjectOfType<CrafterMachineWindow>(true);
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
