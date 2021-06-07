using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class StarterMachineInfo : MonoBehaviour
{
    private Label label;
    private DropdownField quantity;
    private DropdownField type;
    private StarterMachine starterMachine;

    public void Init(StarterMachine starterMachine) {
        this.starterMachine = starterMachine;
        quantity.SetValueWithoutNotify(starterMachine.quantity.ToString());
        type.SetValueWithoutNotify(starterMachine.artifactType.ToString());

        quantity.RegisterValueChangedCallback(ev => {
            switch (ev.newValue) {
                case "1": starterMachine.quantity = 1; break;
                case "2": starterMachine.quantity = 2; break;
                case "3": starterMachine.quantity = 3; break;
                default: starterMachine.quantity = 0; break;
        }});

        type.RegisterValueChangedCallback(ev =>
            starterMachine.artifactType = (ArtifactType) Enum.Parse(typeof(ArtifactType), ev.newValue, true)
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
        type = new DropdownField("type", typeChoices, 0);
        root.Add(type);
        List<String> quantityChoices = new List<String>() { "None", "1", "2", "3" };
        quantity = new DropdownField("quantity", quantityChoices, 0);
        root.Add(quantity);
    }
}
