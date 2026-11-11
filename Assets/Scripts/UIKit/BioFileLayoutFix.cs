#if UNITY_EDITOR
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// One-off layout/navigation clean-up for the Bio-File (Halaman Belajar) prefab:
/// - every Back button sits at the same place,
/// - detail pages have ONE back button (top-left) that returns to the list; the little round arrow
///   inside the detail panel is hidden,
/// - the "next" arrow on every list page sits at the same place and carries a "DETAIL" caption,
/// - the Bacteria / Other choice is ordered main-first.
/// Positions are given in 1920x1080 screen pixels. The canvases are stored with scale 0 inside the
/// prefab, so they are given their runtime scale while measuring and restored afterwards.
/// </summary>
public static class BioFileLayoutFix
{
    const string PrefabPath = "Assets/Prefab/RangerBio/RangerGroup.prefab";
    const float K = 2.4f;   // 1920 / 800 reference width

    static readonly Vector2 BackCenter = new Vector2(186.5f, 959.5f);
    static readonly Vector2 ArrowCenter = new Vector2(1760f, 590f);
    const float ArrowSize = 88f;

    struct Saved { public RectTransform rt; public Vector3 pos; public Vector3 scale; public Vector2 size; public Vector2 aMin, aMax, pivot, aPos; }

    static readonly HashSet<string> ListPages = new HashSet<string>
    { "Bio Ranger Canvas", "Bio Enemy Bacteria Panel", "Bio Enemy Other Panel", "Bio Enemy Default" };

    public static string Run()
    {
        StringBuilder log = new StringBuilder();
        GameObject root = PrefabUtility.LoadPrefabContents(PrefabPath);
        List<Saved> saved = new List<Saved>();
        try
        {
            foreach (Canvas c in root.GetComponentsInChildren<Canvas>(true))
            {
                RectTransform rt = (RectTransform)c.transform;
                if (rt.parent != root.transform) continue;
                saved.Add(new Saved { rt = rt, pos = rt.localPosition, scale = rt.localScale, size = rt.sizeDelta, aMin = rt.anchorMin, aMax = rt.anchorMax, pivot = rt.pivot, aPos = rt.anchoredPosition });
                rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
                rt.pivot = new Vector2(0.5f, 0.5f);
                rt.sizeDelta = new Vector2(800f, 450f);
                rt.localScale = Vector3.one * K;
                rt.localPosition = new Vector3(960f, 540f, 0f);
            }
            Vector3 rootPos = root.transform.localPosition;
            root.transform.localPosition = Vector3.zero;

            int backs = 0, merged = 0, arrows = 0, labels = 0;
            foreach (Button b in root.GetComponentsInChildren<Button>(true))
            {
                string n = b.name;
                Transform parent = b.transform.parent;
                if (n == "Back" || n == "Back Button")
                {
                    // detail page: copy the little arrow's "go to list" call onto the big back, hide the arrow
                    Button side = null;
                    if (parent != null && !ListPages.Contains(parent.name) && parent.name != "Bio Enemy Choose Panel" && parent.name != "General Canvas")
                        foreach (Button o in parent.GetComponentsInChildren<Button>(true))
                            if (o != b && o.name.EndsWith("SideButton") && o.transform.parent == parent) { side = o; break; }
                    if (side != null && CopyFirstCall(side, b))
                    {
                        side.gameObject.SetActive(false);
                        merged++;
                    }
                    PlaceCenter((RectTransform)b.transform, BackCenter, null);
                    backs++;
                }
                else if (n.EndsWith("SideButton") && parent != null && ListPages.Contains(parent.name))
                {
                    PlaceCenter((RectTransform)b.transform, ArrowCenter, new Vector2(ArrowSize, ArrowSize));
                    arrows++;
                    if (b.transform.Find("MNX Detail Label") == null && b.GetComponent<Image>() != null && b.GetComponent<Image>().enabled)
                    {
                        float s = Mathf.Max(0.0001f, MicroNexusUIKit.ScaleToCanvas((RectTransform)b.transform));
                        float h = (ArrowSize / K) / s;   // arrow height in the arrow's own units
                        TextMeshProUGUI t = MicroNexusUIKit.AddLabel(b.transform, "DETAIL", new Vector2(0f, -(ArrowSize / K) * 0.5f - 7f), new Vector2(70f, 10f), 6.5f, MicroNexusUIKit.TextLight);
                        t.gameObject.name = "MNX Detail Label";
                        labels++;
                    }
                }
            }

            // Bacteria first, Other second
            Transform choose = root.transform.Find("General Canvas/Bio Enemy Choose Panel");
            if (choose != null)
            {
                RectTransform bac = choose.Find("Bacteria button") as RectTransform;
                RectTransform oth = choose.Find("Other button") as RectTransform;
                if (bac != null && oth != null)
                {
                    Vector2 a = bac.anchoredPosition; bac.anchoredPosition = oth.anchoredPosition; oth.anchoredPosition = a;
                    if (bac.anchoredPosition.x > oth.anchoredPosition.x) { a = bac.anchoredPosition; bac.anchoredPosition = oth.anchoredPosition; oth.anchoredPosition = a; }
                    log.AppendLine("choose panel ordered: Bacteria x=" + bac.anchoredPosition.x + " Other x=" + oth.anchoredPosition.x);
                }
            }
            log.AppendLine("backs=" + backs + " mergedDetailBack=" + merged + " arrows=" + arrows + " labels=" + labels);

            root.transform.localPosition = rootPos;
            foreach (Saved s in saved)
            {
                s.rt.anchorMin = s.aMin; s.rt.anchorMax = s.aMax; s.rt.pivot = s.pivot;
                s.rt.sizeDelta = s.size; s.rt.anchoredPosition = s.aPos;
                s.rt.localScale = s.scale; s.rt.localPosition = s.pos;
            }
            PrefabUtility.SaveAsPrefabAsset(root, PrefabPath);
        }
        finally
        {
            PrefabUtility.UnloadPrefabContents(root);
        }
        return log.ToString();
    }

