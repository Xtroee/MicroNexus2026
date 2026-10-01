using UnityEngine;

public class ShooterAnimationEventPenicilin : MonoBehaviour
{
    [SerializeField] private BasicShooterPenicilinSoldier shooter;

    public void FireProjectile()
    {
        if (shooter != null)
            shooter.FireProjectileFromAnimation();
    }
}
