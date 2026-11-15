using UnityEngine;

public class MobileControlsController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCombat playerCombat;

    private void Awake()
    {
        bool isMobile =
            Application.platform == RuntimePlatform.Android ||
            Application.isEditor;

        gameObject.SetActive(isMobile);

        if (joystick == null)
        {
            Debug.LogError("Joystick atanmadı.", this);
        }

        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement atanmadı.", this);
        }

        if (playerCombat == null)
        {
            Debug.LogError("PlayerCombat atanmadı.", this);
        }
    }

    private void Update()
    {
        if (joystick == null || playerMovement == null)
        {
            return;
        }

        Vector2 input = new Vector2(joystick.Horizontal, 0f);
        playerMovement.MobileMoveInput = input;
    }

    public void Jump()
    {
        if (playerMovement != null)
        {
            playerMovement.TryJump();
        }
    }

    public void Attack()
    {
        if (playerCombat != null)
        {
            playerCombat.TryAttack();
        }
    }
}