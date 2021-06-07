using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Machine : MonoBehaviour
{
    public Vector2Int position;
    public Direction direction;
    protected static GameController gameController = null;

    void Awake() {
        if (gameController == null) {
            gameController = FindObjectOfType<GameController>();
        }
    }

    public void Init(Vector2Int position, Direction direction)
    {
        transform.position = new Vector3(position.x, position.y, 0);
        transform.rotation = Quaternion.Euler(0, 0, ((int)direction) * 90);
        this.position = position;
        this.direction = direction;
    }

    public abstract void Feed(Artifact artifact);
    public abstract void OnTick();
    public void Rotate(int count) {
        direction = direction.Rotate(count);
        transform.rotation = Quaternion.Euler(0, 0, ((int)direction) * 90);
    }

    protected void Add(ArtifactType type, Direction direction) {
        gameController.Add(type, position, direction);
    }

    protected void Remove(Artifact artifact) {
        gameController.Remove(artifact);
    }
}