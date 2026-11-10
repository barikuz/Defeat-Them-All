using UnityEngine;

public class KeepHealthBarFixed : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = transform.parent;
    }

    private void LateUpdate()
    {
        transform.localScale = new Vector3(
            Mathf.Sign(player.localScale.x),
            1f,
            1f
        );
    }
}