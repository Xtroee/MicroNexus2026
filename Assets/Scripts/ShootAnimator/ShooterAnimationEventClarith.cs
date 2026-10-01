using UnityEngine;

public class ShooterAnimationEventClarith : MonoBehaviour
{
    [SerializeField] private BasicShooterClarithSoldier shooter;

    public void FireProjectile()
    {
        if (shooter != null)
            shooter.FireProjectileFromAnimation();
    }
}
