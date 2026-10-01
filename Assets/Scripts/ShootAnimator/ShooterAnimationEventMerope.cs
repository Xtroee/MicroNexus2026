using UnityEngine;

public class ShooterAnimationEventMerope : MonoBehaviour
{
    [SerializeField] private BasicShooterMeropeSoldier shooter;

    // DIPANGGIL DARI ANIMATION EVENT
    public void FireProjectile()
    {
        if (shooter != null)
            shooter.FireProjectileFromAnimation();
    }
}
