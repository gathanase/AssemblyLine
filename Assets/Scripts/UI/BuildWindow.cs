using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildWindow : Window
{
    public Button closeButton;
    public Button templateButton;
    private Button starterButton;
    private Button rollerButton;

    public override void Init() {
        base.Init();

        this.starterButton = Instantiate(templateButton);
        //this.starterButton.GetComponentInChildren<Image>().sprite = machineSprites

        // starterButton.onClick.RemoveAllListeners();
        // starterButton.onClick.AddListener(() => SetQuantity(starterMachine.quantity - 1));
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(Close);
    }
}
