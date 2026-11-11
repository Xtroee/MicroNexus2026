#if UNITY_EDITOR
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Builds the milk bucket ("Tarik Obat") card in the HUD of the currently open gameplay scene:
/// bottom-right corner, same navy plate + ring look as the other cards, coral ring for a "remove" action.
/// Safe to run again: an existing card is replaced.
/// </summary>
public static class MilkBucketSetup
{
    const string IconPath = "Assets/Art/milkBucket.png";
    const string CardName = "Milk Bucket Card";

    // card centre in 1920x1080 screen pixels, converted to canvas units (canvas is 800 wide, scale 2.4)
    static readonly Vector2 CardCenterUnits = new Vector2((1850f - 960f) / 2.4f, (175f - 540f) / 2.4f);
    static readonly Vector2 CardSize = new Vector2(54f, 66f);

    public static string Run()
    {
        GameManager gm = Object.FindFirstObjectByType<GameManager>(FindObjectsInactive.Include);
        if (gm == null) return "no GameManager";

        Canvas canvas = gm.sunsText != null ? gm.sunsText.canvas.rootCanvas : null;
        if (canvas == null) return "no HUD canvas";

        Sprite icon = AssetDatabase.LoadAssetAtPath<Sprite>(IconPath);
        if (icon == null) return "milk bucket sprite missing";

        Transform old = canvas.transform.Find(CardName);
        if (old != null) Object.DestroyImmediate(old.gameObject);

        // card root
        GameObject root = new GameObject(CardName, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
        Undo.RegisterCreatedObjectUndo(root, "Milk bucket card");
        root.layer = 5;
        RectTransform rt = (RectTransform)root.transform;
        rt.SetParent(canvas.transform, false);
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = CardSize;
        rt.anchoredPosition = CardCenterUnits;
        rt.SetAsLastSibling();

        Image plate = root.GetComponent<Image>();
        plate.raycastTarget = true;
        MicroNexusUIKit.MakePanel(plate, MicroNexusUIKit.Navy, MicroNexusUIKit.Coral, 8f, false);

        Button btn = root.GetComponent<Button>();
        btn.targetGraphic = plate;
        ColorBlock cb = btn.colors;
        cb.normalColor = Color.white;
        cb.highlightedColor = new Color(1.12f, 1.12f, 1.12f, 1f);
        cb.pressedColor = new Color(0.8f, 0.85f, 0.95f, 1f);
        cb.disabledColor = new Color(0.75f, 0.75f, 0.8f, 0.9f);
        btn.colors = cb;

        // icon
        RectTransform ir = MicroNexusUIKit.Child(rt, "Icon");
        ir.anchorMin = ir.anchorMax = new Vector2(0.5f, 0.5f);
        ir.sizeDelta = new Vector2(38f, 36f);
        ir.anchoredPosition = new Vector2(0f, 3f);
        Image iconImg = MicroNexusUIKit.Img(ir, icon, Color.white, 1f, false, false);
        iconImg.preserveAspect = true;

        // texts
        TextMeshProUGUI name = MicroNexusUIKit.AddLabel(rt, "TARIK OBAT", new Vector2(0f, 27f), new Vector2(52f, 8f), 4.4f, MicroNexusUIKit.TextLight);
        name.gameObject.name = "UIK Name";
        TextMeshProUGUI value = MicroNexusUIKit.AddLabel(rt, "+50%", new Vector2(0f, -25f), new Vector2(50f, 10f), 6.5f, MicroNexusUIKit.Amber);
        value.gameObject.name = "UIK Value";

        // cooldown overlay + countdown
        RectTransform orr = MicroNexusUIKit.Child(rt, "Cooldown Overlay");
        MicroNexusUIKit.Stretch(orr, 0, 0, 0, 0);
        float s = Mathf.Max(0.0001f, MicroNexusUIKit.ScaleToCanvas(orr));
        Image overlay = MicroNexusUIKit.Img(orr, MicroNexusUIKit.Plate, new Color(0f, 0f, 0.05f, 0.62f), 16f * s / 8f, true, false);
        TextMeshProUGUI cd = MicroNexusUIKit.AddLabel(orr, "0s", Vector2.zero, new Vector2(40f, 12f), 9f, Color.white);
        cd.gameObject.name = "UIK Countdown";
        orr.gameObject.SetActive(false);

        // wire the tool
        MilkBucketTool tool = root.AddComponent<MilkBucketTool>();
        SerializedObject so = new SerializedObject(tool);
        so.FindProperty("cardImage").objectReferenceValue = plate;
        so.FindProperty("iconImage").objectReferenceValue = iconImg;
        so.FindProperty("cooldownOverlay").objectReferenceValue = overlay;
        so.FindProperty("cooldownText").objectReferenceValue = cd;
        so.ApplyModifiedProperties();

        // Endless: only during the play phase (the select phase has the READY button around there)
        EndlessPhaseController phase = Object.FindFirstObjectByType<EndlessPhaseController>(FindObjectsInactive.Include);
        string where = "level HUD";
        if (phase != null)
        {
            SerializedObject ps = new SerializedObject(phase);
            SerializedProperty list = ps.FindProperty("playSlot");
            for (int i = list.arraySize - 1; i >= 0; i--)          // a replaced card leaves a null entry behind
                if (list.GetArrayElementAtIndex(i).objectReferenceValue == null)
                    list.DeleteArrayElementAtIndex(i);
            bool has = false;
            for (int i = 0; i < list.arraySize; i++)
                if (list.GetArrayElementAtIndex(i).objectReferenceValue == root) has = true;
            if (!has)
            {
                list.InsertArrayElementAtIndex(list.arraySize);
                list.GetArrayElementAtIndex(list.arraySize - 1).objectReferenceValue = root;
                ps.ApplyModifiedProperties();
            }
            root.SetActive(false);
            where = "Endless play phase slot";
        }

        EditorUtility.SetDirty(root);
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(root.scene);
        return "milk bucket card added (" + where + ") in " + root.scene.name;
    }
}
#endif
