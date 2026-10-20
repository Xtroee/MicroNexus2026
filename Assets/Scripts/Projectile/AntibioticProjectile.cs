using UnityEngine;

/// <summary>
/// Straight-flying capsule used by the iterasi 2 characters (Ceftria, Clarith, Colistin, Doxy, Linezo,
/// Penicilin). Same behaviour as MeropeProjectile, but the damage per bacteria is set on the prefab
/// instead of one hard-coded class per character.
/// Values come from docs/antibiotic-bacteria-matrix-full-14x14.md (scale 0-20):
/// Purple = S. pneumoniae, Yellow = MSSA, Orange = MRSA, Red = M. catarrhalis,
/// Blue = H. influenzae, Pink = E. coli, Green = P. aeruginosa.
/// </summary>
public class AntibioticProjectile : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;

    [Header("Who is firing (used for the stage 7-13 bacteria)")]
    [SerializeField] private AntibioticDamage.Attacker attacker;

    [Header("Damage per bacteria (0-20)")]
    [SerializeField] private int damagePurple;
    [SerializeField] private int damageYellow;
    [SerializeField] private int damageOrange;
    [SerializeField] private int damageRed;
    [SerializeField] private int damageBlue;
    [SerializeField] private int damagePink;
    [SerializeField] private int damageGreen;

    [Header("Damage Virus")]
    [SerializeField] private int damageVirus = 1;

    [Header("Damage Other Enemies")]
    [SerializeField] private int damageMushroom = 1;
    [SerializeField] private int damageProtozoa = 1;
    [SerializeField] private int damageHelminth = 1;

    [Header("Lifetime")]
    [SerializeField] private float lifeTime = 25f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<BacteriaSpeciesTag>(out var speciesTag))
        {
            Hit(() => speciesTag.ProjectileHit(AntibioticDamage.Get(attacker, speciesTag.species)));
            return;
        }

        if (other.TryGetComponent<BacteriaControllerPurple>(out var purple))
        {
            Hit(() => purple.ProjectileHit(damagePurple));
            return;
        }

        if (other.TryGetComponent<BacteriaControllerBlue>(out var blue))
        {
            Hit(() => blue.ProjectileHit(damageBlue));
            return;
        }

        if (other.TryGetComponent<BacteriaControllerRed>(out var red))
        {
            Hit(() => red.ProjectileHit(damageRed));
            return;
        }

        if (other.TryGetComponent<BacteriaControllerPink>(out var pink))
        {
            Hit(() => pink.ProjectileHit(damagePink));
            return;
        }

        if (other.TryGetComponent<BacteriaControllerGreen>(out var green))
        {
            Hit(() => green.ProjectileHit(damageGreen));
            return;
        }

        if (other.TryGetComponent<BacteriaControllerYellow>(out var yellow))
        {
            Hit(() => yellow.ProjectileHit(damageYellow));
            return;
        }

        if (other.TryGetComponent<BacteriaControllerOrange>(out var orange))
        {
            Hit(() => orange.ProjectileHit(damageOrange));
            return;
        }

        if (other.TryGetComponent<VirusController>(out var virus))
        {
            Hit(() => virus.ProjectileHit(damageVirus));
            return;
        }

        if (other.TryGetComponent<MushroomController>(out var mushroom))
        {
            Hit(() => mushroom.ProjectileHit(damageMushroom));
            return;
        }

        if (other.TryGetComponent<ProtozoaController>(out var protozoa))
        {
            Hit(() => protozoa.ProjectileHit(damageProtozoa));
            return;
        }

        if (other.TryGetComponent<HelminthController>(out var helminth))
        {
            Hit(() => helminth.ProjectileHit(damageHelminth));
        }
    }

    private void Hit(System.Action applyDamage)
    {
        SoundManager.Instance?.PlayYellowHit();
        applyDamage();
        Destroy(gameObject);
    }
}
