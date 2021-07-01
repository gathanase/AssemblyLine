using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    protected static GameController gameController = null;
    protected static ArtifactSprites artifactSprites = null;

    public virtual void Init() {
        if (artifactSprites == null) {
            artifactSprites = FindObjectOfType<ArtifactSprites>();
        }
        if (gameController == null) {
            gameController = FindObjectOfType<GameController>();
        }
        gameObject.SetActive(true);
    }

    public void Close() {
        gameObject.SetActive(false);
    }
}