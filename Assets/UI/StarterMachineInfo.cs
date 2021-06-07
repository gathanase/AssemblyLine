using System.Collections;
using System.Collections.Generic;
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
        quantity.value = starterMachine.quantity.ToString();
        type.value = starterMachine.artifactType.ToString();
    }

    void OnEnable() {
        var root = GetComponent<UIDocument>().rootVisualElement;
        quantity = root.Q<DropdownField>("quantity");
        type = root.Q<DropdownField>("type");
    }
}
