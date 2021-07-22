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

    public class Save {
        public string type;
        public int x, y;
        public string direction;

        public Artifact Load(GameDatabase gameDatabase) {
            ArtifactType type = ArtifactTypeExtensions.Parse(this.type);
            Vector2Int position = new Vector2Int(this.x, this.y);
            Direction direction = DirectionExtensions.Parse(this.direction);
            Artifact artifact = Instantiate(gameDatabase.GetModel(type));
            artifact.Init(position, direction, type);
            return artifact;
        }
    }

    public Save ToSave() {
        Save save = new Save();
        save.type = type.ToString();
        save.x = position.x;
        save.y = position.y;
        save.direction = direction.ToString();
        return save;
    }

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
