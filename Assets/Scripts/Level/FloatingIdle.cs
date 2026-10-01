using UnityEngine;

/// <summary>
/// Gentle up/down bobbing idle animation for UI elements (e.g. Level Map nodes).
/// Adds a small sine-wave offset on top of whatever anchoredPosition the node was
/// placed at (by LevelMapController's zigzag layout) - never overwrites the base
/// position, so it's safe to just drop on the prefab with no other wiring needed.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class FloatingIdle : MonoBehaviour
{
    [Header("Bobbing")]
    [Tooltip("How far up/down the node drifts, in canvas units.")]
    [SerializeField] private float amplitude = 6f;

    [Tooltip("How fast it bobs. Roughly one full up-down cycle every (2*PI / speed) seconds.")]
    [SerializeField] private float speed = 1.2f;

    [Tooltip("Randomize each node's phase a bit so they don't all bob in perfect unison.")]
    [SerializeField] private bool randomizePhase = true;

    private RectTransform rt;
    private Vector2 basePosition;
    private float phase;

    private void Start()
    {
        rt = (RectTransform)transform;
        // Cached here (not Awake) so LevelMapController's LayoutAndInstantiate has
        // already applied the real anchoredPosition for this node by the time we read it.
        basePosition = rt.anchoredPosition;
        phase = randomizePhase ? Random.Range(0f, Mathf.PI * 2f) : 0f;
    }

    private void Update()
    {
        float yOffset = Mathf.Sin(Time.time * speed + phase) * amplitude;
        rt.anchoredPosition = basePosition + new Vector2(0f, yOffset);
    }
}
