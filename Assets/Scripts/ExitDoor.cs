using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement; // Required to load scenes

public class ExitDoor : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string nextSceneName = "Level2"; // Name of the next level scene

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the player
        if (collision.CompareTag("Player"))
        {
            // Check if all door tiles have changed to cyan (indicating it is open).
            Tilemap doorTilemap = GetComponent<Tilemap>();
            if (doorTilemap != null && IsDoorOpen(doorTilemap))
            {
                Debug.Log("Level completed! Loading next level...");
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    private bool IsDoorOpen(Tilemap doorTilemap)
    {
        bool hasTiles = false;

        foreach (Vector3Int position in doorTilemap.cellBounds.allPositionsWithin)
        {
            if (!doorTilemap.HasTile(position))
            {
                continue;
            }

            hasTiles = true;
            if (doorTilemap.GetColor(position) != Color.cyan)
            {
                return false;
            }
        }

        return hasTiles;
    }
}