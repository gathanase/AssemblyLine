using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    protected static GameController gameController = null;
    protected static ArtifactSprites artifactSprites = null;

    public void Init() {
        if (artifactSprites == null) {
            artifactSprites = FindObjectOfType<ArtifactSprites>();
        }
        if (gameController == null) {
            gameController = FindObjectOfType<GameController>();
        }
        gameController.infoWindow = this;
        gameObject.SetActive(true);
    }

    public void Close() {
        gameController.infoWindow = null;
        gameObject.SetActive(false);
    }
}