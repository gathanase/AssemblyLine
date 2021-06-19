using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Toolbar : MonoBehaviour
{
    public Button infoButton;
    public Button buildButton;
    public Button rotateButton;
    public GameController gameController;

    void Awake() {
        infoButton.onClick.AddListener(() => gameController.SetTool(GameTool.INFO));
        buildButton.onClick.AddListener(() => gameController.SetTool(GameTool.BUILD));
        rotateButton.onClick.AddListener(() => gameController.SetTool(GameTool.ROTATE));
    }
}
