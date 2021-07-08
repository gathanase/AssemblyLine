using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : MonoBehaviour
{
    public ArtifactType type;
    public Vector2Int position;
    public Direction direction;
    private static ArtifactSprites artifactSprites = null;
    private static ArtifactDatabase artifactDatabase = null;

    void Awake() {
        if (artifactSprites == null) {
            artifactSprites = FindObjectOfType<ArtifactSprites>();
        }
        if (artifactDatabase == null) {
            artifactDatabase = FindObjectOfType<ArtifactDatabase>();
        }
    }

    public void Init(Vector2Int position, Direction direction, ArtifactType type)
    {
        this.position = position;
        this.type = type;
        this.direction = direction;
        transform.position = new Vector3(position.x, position.y, 0);
        GetComponent<SpriteRenderer>().sprite = artifactSprites.GetSprite(this.type);
    }

    void Update()
    {
        transform.Translate(direction.ToVector2() * Time.deltaTime, Space.World);
    }

    public void OnTick() {
        position += direction.ToVector2Int();
        transform.position = new Vector3(position.x, position.y, 0);
    }
    
    public ArtifactDatabase.ArtifactInfo GetInfo() {
        return artifactDatabase.GetInfo(type);
    }
}
