using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class StarterMachineWindow : Window
{
    private DropdownField quantityField;
    private DropdownField typeField;
    private Button closeButton;
    private StarterMachine starterMachine;

    public void Init(StarterMachine starterMachine, GameController gameController) {
        Init(gameController);
        this.starterMachine = starterMachine;
        quantityField.SetValueWithoutNotify(starterMachine.quantity.ToString());
        typeField.SetValueWithoutNotify(starterMachine.artifactType.ToString());

        quantityField.RegisterValueChangedCallback(ev => {
            switch (ev.newValue) {
                case "1": starterMachine.quantity = 1; break;
                case "2": starterMachine.quantity = 2; break;
                case "3": starterMachine.quantity = 3; break;
                default: starterMachine.quantity = 0; break;
        }});

        typeField.RegisterValueChangedCallback(ev =>
            starterMachine.artifactType = ArtifactTypeExtensions.Parse(ev.newValue)
        );
    }

    void OnEnable() {
        var root = GetComponent<UIDocument>().rootVisualElement;
        //quantity = root.Q<DropdownField>("quantity");
        //type = root.Q<DropdownField>("type");

        root.Clear();
        root.Add(new Label("Starter Machine"));
        List<String> typeChoices = new List<ArtifactType>() {
            ArtifactType.IRON, ArtifactType.ALUMINUM, ArtifactType.GOLD, ArtifactType.COPPER, ArtifactType.DIAMOND }
            .ConvertAll<String>(type => type.ToString());
        typeField = new DropdownField("type", typeChoices, 0);
        root.Add(typeField);
        List<String> quantityChoices = new List<String>() { "None", "1", "2", "3" };
        quantityField = new DropdownField("quantity", quantityChoices, 0);
        root.Add(quantityField);
        closeButton = new Button(Close);
        closeButton.text = "close";
        root.Add(closeButton);
    }
}
