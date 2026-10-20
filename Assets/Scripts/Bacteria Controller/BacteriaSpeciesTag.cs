using UnityEngine;

/// <summary>
/// Put on the stage 7-13 bacteria prefabs (Klebsiella ESBL, N. gonorrhoeae, VRE, H. pylori, S. pyogenes,
/// CRAB, C. trachomatis). These prefabs still carry the BacteriaController of the original bacterium they
/// were recoloured from, so the spawners, Nucleus, shooters and wave counting keep working unchanged.
/// Projectiles look for this tag first and use the 14x14 matrix (AntibioticDamage) for the real damage.
/// </summary>
public class BacteriaSpeciesTag : MonoBehaviour
{
    public AntibioticDamage.Species species;

    /// <summary>Forwards the damage to whichever BacteriaController this prefab carries.</summary>
    public void ProjectileHit(int damage)
    {
        if (TryGetComponent<BacteriaControllerPurple>(out var purple)) { purple.ProjectileHit(damage); return; }
        if (TryGetComponent<BacteriaControllerRed>(out var red)) { red.ProjectileHit(damage); return; }
        if (TryGetComponent<BacteriaControllerYellow>(out var yellow)) { yellow.ProjectileHit(damage); return; }
        if (TryGetComponent<BacteriaControllerPink>(out var pink)) { pink.ProjectileHit(damage); return; }
        if (TryGetComponent<BacteriaControllerOrange>(out var orange)) { orange.ProjectileHit(damage); return; }
        if (TryGetComponent<BacteriaControllerGreen>(out var green)) { green.ProjectileHit(damage); return; }
        if (TryGetComponent<BacteriaControllerBlue>(out var blue)) { blue.ProjectileHit(damage); return; }
    }
}
