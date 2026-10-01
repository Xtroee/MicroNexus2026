using UnityEngine;

public class CapsuleOrange : MonoBehaviour
{
    public Tile ownerTile; // diisi GameManager saat spawn capsule

    private bool hasTriggered = false;   // 🔒 pengaman satu kali trigger (hanya di-set saat mati)
    private Collider2D capsuleCollider;
    private DefenderHealth health;

    private void Awake()
    {
        capsuleCollider = GetComponent<Collider2D>();
        health = gameObject.AddComponent<DefenderHealth>();
        health.OnDeath += CleanupCapsule;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 🔒 Capsule tidak boleh bereaksi lagi setelah mati
        if (hasTriggered) return;

        // ===============================
        // TARGET UTAMA: HELMINTH
        // ===============================
        if (collision.TryGetComponent<HelminthController>(out HelminthController helminth))
        {
            helminth.Hit();     // 🔥 HELMINTH HANCUR
            CleanupCapsule();   // target non-bakteri: capsule tetap ikut mati seperti semula
            return;
        }

        // ===============================
        // BAKTERI: SERANG CAPSULE (HP), TIDAK ONE-SHOT
        // ===============================
        if (collision.TryGetComponent<BacteriaControllerGreen>(out _) ||
            collision.TryGetComponent<BacteriaControllerRed>(out _) ||
            collision.TryGetComponent<BacteriaControllerPurple>(out _))
        {
            EngageAttacker(collision.gameObject);
            return;
        }

        // ===============================
        // MUSUH LAIN (TIDAK ADA EFEK, TETAP ONE-SHOT SEPERTI SEMULA)
        // ===============================
        if (collision.TryGetComponent<ProtozoaController>(out _) ||
            collision.TryGetComponent<MushroomController>(out _))
        {
            CleanupCapsule();
            return;
        }
    }

    private void EngageAttacker(GameObject enemy)
    {
        BacteriaMeleeAttacker attacker = enemy.GetComponent<BacteriaMeleeAttacker>();
        if (attacker == null)
            attacker = enemy.AddComponent<BacteriaMeleeAttacker>();

        attacker.Engage(health);
    }

    private void CleanupCapsule()
    {
        if (hasTriggered) return;
        hasTriggered = true;

        // 🔒 Matikan collider agar tidak trigger ulang
        if (capsuleCollider != null)
            capsuleCollider.enabled = false;

        // 🔹 Beri tahu tile bahwa capsule hilang
        if (ownerTile != null)
        {
            ownerTile.hasCapsule = false;
            ownerTile.currentCapsule = null;
        }

        // 🔥 Hancurkan capsule
        Destroy(gameObject);
    }
}
