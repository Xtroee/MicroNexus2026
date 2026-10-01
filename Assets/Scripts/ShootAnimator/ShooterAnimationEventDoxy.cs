using UnityEngine;

public class ShooterAnimationEventDoxy : MonoBehaviour
{
    [SerializeField] private BasicShooterDoxySoldier shooter;

    public void FireProjectile()
    {
        if (shooter != null)
            shooter.FireProjectileFromAnimation();
    }
}
