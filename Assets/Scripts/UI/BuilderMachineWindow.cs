using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuilderMachineWindow : Window
{
    public Dropdown typeField;
    public Button closeButton;
    private List<Recipe> recipes;
    private List<ArtifactType> artifactTypes;
    private BuilderMachine builderMachine;

    public void Init(BuilderMachine builderMachine) {
        Init();
        this.builderMachine = builderMachine;
        this.recipes = gameController.recipeDatabase.recipes;

        typeField.options = recipes.ConvertAll<Dropdown.OptionData>(recipe => {
            ArtifactType type = recipe.output;
            return new Dropdown.OptionData(type.ToString(), artifactSprites.GetSprite(type));
        });
        typeField.SetValueWithoutNotify(gameController.recipeDatabase.recipes.IndexOf(builderMachine.recipe));
        typeField.onValueChanged.AddListener(ev => builderMachine.recipe = recipes[typeField.value]);
        closeButton.onClick.AddListener(Close);
    }
}