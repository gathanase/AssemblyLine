using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildWindow : Window
{
    public GameObject content;
    public Button closeButton;
    public Button templateButton;
    private BuildTool buildTool;

    public override void Init() {
        base.Init();        
        buildTool = FindObjectOfType<BuildTool>(true);
        Add(MachineType.STARTER);
        Add(MachineType.SELLER);
        Add(MachineType.ROLLER);
        Add(MachineType.CRAFTER);
        Add(MachineType.CUTTER);
        Add(MachineType.FURNACE);
        Add(MachineType.HYDRAULIC_PRESS);
        Add(MachineType.WIRE_DRAWER);
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(Close);
    }

    private void Add(MachineType machineType) {
        MachineDatabase.MachineInfo machineInfo = gameDatabase.GetInfo(machineType);
        Button button = Instantiate(templateButton, content.transform);
        button.transform.Find("Image").GetComponent<Image>().sprite = gameDatabase.GetSprite(machineType);
        button.transform.Find("Label").GetComponent<Text>().text = machineInfo.name;
        button.onClick.AddListener(() => {
            this.Close();
            buildTool.SetMachineType(machineType);
            gameController.SetTool(ToolType.BUILD);
        });
        button.gameObject.SetActive(true);
    }
}
