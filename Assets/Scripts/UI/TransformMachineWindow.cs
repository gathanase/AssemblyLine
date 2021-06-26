using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TransformMachineWindow : Window
{
    // public Dropdown typeField;
    // public Slider quantityField;
    // public List<ArtifactType> artifactTypes;
    public Button closeButton;
    public GameObject stockPanel;
    public GameObject template;
    private TransformMachine transformMachine;
    private Queue<ArtifactType> stock;

    public void Init(TransformMachine transformMachine) {
        Init();
        this.transformMachine = transformMachine;
        this.stock = new Queue<ArtifactType>();
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(Close);
    }

    public void Update() {
        if (Enumerable.SequenceEqual(stock, transformMachine.queue)) {
            return;
        }
        stock = new Queue<ArtifactType>(transformMachine.queue);
        // remove old sprites
        foreach (Transform child in stockPanel.transform) {
            if (child.gameObject.activeSelf) {
                Destroy(child.gameObject);
            }
        }
        // add new sprites
        foreach (ArtifactType type in stock) {
            GameObject image = Instantiate(template, Vector3.zero, Quaternion.identity, stockPanel.transform);
            image.GetComponent<Image>().sprite = artifactSprites.GetSprite(type);
            image.SetActive(true);
        }
    }
}