using UnityEngine;

public class ShooterAnimationEventLinezo : MonoBehaviour
{
    [SerializeField] private BasicShooterLinezoSoldier shooter;

    public void FireProjectile()
    {
        if (shooter != null)
            shooter.FireProjectileFromAnimation();
    }
}
