using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    private EnemyCombat enemyCombat;

    private void Awake()
    {
        enemyCombat = GetComponentInParent<EnemyCombat>();
    }

    public void DealAttackDamage()
    {
        enemyCombat.DealAttackDamage();
    }
}