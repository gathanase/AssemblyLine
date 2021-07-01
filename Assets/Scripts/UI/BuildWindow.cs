using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildWindow : Window
{
    public GameObject content;
    public Button closeButton;
    public Button templateButton;
    private MachineSprites machineSprites;
    private MachineDatabase machineDatabase;
    private BuildTool buildTool;

    public override void Init() {
        base.Init();        
        machineSprites = FindObjectOfType<MachineSprites>(true);
        machineDatabase = FindObjectOfType<MachineDatabase>(true);
        buildTool = FindObjectOfType<BuildTool>(true);
        Add(MachineType.STARTER);
        Add(MachineType.ROLLER);
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(Close);
    }

    private void Add(MachineType machineType) {
        MachineDatabase.MachineInfo machineInfo = machineDatabase.GetInfo(machineType);
        Button button = Instantiate(templateButton, content.transform);
        button.transform.Find("Image").GetComponent<Image>().sprite = machineSprites.GetSprite(machineType);
        button.transform.Find("Label").GetComponent<Text>().text = machineInfo.name;
        button.onClick.AddListener(() => {
            this.Close();
            buildTool.SetMachineType(machineType);
            gameController.SetTool(ToolType.BUILD);
        });
        button.gameObject.SetActive(true);
    }
}
