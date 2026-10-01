using UnityEngine;

/// <summary>
/// HP tracker for a placed antibiotic unit (Capsule/BasicShooter/Soldier tier). Added to each
/// defender at Awake() by its own script - the defender still owns its death/cleanup logic
/// (tile release, collider disable, Destroy); this component only tracks HP, shows the "-N HP"
/// popup on each hit, and fires OnDeath once when HP first reaches 0.
/// </summary>
public class DefenderHealth : MonoBehaviour
{
    [SerializeField] private int maxHP = 5;

    public int CurrentHP { get; private set; }
    public bool IsDead => CurrentHP <= 0;

    public event System.Action OnDeath;

    private void Awake()
    {
        CurrentHP = maxHP;
    }

    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        CurrentHP -= amount;
        DefenderHitPopup.Show(transform.position, amount);

        if (CurrentHP <= 0)
            OnDeath?.Invoke();
    }
}
