using UnityEngine;

public class ShooterAnimationEventColistin : MonoBehaviour
{
    [SerializeField] private BasicShooterColistinSoldier shooter;

    public void FireProjectile()
    {
        if (shooter != null)
            shooter.FireProjectileFromAnimation();
    }
}
