using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDatabase : MonoBehaviour
{
    public RecipeDatabase recipeDatabase;
    public MachineDatabase machineDatabase;
    public ArtifactDatabase artifactDatabase;
    public MachineSprites machineSprites;
    public ArtifactSprites artifactSprites;

    void Awake() {
        recipeDatabase = new RecipeDatabase();
        recipeDatabase.Load();
    }

    public Sprite GetSprite(MachineType machineType) {
        return machineSprites.GetSprite(machineType);
    }

    public Sprite GetSprite(ArtifactType artifactType) {
        return artifactSprites.GetSprite(artifactType);
    }

    public MachineDatabase.MachineInfo GetInfo(MachineType machineType) {
        return machineDatabase.GetInfo(machineType);
    }

    public ArtifactDatabase.ArtifactInfo GetInfo(ArtifactType artifactType) {
        return artifactDatabase.GetInfo(artifactType);
    }

    public Machine GetModel(MachineType machineType) {
        return machineDatabase.GetModel(machineType);
    }

    public Artifact GetModel(ArtifactType artifactType) {
        return artifactDatabase.GetModel(artifactType);
    }

    public List<Recipe> GetRecipes() {
        return recipeDatabase.recipes;
    }
}
