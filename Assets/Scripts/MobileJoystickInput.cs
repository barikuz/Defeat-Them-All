using UnityEngine;

public class MobileJoystickInput : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private PlayerMovement playerMovement;

    private void Awake()
    {
        if (playerMovement == null)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        if (joystick == null)
        {
            Debug.LogError("MobileJoystickInput: Joystick atanmadı.", this);
        }
    }

    private void Update()
    {
        if (joystick == null || playerMovement == null)
        {
            return;
        }

        Vector2 input = new Vector2(joystick.Horizontal, 0f);
        playerMovement.SetMobileMoveInput(input);
    }
}