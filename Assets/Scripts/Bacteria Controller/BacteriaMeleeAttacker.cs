using UnityEngine;
using System.Collections;
using System.Reflection;

/// <summary>
/// Added at runtime to a bacteria GameObject the moment it first touches a defender (Capsule/
/// BasicShooter/Soldier tier). Freezes the bacteria's movement via reflection on its "speed"
/// field (each BacteriaController&lt;Color&gt; is an independent class with no shared base) and
/// deals damage to the defender every attackInterval seconds until the defender dies or the
/// bacteria leaves melee range.
/// </summary>
public class BacteriaMeleeAttacker : MonoBehaviour
{
    [SerializeField] private int damagePerHit = 1;
    [SerializeField] private float attackInterval = 1.5f;

    private DefenderHealth currentTarget;
    private Coroutine attackRoutine;

    private Component speedOwner;
    private FieldInfo speedField;
    private float capturedSpeed;

    public void Engage(DefenderHealth target)
    {
        if (target == null || target.IsDead) return;
        if (currentTarget == target) return;

        if (currentTarget == null)
            FreezeMovement();

        currentTarget = target;

        if (attackRoutine == null)
            attackRoutine = StartCoroutine(AttackLoop());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DefenderHealth health = collision.GetComponent<DefenderHealth>();
        if (health != null && health == currentTarget)
            Disengage();
    }

    private void Disengage()
    {
        currentTarget = null;

        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            attackRoutine = null;
        }

        RestoreMovement();
    }

    private IEnumerator AttackLoop()
    {
        while (currentTarget != null && !currentTarget.IsDead)
        {
            currentTarget.TakeDamage(damagePerHit);
            yield return new WaitForSeconds(attackInterval);
        }

        Disengage();
    }

    private void FreezeMovement()
    {
        Component controller = FindBacteriaController();
        if (controller == null) return;

        FieldInfo field = controller.GetType().GetField("speed", BindingFlags.Public | BindingFlags.Instance);
        if (field == null) return;

        speedOwner = controller;
        speedField = field;
        capturedSpeed = (float)field.GetValue(controller);
        field.SetValue(controller, 0f);
    }

    private void RestoreMovement()
    {
        if (speedField != null && speedOwner != null)
            speedField.SetValue(speedOwner, capturedSpeed);

        speedOwner = null;
        speedField = null;
    }

    private Component FindBacteriaController()
    {
        foreach (Component c in GetComponents<Component>())
        {
            if (c != null && c.GetType().Name.StartsWith("BacteriaController"))
                return c;
        }
        return null;
    }
}
