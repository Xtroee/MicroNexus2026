using UnityEngine;

public class ShooterAnimationEventCeftria : MonoBehaviour
{
    [SerializeField] private BasicShooterCeftriaSoldier shooter;

    public void FireProjectile()
    {
        if (shooter != null)
            shooter.FireProjectileFromAnimation();
    }
}
