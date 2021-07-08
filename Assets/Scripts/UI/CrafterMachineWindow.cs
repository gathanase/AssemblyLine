using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrafterMachineWindow : Window
{
    public Dropdown typeField;
    public Button closeButton;
    private List<Recipe> recipes;
    private ArtifactDatabase artifactDatabase;
    private List<ArtifactType> artifactTypes;
    private CrafterMachine crafterMachine;
    public List<Transform> inputFields;
    public Transform outputField;

    public void Init(CrafterMachine crafterMachine) {
        Init();
        this.crafterMachine = crafterMachine;
        this.recipes = gameController.recipeDatabase.recipes;
        this.artifactDatabase = FindObjectOfType<ArtifactDatabase>(true);

        typeField.options = recipes.ConvertAll<Dropdown.OptionData>(recipe => {
            ArtifactType type = recipe.output;
            return new Dropdown.OptionData(type.ToString(), artifactSprites.GetSprite(type));
        });
        typeField.SetValueWithoutNotify(gameController.recipeDatabase.recipes.IndexOf(crafterMachine.recipe));
        DisplayRecipe();
        typeField.onValueChanged.RemoveAllListeners();
        typeField.onValueChanged.AddListener(ev => {
            crafterMachine.recipe = recipes[typeField.value];
            DisplayRecipe();
        });
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(Close);
    }

    private void DisplayRecipeInput(Recipe recipe, int index, ArtifactType type) {
        int quantity = recipe.inputs.GetCount(type);
        Sprite sprite = artifactSprites.GetSprite(type);
        ArtifactDatabase.ArtifactInfo info = artifactDatabase.GetInfo(type);
        Transform field = inputFields[index];
        field.Find("Sprite").GetComponent<Image>().sprite = sprite;
        field.Find("Label").GetComponent<Text>().text = string.Format("<b>{0}</b>x {1}", quantity, info.name);
        field.gameObject.SetActive(true);
    }

    private void DisplayRecipeOutput(Recipe recipe) {
        Sprite sprite = artifactSprites.GetSprite(recipe.output);
        ArtifactDatabase.ArtifactInfo info = artifactDatabase.GetInfo(recipe.output);
        outputField.Find("Sprite").GetComponent<Image>().sprite = sprite;
        outputField.Find("Label").GetComponent<Text>().text = string.Format(info.name);
    }

    private void DisplayRecipe() {
        Recipe recipe = recipes[typeField.value];
        List<ArtifactType> inputTypes = new List<ArtifactType>(recipe.inputs.GetArtifactTypes());
        int index = 0;
        foreach (Transform field in inputFields) {
            field.gameObject.SetActive(false);
        }
        foreach (ArtifactType type in recipe.inputs.GetArtifactTypes()) {
            DisplayRecipeInput(recipe, index, type);
            index++;
        }
        DisplayRecipeOutput(recipe);
    }
}