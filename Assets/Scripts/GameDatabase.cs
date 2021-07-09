using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDatabase : MonoBehaviour
{
    public Artifact artifactModel;
    public RecipeDatabase recipeDatabase;
    public MachineDatabase machineDatabase;
    public ArtifactDatabase artifactDatabase;
    public MachineSprites machineSprites;
    public ArtifactSprites artifactSprites;
    private Dictionary<ToolType, GameTool> gameTools;

    void Awake() {
        recipeDatabase = new RecipeDatabase();
        recipeDatabase.Load();
        gameTools = new Dictionary<ToolType, GameTool>();
        gameTools.Add(ToolType.INFO, FindObjectOfType<InfoTool>());
        gameTools.Add(ToolType.BUILD, FindObjectOfType<BuildTool>());
        gameTools.Add(ToolType.DELETE, FindObjectOfType<DeleteTool>());
        gameTools.Add(ToolType.ROTATE, FindObjectOfType<RotateTool>());
        gameTools.Add(ToolType.MOVE, FindObjectOfType<MoveTool>());
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
        return artifactModel;
    }

    public List<Recipe> GetRecipes() {
        return recipeDatabase.recipes;
    }

    public List<GameTool> GetTools() {
        return new List<GameTool>(gameTools.Values);
    }

    public GameTool GetTool(ToolType toolType) {
        return gameTools[toolType];
    }
}