    /// <summary>Moves a rect so that its visual centre lands on the given screen pixel; optionally resizes (square size in pixels).</summary>
    static void PlaceCenter(RectTransform rt, Vector2 centerPx, Vector2? sizePx)
    {
        if (sizePx.HasValue && rt.anchorMin == rt.anchorMax)
        {
            float ls = Mathf.Max(0.0001f, rt.lossyScale.x);
            rt.sizeDelta = new Vector2(sizePx.Value.x / ls, sizePx.Value.y / ls);
        }
        Vector3[] c = new Vector3[4];
        rt.GetWorldCorners(c);
        Vector3 cur = (c[0] + c[2]) * 0.5f;
        Vector3 delta = new Vector3(centerPx.x, centerPx.y, cur.z) - cur;
        rt.position += delta;
    }

    static bool CopyFirstCall(Button from, Button to)
    {
        SerializedObject sf = new SerializedObject(from);
        SerializedObject st = new SerializedObject(to);
        SerializedProperty cf = sf.FindProperty("m_OnClick.m_PersistentCalls.m_Calls");
        SerializedProperty ct = st.FindProperty("m_OnClick.m_PersistentCalls.m_Calls");
        if (cf == null || ct == null || cf.arraySize == 0) return false;
        SerializedProperty src = cf.GetArrayElementAtIndex(0);
        if (src.FindPropertyRelative("m_Target").objectReferenceValue == null) return false;
        ct.ClearArray();
        ct.InsertArrayElementAtIndex(0);
        SerializedProperty dst = ct.GetArrayElementAtIndex(0);
        dst.FindPropertyRelative("m_Target").objectReferenceValue = src.FindPropertyRelative("m_Target").objectReferenceValue;
        dst.FindPropertyRelative("m_TargetAssemblyTypeName").stringValue = src.FindPropertyRelative("m_TargetAssemblyTypeName").stringValue;
        dst.FindPropertyRelative("m_MethodName").stringValue = src.FindPropertyRelative("m_MethodName").stringValue;
        dst.FindPropertyRelative("m_Mode").enumValueIndex = src.FindPropertyRelative("m_Mode").enumValueIndex;
        dst.FindPropertyRelative("m_CallState").enumValueIndex = src.FindPropertyRelative("m_CallState").enumValueIndex;
        dst.FindPropertyRelative("m_Arguments.m_StringArgument").stringValue = "";
        st.ApplyModifiedProperties();
        return true;
    }
}
#endif
