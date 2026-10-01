using UnityEngine;
using TMPro;

public class BacteriaControllerPink : MonoBehaviour
{
    [Header("Identity")]
    [SerializeField] private string speciesName = "Escherichia coli";

    [Header("Movement")]
    public float speed = 1.5f;

    [Header("Spawn Reference")]
    [HideInInspector] public SpawnPointSlot spawnPoint;

    [Header("Visual")]
    [SerializeField] private GameObject bacteriaVisual;

    [Tooltip("Particle VFX child (awal NONAKTIF)")]
    [SerializeField] private GameObject hitParticleEffect;

    [Header("Stats")]
    public int health = 3;

    [Header("Atom Drop On Death")]
    [SerializeField] private GameObject atomPrefab;

    [Range(0f, 1f)]
    [SerializeField] private float atomDropChance = 1f;

    private bool isHit = false;
    private Collider2D myCollider;
    private ParticleSystem hitPS;
    private TextMeshPro nameLabel;
    private GameObject nameLabelRoot;

    // =========================
    // UNITY EVENTS
    // =========================

    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();

        if (hitParticleEffect != null)
        {
            hitPS = hitParticleEffect.GetComponent<ParticleSystem>();
            hitParticleEffect.SetActive(false);
        }

        Debug.Log($"[DEBUG][Pink] Awake → VFX ready: {hitPS != null}");
    }

    private void FixedUpdate()
    {
        if (isHit) return;

        // Movement
        transform.position -= new Vector3(speed, 0f, 0f);
    }

    // =========================
    // HOVER NAME LABEL
    // =========================

    private void OnMouseEnter()
    {
        if (isHit) return;

        if (nameLabelRoot == null)
        {
            nameLabelRoot = BacteriaLabelFactory.Create("NameLabel", speciesName, Color.white, 1.8f, out nameLabel, out _);
            nameLabelRoot.transform.SetParent(transform);
            nameLabelRoot.transform.localPosition = new Vector3(0f, 0.6f, -1f);
        }

        nameLabelRoot.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (nameLabelRoot != null)
            nameLabelRoot.SetActive(false);
    }

    // =========================
    // MAIN DEATH HANDLER
    // =========================

    public void Hit()
    {
        if (isHit)
        {
            Debug.Log("[DEBUG][Pink] Hit() diabaikan (isHit == true)");
            return;
        }

        isHit = true;

        if (nameLabelRoot != null)
            nameLabelRoot.SetActive(false);

        speed = 0f;

        // Disable collider
        if (myCollider != null)
            myCollider.enabled = false;

        // Hide visual
        if (bacteriaVisual != null)
            bacteriaVisual.SetActive(false);

        // Play VFX
        if (hitParticleEffect != null && hitPS != null)
        {
            hitParticleEffect.SetActive(true);
            hitPS.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            hitPS.Play(true);
        }

        // Drop atom
        TrySpawnAtom();

        // Register kill
        WaveManager manager = FindFirstObjectByType<WaveManager>();
        if (manager != null)
            manager.RegisterKill();

        // Destroy after delay
        Destroy(gameObject, 3f);

        Debug.Log("[DEBUG][Pink] Bakteri akan dihancurkan 3 detik lagi");
    }

    // =========================
    // CLEAR SLOT SAFELY
    // =========================

    private void OnDestroy()
    {
        if (spawnPoint != null)
        {
            spawnPoint.ClearOccupied();
            spawnPoint = null;
        }
    }

    // =========================
    // ATOM DROP
    // =========================

    private void TrySpawnAtom()
    {
        if (atomPrefab == null)
        {
            Debug.LogWarning("[DEBUG][Pink] atomPrefab belum di-assign");
            return;
        }

        if (Random.value > atomDropChance)
        {
            Debug.Log("[DEBUG][Pink] Atom tidak drop (chance gagal)");
            return;
        }

        GameObject atom = Instantiate(
            atomPrefab,
            transform.position,
            Quaternion.identity
        );

        Atom atomScript = atom.GetComponent<Atom>();

        if (atomScript != null)
            atomScript.useRandomSpawn = false;

        Debug.Log("[DEBUG][Pink] Atom drop berhasil (mode bakteri)");
    }

    // =========================
    // PROJECTILE EVENTS
    // =========================

    public void ProjectileHit(int damage)
    {
        if (isHit) return;

        BacteriaEffectivenessPopup.Show(transform.position, damage);

        health -= damage;

        Debug.Log($"[DEBUG][Pink] Damage={damage}, HP sisa={health}");

        if (health <= 0)
            Hit();
    }

    public void ProjectileDead()
    {
        if (isHit) return;

        Debug.Log("[DEBUG][Pink] ProjectileDead()");
        Hit();
    }
}
