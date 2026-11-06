using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private PlayerCombat playerCombat;

    private void Awake()
    {
        playerCombat = GetComponentInParent<PlayerCombat>();
    }

    public void Attack()
    {
        playerCombat.Attack();
    }
}