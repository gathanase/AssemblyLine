using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrafterMachine : Machine
{
    public ArtifactType output;
    private ArtifactStock stock;

    public CrafterMachine() {
        stock = new ArtifactStock();
        output = ArtifactType.AI_ROBOT;
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
        save.output = output.ToString();
        List<ArtifactType> types = stock.GetArtifactTypes();
        save.stock = types.ToDictionary(type => type.ToString(), type => stock.GetCount(type));
        return save;
    }

    public override void Init(Machine.Save _save, FactoryFloor floor, GameDatabase gameDatabase)
    {
        Save save = (Save) _save;
        base.Init(save, floor, gameDatabase);
        output = ArtifactTypeExtensions.Parse(save.output);
        save.stock.ToList().ForEach(kv => stock.Add(ArtifactTypeExtensions.Parse(kv.Key), kv.Value));
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

    public Recipe GetRecipe() {
        return gameDatabase.recipeDatabase.recipes.Find(recipe => recipe.output == output);
    }

    public override void OnTick() {
        Recipe recipe = GetRecipe();
        if (stock.Contains(recipe.inputs)) {
            stock.RemoveAll(recipe.inputs);
            Add(recipe.output, this.direction);
        }
    }
}
