using UnityEngine;
using System.Collections;

public class BasicShooterCeftriaSoldier : MonoBehaviour
{
    [Header("Tile Ownership")]
    public Tile ownerTile;

    [Header("Shooting")]
    public GameObject projectile;
    public Transform shootOrigin;
    public float cooldown = 1f;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Detection (LINE CHECK)")]
    public float range = 11f;
    public LayerMask shootMask;

    private bool hasTriggered = false;
    private bool canShoot = true;
    private Collider2D myCollider;
    private DefenderHealth health;

    private RaycastHit2D runtimeHit;

    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();
        health = gameObject.AddComponent<DefenderHealth>();
        health.OnDeath += CleanupShooter;

        if (animator == null)
            Debug.LogWarning("[BasicShooterCeftria] Animator belum di-assign (harus child Sprite).");
    }

    private void Update()
    {
        if (hasTriggered) return;
        if (ownerTile == null) return;
        if (!canShoot) return;

        runtimeHit = Physics2D.Raycast(transform.position, Vector2.right, range, shootMask);

        if (runtimeHit.collider != null)
            TryShoot();
    }

    private void TryShoot()
    {
        if (!canShoot || hasTriggered)
            return;

        canShoot = false;

        if (animator != null)
            animator.SetTrigger("Shoot");
    }

    public void FireProjectileFromAnimation()
    {
        if (hasTriggered) return;

        if (projectile != null && shootOrigin != null)
            Instantiate(projectile, shootOrigin.position, Quaternion.identity);

        StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(cooldown);

        if (!hasTriggered)
            canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTriggered) return;

        if (IsBacteria(collision))
        {
            EngageAttacker(collision.gameObject);
            return;
        }

        if (IsOtherEnemy(collision))
            CleanupShooter();
    }

    private bool IsBacteria(Collider2D col)
    {
        return
            col.GetComponent<BacteriaControllerBlue>() != null ||
            col.GetComponent<BacteriaControllerRed>() != null ||
            col.GetComponent<BacteriaControllerGreen>() != null ||
            col.GetComponent<BacteriaControllerYellow>() != null ||
            col.GetComponent<BacteriaControllerPink>() != null ||
            col.GetComponent<BacteriaControllerOrange>() != null ||
            col.GetComponent<BacteriaControllerPurple>() != null;
    }

    private bool IsOtherEnemy(Collider2D col)
    {
        return
            col.GetComponent<VirusController>() != null ||
            col.GetComponent<MushroomController>() != null ||
            col.GetComponent<ProtozoaController>() != null ||
            col.GetComponent<HelminthController>() != null;
    }

    private void EngageAttacker(GameObject enemy)
    {
        BacteriaMeleeAttacker attacker = enemy.GetComponent<BacteriaMeleeAttacker>();
        if (attacker == null)
            attacker = enemy.AddComponent<BacteriaMeleeAttacker>();

        attacker.Engage(health);
    }

    private void CleanupShooter()
    {
        if (hasTriggered) return;
        hasTriggered = true;

        canShoot = false;

        if (myCollider != null)
            myCollider.enabled = false;

        if (ownerTile != null)
        {
            ownerTile.hasCapsule = false;
            ownerTile.currentCapsule = null;
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Vector3 start = transform.position;
        Vector3 end = start + Vector3.right * range;

        Gizmos.color = new Color(0.5f, 0.2f, 0.6f);
        Gizmos.DrawLine(start, end);

        if (Application.isPlaying && runtimeHit.collider != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(start, runtimeHit.point);
            Gizmos.DrawSphere(runtimeHit.point, 0.1f);
        }
    }
}
