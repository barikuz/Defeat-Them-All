using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    // Singleton instance so other scripts can easily access the GameManager
    public static GameManager Instance;

    [Header("Level Status")]
    [SerializeField] private int totalEnemies;
    [SerializeField] private GameObject exitDoor; // Reference to the door object

    void Awake()
    {
        // Set up the singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Automatically count how many enemies are tagged as "Enemy" in the scene
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

    }

    // Called whenever an enemy is defeated
    public void EnemyDefeated()
    {
        totalEnemies--;

        // Check if all enemies are cleared
        if (totalEnemies <= 0)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        Debug.Log("All enemies cleared! The door is now open.");

        if (exitDoor != null)
        {
            // Change the tilemap color to indicate that the door is open.
            Tilemap doorTilemap = exitDoor.GetComponent<Tilemap>();
            if (doorTilemap != null)
            {
                doorTilemap.color = Color.white;

                foreach (Vector3Int position in doorTilemap.cellBounds.allPositionsWithin)
                {
                    if (doorTilemap.HasTile(position))
                    {
                        doorTilemap.RemoveTileFlags(position, TileFlags.LockColor);
                        doorTilemap.SetColor(position, Color.cyan);
                    }
                }
            }
        }
    }
}