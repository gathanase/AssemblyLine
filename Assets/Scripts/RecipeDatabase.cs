using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RecipeDatabase
{
    public List<Recipe> recipes;

    [System.Serializable]
    private class InputJson {
        public string type;
        public int quantity;
    }

    [System.Serializable]
    private class RecipeJson {
        public string output;
        public List<InputJson> inputs;
    }

    [System.Serializable]
    private class RecipesJson {
        public List<RecipeJson> recipes;
    }

    public void Load() {
        Debug.Log("Loading recipes");
        TextAsset file = Resources.Load<TextAsset>("Recipes");
        RecipesJson recipesJson = JsonUtility.FromJson<RecipesJson>(file.text);
        recipes = recipesJson.recipes.ConvertAll(recipeJson => ToModel(recipeJson));
    }

    private Recipe ToModel(RecipeJson recipeJson) {
        Recipe model = new Recipe();
        model.output = ArtifactTypeExtensions.Parse(recipeJson.output);
        model.inputs = new ArtifactStock();
        recipeJson.inputs.ForEach(inputJson => 
            model.inputs.Add(ArtifactTypeExtensions.Parse(inputJson.type), inputJson.quantity)
        );
        return model;
    }
}
