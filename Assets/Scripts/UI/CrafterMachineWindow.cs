using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrafterMachineWindow : Window
{
    public Dropdown typeField;
    public Button closeButton;
    private List<Recipe> recipes;
    private List<ArtifactType> artifactTypes;
    private CrafterMachine crafterMachine;

    public void Init(CrafterMachine crafterMachine) {
        Init();
        this.crafterMachine = crafterMachine;
        this.recipes = gameController.recipeDatabase.recipes;

        typeField.options = recipes.ConvertAll<Dropdown.OptionData>(recipe => {
            ArtifactType type = recipe.output;
            return new Dropdown.OptionData(type.ToString(), artifactSprites.GetSprite(type));
        });
        typeField.SetValueWithoutNotify(gameController.recipeDatabase.recipes.IndexOf(crafterMachine.recipe));
        typeField.onValueChanged.RemoveAllListeners();
        typeField.onValueChanged.AddListener(ev => crafterMachine.recipe = recipes[typeField.value]);
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(Close);
    }
}