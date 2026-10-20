#if UNITY_EDITOR
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Editor-only helpers used to re-skin the project's UI to the MicroNexus "biotech" look
/// (navy plates, accent frames, Retro Gaming font) set by the Mode Selection screen.
/// Every builder is idempotent: it clears what it created on a previous run (children
/// named with the "UIK " prefix) before building again.
/// </summary>
public static class MicroNexusUIKit
{
    public const string Prefix = "UIK ";

    public static readonly Color Navy = new Color(0.07f, 0.09f, 0.21f, 0.95f);
    public static readonly Color Teal = new Color(0.35f, 0.88f, 0.97f, 0.95f);
    public static readonly Color Coral = new Color(1f, 0.5f, 0.62f, 0.95f);
    public static readonly Color Violet = new Color(0.72f, 0.6f, 1f, 0.95f);
    public static readonly Color Amber = new Color(1f, 0.78f, 0.35f, 0.95f);
    public static readonly Color Mint = new Color(0.5f, 0.95f, 0.7f, 0.95f);
    public static readonly Color TextLight = new Color(0.85f, 0.94f, 1f, 1f);

    private static Sprite Load(string path) { return AssetDatabase.LoadAssetAtPath<Sprite>(path); }
    public static Sprite Plate { get { return Load("Assets/Art/Mode Selection/ModeCardPlate.png"); } }
    public static Sprite Ring { get { return Load("Assets/Art/Mode Selection/ModeCardFrame.png"); } }
    public static Sprite Shadow { get { return Load("Assets/Art/Mode Selection/ModeCardShadow.png"); } }
    public static Sprite BackSprite { get { return Load("Assets/Art/UI Kit/UIBackButton.png"); } }
    public static Sprite RoundArrow { get { return Load("Assets/Art/UI Kit/UIRoundArrow.png"); } }
    public static Sprite Vignette { get { return Load("Assets/Art/UI Kit/UIVignette.png"); } }
    public static Sprite Background { get { return Load("Assets/Art/Background/levelMap.png"); } }

    public static TMP_FontAsset Retro { get { return AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Font/Asset/Retro Gaming SDF.asset"); } }

    public static TMP_FontAsset BodyFont
    {
        get
        {
            foreach (string g in AssetDatabase.FindAssets("MYRIADPRO-BOLD SDF t:TMP_FontAsset"))
                return AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(AssetDatabase.GUIDToAssetPath(g));
            return null;
        }
    }

    // ---------------------------------------------------------------- scale helper
    /// <summary>Local-units-per-canvas-unit for a RectTransform (1 when it is not scaled relative to its canvas).</summary>
    public static float ScaleToCanvas(RectTransform rt)
    {
        // Product of local scales from the element up to (but excluding) its root canvas. This keeps
        // working inside prefab assets, where the canvas itself is stored with scale 0.
        // includeInactive: panels that are switched off must still resolve their canvas. Walk to the
        // topmost Canvas by hand, rootCanvas is unreliable on inactive hierarchies.
        Transform stop = null;
        for (Transform p = rt; p != null; p = p.parent)
            if (p.GetComponent<Canvas>() != null) stop = p;
        float s = 1f;
        Transform t = rt;
        while (t != null && t != stop)
        {
            s *= t.localScale.x;
            t = t.parent;
        }
        return s;
    }

    // ---------------------------------------------------------------- builders
    public static void ClearKit(Transform parent)
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
            if (parent.GetChild(i).name.StartsWith(Prefix))
                Object.DestroyImmediate(parent.GetChild(i).gameObject);
    }

    public static RectTransform Child(Transform parent, string name)
    {
        GameObject go = new GameObject(Prefix + name, typeof(RectTransform));
        Undo.RegisterCreatedObjectUndo(go, "UI kit");
        go.layer = 5;
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.SetParent(parent, false);
        return rt;
    }

    public static void Stretch(RectTransform r, float l, float b, float rr, float t)
    {
        r.anchorMin = Vector2.zero;
        r.anchorMax = Vector2.one;
        r.offsetMin = new Vector2(l, b);
        r.offsetMax = new Vector2(-rr, -t);
    }

    public static Image Img(RectTransform r, Sprite sp, Color col, float ppum, bool sliced = true, bool raycast = false)
    {
        Image im = r.gameObject.GetComponent<Image>();
        if (im == null) im = r.gameObject.AddComponent<Image>();
        im.sprite = sp;
        im.type = sliced ? Image.Type.Sliced : Image.Type.Simple;
        im.color = col;
        im.pixelsPerUnitMultiplier = ppum;
        im.raycastTarget = raycast;
        im.preserveAspect = false;
        return im;
    }

    public static TextMeshProUGUI Txt(RectTransform r, string s, float size, Color col, TextAlignmentOptions al, TMP_FontAsset font = null)
    {
        TextMeshProUGUI t = r.gameObject.GetComponent<TextMeshProUGUI>();
        if (t == null) t = r.gameObject.AddComponent<TextMeshProUGUI>();
        t.font = font != null ? font : Retro;
        t.text = s;
        t.fontSize = size;
        t.color = col;
        t.alignment = al;
        t.raycastTarget = false;
        t.overflowMode = TextOverflowModes.Ellipsis;
        t.textWrappingMode = TextWrappingModes.NoWrap;
        return t;
    }

    /// <summary>Restyle an existing Image as a rounded navy plate with an accent ring child.</summary>
    public static void MakePanel(Image target, Color fill, Color accent, float cornerCanvasUnits = 14f, bool withShadow = false)
    {
        RectTransform rt = (RectTransform)target.transform;
        float s = Mathf.Max(0.0001f, ScaleToCanvas(rt));
        float ppum = 16f * s / cornerCanvasUnits;   // sprite border 20px ~ 16px radius -> cornerCanvasUnits on screen
        target.sprite = Plate;
        target.type = Image.Type.Sliced;
        target.pixelsPerUnitMultiplier = ppum;
        target.color = fill;
        ClearKit(rt);
        if (withShadow)
        {
            RectTransform sh = Child(rt, "Shadow");
            sh.SetAsFirstSibling();
            float pad = 22f / s;
            Stretch(sh, -pad, -pad * 1.3f, -pad, pad * 0.6f);
            Img(sh, Shadow, new Color(0f, 0f, 0f, 0.45f), 16f * s / cornerCanvasUnits * 2f);
        }
        RectTransform ring = Child(rt, "Frame");
        Stretch(ring, 0, 0, 0, 0);
        Img(ring, Ring, accent, ppum);
    }

    public static void StyleBack(Button btn)
    {
        Image im = btn.targetGraphic as Image;
        if (im == null) im = btn.GetComponent<Image>();
        if (im == null) return;
        im.sprite = BackSprite;
        im.type = Image.Type.Simple;
        im.preserveAspect = false;
        im.color = Color.white;
        ColorBlock cb = btn.colors;
        cb.normalColor = Color.white;
        cb.highlightedColor = new Color(0.85f, 1f, 1f, 1f);
        cb.pressedColor = new Color(0.7f, 0.82f, 0.92f, 1f);
        cb.selectedColor = Color.white;
        btn.colors = cb;
        // the old orange sprite had a baked "Text" label on some buttons - hide leftovers
        foreach (TextMeshProUGUI t in btn.GetComponentsInChildren<TextMeshProUGUI>(true))
            if (t.text == "Button") t.gameObject.SetActive(false);
        EditorUtility.SetDirty(btn.gameObject);
    }

    public static void StyleRoundArrow(Button btn)
    {
        Image im = btn.targetGraphic as Image;
        if (im == null) im = btn.GetComponent<Image>();
        if (im == null) return;
        im.sprite = RoundArrow;
        im.type = Image.Type.Simple;
        im.preserveAspect = true;
        im.color = Color.white;
        ColorBlock cb = btn.colors;
        cb.highlightedColor = new Color(0.85f, 1f, 1f, 1f);
        cb.pressedColor = new Color(0.7f, 0.82f, 0.92f, 1f);
        btn.colors = cb;
        EditorUtility.SetDirty(btn.gameObject);
    }

    /// <summary>
    /// Adds an accent ring (and optionally a navy plate behind) over/under an existing element by
    /// cloning its RectTransform into the same parent. Used where the element itself must keep its
    /// own graphic (e.g. a masked scroll area or a screenshot image).
    /// </summary>
    public static void RingAround(RectTransform source, Color accent, float pad = 0f, bool plateBehind = false, float corner = 12f)
    {
        Transform parent = source.parent;
        // drop earlier runs for this source
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            string n = parent.GetChild(i).name;
            if (n == Prefix + "Ring " + source.name || n == Prefix + "Mat " + source.name)
                Object.DestroyImmediate(parent.GetChild(i).gameObject);
        }
        float s = Mathf.Max(0.0001f, ScaleToCanvas(source));
        float ppum = 16f * s / corner;
        float padLocal = pad / s;

        RectTransform ring = Child(parent, "Ring " + source.name);
        CopyRect(source, ring, padLocal);
        Img(ring, Ring, accent, ppum);
        ring.SetSiblingIndex(source.GetSiblingIndex() + 1);

        if (plateBehind)
        {
            RectTransform mat = Child(parent, "Mat " + source.name);
            CopyRect(source, mat, padLocal);
            Img(mat, Plate, Navy, ppum);
            mat.SetSiblingIndex(source.GetSiblingIndex());
        }
    }

    /// <summary>
    /// Accent ring as a CHILD of the element (stretched over it), so it follows the element's
    /// active state. Not for masked elements - a ring child would be clipped by the mask.
    /// </summary>
    public static void RingChild(RectTransform source, Color accent, float pad = 0f, float corner = 8f)
    {
        ClearKit(source);
        float s = Mathf.Max(0.0001f, ScaleToCanvas(source));
        float padLocal = pad / s;
        RectTransform ring = Child(source, "Ring");
        Stretch(ring, -padLocal, -padLocal, -padLocal, -padLocal);
        Img(ring, Ring, accent, 16f * s / corner);
    }

    private static void CopyRect(RectTransform src, RectTransform dst, float padLocal)
    {
        dst.anchorMin = src.anchorMin;
        dst.anchorMax = src.anchorMax;
        dst.pivot = src.pivot;
        dst.localScale = src.localScale;
        dst.localRotation = src.localRotation;
        dst.anchoredPosition = src.anchoredPosition;
        dst.sizeDelta = src.sizeDelta + new Vector2(padLocal * 2f, padLocal * 2f);
        if (src.anchorMin != src.anchorMax)
        {
            dst.offsetMin = src.offsetMin - new Vector2(padLocal, padLocal);
            dst.offsetMax = src.offsetMax + new Vector2(padLocal, padLocal);
        }
    }

    /// <summary>Banner (title bar) = rounded navy plate with accent ring, Retro title that shrinks to fit.</summary>
    public static void StyleBanner(Image banner, TextMeshProUGUI title, Color accent, float min = 16f, float max = 48f)
    {
        MakePanel(banner, Navy, accent, 12f, true);
        if (title == null) return;
        title.font = Retro;
        title.color = Color.white;
        title.enableAutoSizing = true;
        title.fontSizeMin = min;
        title.fontSizeMax = max;
        title.fontSize = max;
        title.textWrappingMode = TextWrappingModes.NoWrap;
        title.overflowMode = TextOverflowModes.Ellipsis;
        title.alignment = TextAlignmentOptions.Midline;
        title.transform.SetAsLastSibling();
    }

    public static void StyleScrollbar(Scrollbar bar, Color accent)
    {
        if (bar == null) return;
        Image bg = bar.GetComponent<Image>();
        if (bg != null)
        {
            float s = Mathf.Max(0.0001f, ScaleToCanvas((RectTransform)bar.transform));
            bg.sprite = Plate; bg.type = Image.Type.Sliced; bg.pixelsPerUnitMultiplier = 16f * s / 4f; bg.color = new Color(0.1f, 0.14f, 0.3f, 0.9f);
        }
        Image h = bar.handleRect != null ? bar.handleRect.GetComponent<Image>() : null;
        if (h != null)
        {
            float s = Mathf.Max(0.0001f, ScaleToCanvas(bar.handleRect));
            h.sprite = Plate; h.type = Image.Type.Sliced; h.pixelsPerUnitMultiplier = 16f * s / 4f; h.color = new Color(accent.r, accent.g, accent.b, 0.9f);
        }
        EditorUtility.SetDirty(bar.gameObject);
    }

    // ================================================================ composite helpers

    /// <summary>
    /// A big rounded content panel (navy plate + accent ring) inserted as the FIRST child of a
    /// canvas/panel, replacing the frame art that used to be baked into backgrounds.
    /// insets = (left, bottom, right, top) in the parent's local units.
    /// </summary>
    public static RectTransform AddContentFrame(Transform parent, Color accent, Vector4 insets, string name = "Content Frame")
    {
        Transform old = parent.Find(Prefix + name);
        if (old != null) Object.DestroyImmediate(old.gameObject);
        RectTransform rt = Child(parent, name);
        rt.SetSiblingIndex(0);
        Stretch(rt, insets.x, insets.y, insets.z, insets.w);
        float s = Mathf.Max(0.0001f, ScaleToCanvas(rt));
        Img(rt, Plate, new Color(0.05f, 0.07f, 0.18f, 0.72f), 16f * s / 16f);
        RectTransform ring = Child(rt, "Ring");
        Stretch(ring, 0, 0, 0, 0);
        Img(ring, Ring, accent, 16f * s / 16f);
        return rt;
    }

    /// <summary>
    /// A navy plate with accent ring behind a group of elements, placed by centre/size in the parent's
    /// local units (parent must be an unscaled panel). Sibling index 1, i.e. just above the background.
    /// </summary>
    public static RectTransform AddPlate(Transform parent, Vector2 center, Vector2 size, Color accent, string name)
    {
        Transform old = parent.Find(Prefix + name);
        if (old != null) Object.DestroyImmediate(old.gameObject);
        RectTransform rt = Child(parent, name);
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = size;
        rt.anchoredPosition = center;
        // canvases keep a separate full-screen background child: sit just above it, not behind it
        Transform bgChild = parent.Find("BioRangerBG");
        rt.SetSiblingIndex(bgChild != null ? bgChild.GetSiblingIndex() + 1 : 0);
        float s = Mathf.Max(0.0001f, ScaleToCanvas(rt));
        float ppum = 16f * s / 14f;
        Img(rt, Plate, new Color(0.05f, 0.07f, 0.18f, 0.78f), ppum);
        RectTransform ring = Child(rt, "Ring");
        Stretch(ring, 0, 0, 0, 0);
        Img(ring, Ring, accent, ppum);
        return rt;
    }

    /// <summary>Title banner whose text used to be baked into the sprite: plate + ring + a real TMP label.</summary>
    public static void StyleTitleBanner(Transform title, string text, Color accent)
    {
        Image im = title.GetComponent<Image>();
        if (im == null) return;
        RectTransform rt = (RectTransform)title;
        // normalise to the same banner everywhere: 250x62 canvas units, centred 40 units below the top edge
        float scl = Mathf.Max(0.0001f, ScaleToCanvas(rt));
        float parentScl = Mathf.Max(0.0001f, ScaleToCanvas((RectTransform)rt.parent));
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 1f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = new Vector2(250f / scl, 62f / scl);
        rt.anchoredPosition = new Vector2(0f, -40f / parentScl);
        MakePanel(im, Navy, accent, 12f, true);
        Transform old = rt.Find(Prefix + "Title Text");
        if (old != null) Object.DestroyImmediate(old.gameObject);
        RectTransform tx = Child(rt, "Title Text");
        Stretch(tx, 18f, 8f, 18f, 8f);
        TextMeshProUGUI t = Txt(tx, text, 14f, Color.white, TextAlignmentOptions.Midline);
        t.enableAutoSizing = true;
        float sc = Mathf.Max(0.0001f, ScaleToCanvas(rt));
        t.fontSizeMin = 7f / sc;      // never smaller than ~7 canvas units on screen
        t.fontSizeMax = 15f / sc;     // ~15 canvas units
        t.fontSize = t.fontSizeMax;
        // any real label that was already a child stays (the old Text (TMP) of some banners) - hide it
        foreach (TextMeshProUGUI other in rt.GetComponentsInChildren<TextMeshProUGUI>(true))
            if (other != t && !other.name.StartsWith(Prefix)) other.gameObject.SetActive(false);
    }

    /// <summary>The "Scroll Text" block used for long descriptions: navy plate, accent ring, stable viewport, padded text.</summary>
    public static void StyleScrollText(Transform scrollText, Color accent)
    {
        if (scrollText == null) return;
        Mask mask = scrollText.GetComponentInChildren<Mask>(true);
        if (mask == null) return;
        RectTransform area = (RectTransform)mask.transform;
        Image ai = mask.GetComponent<Image>();
        float s = Mathf.Max(0.0001f, ScaleToCanvas(area));
        ai.sprite = Plate;
        ai.type = Image.Type.Sliced;
        ai.pixelsPerUnitMultiplier = 16f * s / 12f;
        ai.color = new Color(0.06f, 0.08f, 0.2f, 0.9f);
        mask.showMaskGraphic = true;
        RingAround(area, accent, 0f, false, 12f);
        ScrollRect sr = area.GetComponentInChildren<ScrollRect>(true);
        if (sr != null)
        {
            SerializedObject so = new SerializedObject(sr);
            so.FindProperty("m_Viewport").objectReferenceValue = area;
            so.ApplyModifiedPropertiesWithoutUndo();
        }
        foreach (TextMeshProUGUI t in scrollText.GetComponentsInChildren<TextMeshProUGUI>(true))
        {
            if (t.name != "Text (TMP)") continue;
            t.font = BodyFont;
            t.color = TextLight;
            t.margin = new Vector4(48f, 44f, 70f, 44f);
            EditorUtility.SetDirty(t);
        }
        foreach (Scrollbar bar in scrollText.GetComponentsInChildren<Scrollbar>(true)) StyleScrollbar(bar, accent);
    }

    /// <summary>
    /// Makes the masked scroll area taller (amounts in canvas units) and keeps its text glued to the top
    /// and its scrollbar the same relative length. Call BEFORE StyleScrollText so the ring is rebuilt.
    /// </summary>
    public static void GrowScrollArea(Transform scrollText, float topUnits, float bottomUnits)
    {
        Mask mask = scrollText.GetComponentInChildren<Mask>(true);
        if (mask == null) return;
        if (scrollText.Find(Prefix + "Grown") != null) return;   // already grown on an earlier run
        Child(scrollText, "Grown");
        RectTransform st = (RectTransform)scrollText;
        RectTransform area = (RectTransform)mask.transform;
        float s = ScaleToCanvas(st);
        float k = area.localScale.y;
        float f = Mathf.Max(0.0001f, s * (1f + k) * 0.5f);   // canvas units per local offset unit (pivot 0.5)
        float dTop = topUnits / f, dBot = bottomUnits / f;
        float oldH = st.rect.height + (area.offsetMax.y - area.offsetMin.y);
        area.offsetMax += new Vector2(0f, dTop);
        area.offsetMin -= new Vector2(0f, dBot);
        float newH = st.rect.height + (area.offsetMax.y - area.offsetMin.y);
        float shift = (dTop + dBot) * 0.5f;
        foreach (TextMeshProUGUI t in area.GetComponentsInChildren<TextMeshProUGUI>(true))
        {
            if (t.name != "Text (TMP)") continue;
            RectTransform tr = (RectTransform)t.transform;
            tr.offsetMin += new Vector2(0f, shift);
            tr.offsetMax += new Vector2(0f, shift);
        }
        foreach (Scrollbar bar in area.GetComponentsInChildren<Scrollbar>(true))
        {
            RectTransform br = (RectTransform)bar.transform;
            float r = newH / Mathf.Max(1f, oldH);
            br.offsetMin = new Vector2(br.offsetMin.x, br.offsetMin.y * r);
            br.offsetMax = new Vector2(br.offsetMax.x, br.offsetMax.y * r);
        }
    }

    /// <summary>Every Button under root whose name ends with "SideButton" (the little next/previous arrows).</summary>
    public static int StyleSideButtons(Transform root)
    {
        int n = 0;
        foreach (Button b in root.GetComponentsInChildren<Button>(true))
            if (b.name.EndsWith("SideButton")) { StyleRoundArrow(b); n++; }
        return n;
    }

    public static int StyleBackButtons(Transform root)
    {
        int n = 0;
        foreach (Button b in root.GetComponentsInChildren<Button>(true))
            if (b.name == "Back" || b.name == "Back Button") { StyleBack(b); n++; }
        return n;
    }

    /// <summary>Generic text button: navy plate + accent ring, Retro label that shrinks to fit.</summary>
    public static void StyleButton(Button b, Color accent, float maxSize = 9f)
    {
        Image im = b.targetGraphic as Image;
        if (im == null) im = b.GetComponent<Image>();
        if (im == null) return;
        MakePanel(im, Navy, accent, 10f, false);
        ColorBlock cb = b.colors;
        cb.normalColor = Color.white;
        cb.highlightedColor = new Color(1.15f, 1.15f, 1.15f, 1f);
        cb.pressedColor = new Color(0.8f, 0.85f, 0.95f, 1f);
        cb.selectedColor = Color.white;
        b.colors = cb;
        foreach (TextMeshProUGUI t in b.GetComponentsInChildren<TextMeshProUGUI>(true))
        {
            RectTransform tr = (RectTransform)t.transform;
            if (tr.parent == b.transform)
            {
                // old labels were scaled/offset to sit on baked art: fill the plate instead
                tr.localScale = Vector3.one;
                tr.anchorMin = Vector2.zero; tr.anchorMax = Vector2.one;
                tr.offsetMin = new Vector2(10f, 4f); tr.offsetMax = new Vector2(-10f, -4f);
            }
            float sc = Mathf.Max(0.0001f, ScaleToCanvas(tr));
            t.margin = Vector4.zero;   // old labels carried big margins to centre on baked art
            t.font = Retro;
            t.enableAutoSizing = true;
            t.fontSizeMin = 4f / sc;
            t.fontSizeMax = maxSize / sc;
            t.fontSize = t.fontSizeMax;
            t.alignment = TextAlignmentOptions.Midline;
            t.textWrappingMode = TextWrappingModes.NoWrap;
            t.overflowMode = TextOverflowModes.Ellipsis;
            EditorUtility.SetDirty(t);
        }
        EditorUtility.SetDirty(b.gameObject);
    }

    private static Color RangerTone(string iconName)
    {
        if (iconName.StartsWith("Amoxicillin")) return new Color(0.88f, 0.95f, 1f, 0.95f);
        if (iconName.StartsWith("Vancomisin")) return new Color(0.4f, 0.7f, 1f, 0.95f);
        if (iconName.StartsWith("Cefixime")) return new Color(1f, 0.62f, 0.82f, 0.95f);
        if (iconName.StartsWith("Azitromisin")) return new Color(1f, 0.45f, 0.5f, 0.95f);
        if (iconName.StartsWith("Ciprofloxacin")) return Mint;
        if (iconName.StartsWith("Levofloksasin")) return Amber;
        if (iconName.StartsWith("Cotrimoxazole")) return Violet;
        // iterasi 2 characters (same colours as their projectile capsules)
        string low = iconName.ToLower();
        if (low.StartsWith("ceftria")) return new Color(0.62f, 0.45f, 0.9f, 0.95f);
        if (low.StartsWith("clarith")) return new Color(1f, 0.55f, 0.4f, 0.95f);
        if (low.StartsWith("colistin")) return new Color(0.58f, 0.72f, 0.3f, 0.95f);
        if (low.StartsWith("doxy")) return new Color(0.85f, 0.32f, 0.26f, 0.95f);
        if (low.StartsWith("linezo")) return new Color(0.4f, 0.75f, 0.98f, 0.95f);
        if (low.StartsWith("merope")) return new Color(1f, 0.7f, 0.2f, 0.95f);
        if (low.StartsWith("penicilin")) return new Color(0.7f, 0.48f, 0.3f, 0.95f);
        return Teal;
    }

    /// <summary>
    /// Re-points one existing level card to another character: prefab, icon, label, name.
    /// The card keeps its slot position and price. Returns false when no card with that label exists.
    /// </summary>
    public static bool RetargetCard(Transform canvas, string oldLabel, string newCardName, GameObject prefab, Sprite icon, string newLabel)
    {
        foreach (Transform c in canvas)
        {
            if (!c.name.StartsWith("card ") || !c.gameObject.activeSelf) continue;
            Transform nm = c.Find("Nama");
            if (nm == null || nm.GetComponent<TextMeshProUGUI>().text != oldLabel) continue;
            CapsuleSlot slot = c.GetComponent<CapsuleSlot>();
            if (slot == null) continue;
            SerializedObject so = new SerializedObject(slot);
            so.FindProperty("capsuleObject").objectReferenceValue = prefab;
            so.FindProperty("capsuleSprite").objectReferenceValue = icon;
            so.ApplyModifiedPropertiesWithoutUndo();
            Image ic = c.Find("Capsule").GetComponent<Image>();
            ic.sprite = icon;
            ic.preserveAspect = true;
            // same icon height as the other cards (474 card-units), width follows the art
            float aspect = icon.rect.width / icon.rect.height;
            ic.rectTransform.sizeDelta = new Vector2(474f * aspect, 474f);
            ic.rectTransform.anchoredPosition = new Vector2(0f, 66f);
            nm.GetComponent<TextMeshProUGUI>().text = newLabel;
            c.name = newCardName;
            EditorUtility.SetDirty(c.gameObject);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Minimal character cards of a level HUD: dark plate with a thin ring in the character's colour,
    /// Retro name, amber price. Functional parts (CapsuleSlot, cooldown masks) are left alone.
    /// </summary>
    public static int StyleLevelCards(Transform canvas)
    {
        int n = 0;
        foreach (Transform c in canvas.GetComponentsInChildren<Transform>(true))
        {
            // live cards ("card ...") and Endless' empty card backs ("... Card Back")
            if (c == null || c.name.StartsWith(Prefix)) continue;   // rings of an earlier run are destroyed while we iterate
            if (!(c.name.StartsWith("card ") || c.name.EndsWith("Card Back")) || c.Find("Nama") == null) continue;
            if (c.GetComponent<Image>() == null) continue;
            Image im = c.GetComponent<Image>();
            Image icon = null;
            foreach (Transform ch in c)
                if (ch.name == "Capsule" || ch.name.EndsWith(" Character")) { icon = ch.GetComponent<Image>(); break; }
            Color tone = RangerTone(icon != null && icon.sprite != null ? icon.sprite.name : "");
            MakePanel(im, new Color(0.06f, 0.08f, 0.2f, 0.95f), tone, 8f, false);
            // keep the ring below the overlays/mask children: it was appended last, move it right after the plate
            Transform ring = c.Find(Prefix + "Frame");
            if (ring != null) ring.SetSiblingIndex(0);

            RectTransform nm = (RectTransform)c.Find("Nama");
            TextMeshProUGUI nt = nm.GetComponent<TextMeshProUGUI>();
            float sc = Mathf.Max(0.0001f, ScaleToCanvas(nm));
            nt.font = Retro; nt.color = Color.white; nt.enableAutoSizing = true;
            nt.fontSizeMin = 3f / sc; nt.fontSizeMax = 5.5f / sc; nt.fontSize = nt.fontSizeMax;
            nt.alignment = TextAlignmentOptions.Midline;
            nt.textWrappingMode = TextWrappingModes.NoWrap; nt.overflowMode = TextOverflowModes.Overflow;
            EditorUtility.SetDirty(nt);

            foreach (TextMeshProUGUI t in c.GetComponentsInChildren<TextMeshProUGUI>(true))
            {
                if (!t.name.StartsWith("Text ")) continue;   // the price
                t.gameObject.SetActive(true);   // the number used to be baked into the old card sprite; CapsuleSlot fills the text
                RectTransform pr = (RectTransform)t.transform;
                float psc = Mathf.Max(0.0001f, ScaleToCanvas(pr));
                pr.anchorMin = pr.anchorMax = new Vector2(0.5f, 0.5f);
                pr.pivot = new Vector2(0.5f, 0.5f);
                RectTransform cr = (RectTransform)c;
                float cardW = cr.sizeDelta.x, cardH = cr.sizeDelta.y;
                pr.localScale = Vector3.one;
                pr.sizeDelta = new Vector2(cardW * 0.8f, cardH * 0.2f);
                pr.anchoredPosition = new Vector2(0f, -cardH * 0.39f);
                t.font = Retro; t.color = Amber; t.enableAutoSizing = true;
                float cs = Mathf.Max(0.0001f, ScaleToCanvas(cr));
                t.fontSizeMin = 4f / cs; t.fontSizeMax = 7.5f / cs; t.fontSize = t.fontSizeMax;
                t.alignment = TextAlignmentOptions.Midline;
                t.textWrappingMode = TextWrappingModes.NoWrap; t.overflowMode = TextOverflowModes.Overflow;
                EditorUtility.SetDirty(t);
            }
            CapsuleSlot slot = c.GetComponent<CapsuleSlot>();
            if (slot != null)
            {
                SerializedObject so = new SerializedObject(slot);
                SerializedProperty sel = so.FindProperty("selectedColor");
                if (sel != null) { sel.colorValue = new Color(0.6f, 1f, 0.85f, 1f); so.ApplyModifiedPropertiesWithoutUndo(); }
            }
            n++;
        }
        Transform hud = canvas.Find("HUD Background Fixed");
        if (hud != null)
            MakePanel(hud.GetComponent<Image>(), new Color(0.05f, 0.07f, 0.18f, 0.8f), Teal, 12f, true);
        Transform selector = canvas.Find("HUD Selector Background");
        if (selector != null && selector.GetComponent<Image>() != null)
            MakePanel(selector.GetComponent<Image>(), new Color(0.05f, 0.07f, 0.18f, 0.85f), Teal, 12f, true);
        return n;
    }

    private static void HudText(Transform t, float canvasSize, Color col, TextAlignmentOptions align)
    {
        if (t == null) return;
        TextMeshProUGUI tx = t.GetComponent<TextMeshProUGUI>();
        if (tx == null) return;
        float sc = Mathf.Max(0.0001f, ScaleToCanvas((RectTransform)t));
        tx.font = Retro; tx.color = col; tx.enableAutoSizing = false;
        tx.fontSize = canvasSize / sc; tx.alignment = align;
        tx.textWrappingMode = TextWrappingModes.NoWrap; tx.overflowMode = TextOverflowModes.Overflow;
        EditorUtility.SetDirty(tx);
    }

    /// <summary>Sun counter, progress tag, Endless cycle/flag texts and Ready button of a level HUD.</summary>
    public static string StyleLevelHud(Transform canvas)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        RectTransform cv = (RectTransform)canvas;
        float plateLeft = -392f, plateBottom = 150f;
        Transform atom = canvas.Find("Atom Points Background");
        if (atom != null && atom.Find("Atom") != null && atom.Find("Sun Text") != null)
        {
            // the old pill art is hidden; a plate sized around the atom icon + number replaces it
            atom.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            ClearKit(atom);
            Vector3[] ac = new Vector3[4]; ((RectTransform)atom.Find("Atom")).GetWorldCorners(ac);
            Vector3[] tc = new Vector3[4]; ((RectTransform)atom.Find("Sun Text")).GetWorldCorners(tc);
            Vector3 a0 = cv.InverseTransformPoint(ac[0]), a2 = cv.InverseTransformPoint(ac[2]);
            Vector3 t0 = cv.InverseTransformPoint(tc[0]), t2 = cv.InverseTransformPoint(tc[2]);
            float left = Mathf.Max(a0.x - 5f, -392f), right = (t0.x + t2.x) * 0.5f + 30f;
            float bottom = a0.y - 3f, top = a2.y + 3f;
            RectTransform plate = Child(atom, "Plate");
            float ps = Mathf.Max(0.0001f, ScaleToCanvas((RectTransform)atom));
            plate.anchorMin = plate.anchorMax = new Vector2(0.5f, 0.5f);
            plate.pivot = new Vector2(0.5f, 0.5f);
            plate.sizeDelta = new Vector2(right - left, top - bottom) / ps;
            plate.position = cv.TransformPoint(new Vector3((left + right) * 0.5f, (top + bottom) * 0.5f, 0f));
            float ppum = 16f * Mathf.Max(0.0001f, ScaleToCanvas(plate)) / 10f;
            Img(plate, Plate, new Color(0.05f, 0.07f, 0.18f, 0.88f), ppum);
            RectTransform pr = Child(plate, "Ring"); Stretch(pr, 0, 0, 0, 0); Img(pr, Ring, Teal, ppum);
            plate.SetAsFirstSibling();
            HudText(atom.Find("Sun Text"), 14f, Color.white, TextAlignmentOptions.Midline);
            plateLeft = left; plateBottom = bottom;
            sb.Append("atom ");
        }
        Transform tag = canvas.Find("Level Progress Background");
        if (tag != null)
        {
            MakePanel(tag.GetComponent<Image>(), new Color(0.05f, 0.07f, 0.18f, 0.9f), Teal, 6f, false);
            HudText(tag.Find("Level Progress"), 5f, Color.white, TextAlignmentOptions.Midline);
            sb.Append("progress ");
        }
        string[] endlessTexts = { "Cycle Text/Cycle Value", "Flag Text/Flag Value" };
        for (int i = 0; i < endlessTexts.Length; i++)
        {
            Transform et = canvas.Find(endlessTexts[i]);
            if (et == null) continue;
            HudText(et, 7f, i == 0 ? Color.white : Amber, TextAlignmentOptions.MidlineLeft);
            RectTransform er = (RectTransform)et;
            er.pivot = new Vector2(0f, 0.5f);
            er.position = cv.TransformPoint(new Vector3(plateLeft + 4f, plateBottom - 10f - 13f * i, 0f));
        }
        Transform ready = canvas.Find("Ready Button");
        Transform slotPanel = canvas.Find("HUD Background Fixed");
        if (ready != null && ready.GetComponent<Button>() != null)
        {
            RectTransform rr = (RectTransform)ready;
            float rs = Mathf.Max(0.0001f, ScaleToCanvas(rr));
            rr.anchorMin = rr.anchorMax = new Vector2(0.5f, 0.5f);
            rr.pivot = new Vector2(0.5f, 0.5f);
            rr.sizeDelta = new Vector2(120f, 24f) / rs;
            if (slotPanel != null)
            {
                Vector3[] pc = new Vector3[4]; ((RectTransform)slotPanel).GetWorldCorners(pc);
                Vector3 p0 = cv.InverseTransformPoint(pc[0]), p2 = cv.InverseTransformPoint(pc[2]);
                rr.position = cv.TransformPoint(new Vector3((p0.x + p2.x) * 0.5f, p0.y - 16f, 0f));
            }
            StyleButton(ready.GetComponent<Button>(), Mint, 9f);
            ButtonLabel(ready, "READY");
            sb.Append("ready ");
        }
        return sb.ToString();
    }

    /// <summary>A real TMP label centred on a point given in canvas units (parent may be scaled).</summary>
    public static TextMeshProUGUI AddLabel(Transform parent, string text, Vector2 center, Vector2 size, float fontCanvas, Color col)
    {
        RectTransform rt = Child(parent, "Label");
        float ps = Mathf.Max(0.0001f, ScaleToCanvas((RectTransform)parent));
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = size / ps;
        rt.anchoredPosition = center / ps;
        TextMeshProUGUI t = Txt(rt, text, fontCanvas / ps, col, TextAlignmentOptions.Midline);
        t.overflowMode = TextOverflowModes.Overflow;
        return t;
    }

    private static void ButtonLabel(Transform button, string label)
    {
        if (button.GetComponentInChildren<TextMeshProUGUI>(true) == null)
        {
            // some buttons never had a text object (label baked into the sprite): give them one
            RectTransform lr = Child(button, "Label");
            Stretch(lr, 10f, 4f, 10f, 4f);
            float lsc = Mathf.Max(0.0001f, ScaleToCanvas(lr));
            TextMeshProUGUI nt = Txt(lr, label, 8f / lsc, Color.white, TextAlignmentOptions.Midline);
            nt.enableAutoSizing = true;
            nt.fontSizeMin = 4f / lsc;
            nt.fontSizeMax = 8f / lsc;
            nt.overflowMode = TextOverflowModes.Ellipsis;
        }
        foreach (TextMeshProUGUI t in button.GetComponentsInChildren<TextMeshProUGUI>(true))
        {
            t.gameObject.SetActive(true);   // old labels were off: the text used to be baked into the sprite
            t.text = label;
            t.color = Color.white;          // the hidden labels kept a faded colour
        }
    }

    /// <summary>
    /// Pause / Win / Lose overlays of a level canvas: navy plate + accent ring, a real title and
    /// labelled plate buttons in place of the baked-text sprites. Missing pieces are skipped.
    /// </summary>
    public static string StyleLevelOverlays(Transform canvas)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        Transform pause = canvas.Find("Pause");
        if (pause != null)
        {
            MakePanel(pause.GetComponent<Image>(), new Color(0.05f, 0.07f, 0.18f, 0.97f), Teal, 14f, true);
            AddLabel(pause, "JEDA", new Vector2(0f, 52f), new Vector2(300f, 26f), 17f, Color.white);
            Transform r = pause.Find("Button Pause Resume");
            Transform l = pause.Find("Button Pause Level Selection");
            if (r != null) { StyleButton(r.GetComponent<Button>(), Mint, 8f); ButtonLabel(r, "LANJUTKAN"); }
            if (l != null) { StyleButton(l.GetComponent<Button>(), Teal, 8f); ButtonLabel(l, "PILIH LEVEL"); }
            sb.Append("pause ");
        }
        Transform win = canvas.Find("Win UI (1)");
        if (win != null)
        {
            RectTransform wr = (RectTransform)win;
            wr.localScale = Vector3.one;
            wr.sizeDelta = new Vector2(300f, 170f);
            MakePanel(win.GetComponent<Image>(), new Color(0.05f, 0.07f, 0.18f, 0.97f), Amber, 14f, true);
            AddLabel(win, "KAMU MENANG!", new Vector2(0f, 38f), new Vector2(280f, 26f), 17f, Amber);
            PlaceButton(win.Find("Button Win Next Level"), new Vector2(76f, -42f), new Vector2(132f, 32f), Mint, "LEVEL BERIKUTNYA");
            PlaceButton(win.Find("Button Pause Level Selection"), new Vector2(-76f, -42f), new Vector2(132f, 32f), Teal, "PILIH LEVEL");
            sb.Append("win ");
        }
        Transform death = canvas.Find("Death");
        if (death != null)
        {
            Transform bg = death.Find("Fade/Tombol Kalah BG");
            if (bg != null)
            {
                Image bgImage = bg.GetComponent<Image>();
                MakePanel(bgImage, new Color(0.05f, 0.07f, 0.18f, 0f), Coral, 14f, true);   // alpha 0: DeathAnimation fades it in
                AddLabel(bg, "KAMU KALAH", new Vector2(0f, 32f), new Vector2(300f, 26f), 17f, Coral);
                CanvasGroup cg = bg.GetComponent<CanvasGroup>();
                if (cg == null) cg = bg.gameObject.AddComponent<CanvasGroup>();
                cg.alpha = 0f;
                if (bg.GetComponent<ImageAlphaFollower>() == null) bg.gameObject.AddComponent<ImageAlphaFollower>();
            }
            Transform rt2 = death.Find("Button Lose Retry");
            Transform ls = death.Find("Button Pause Level Selection");
            if (rt2 != null) { StyleButton(rt2.GetComponent<Button>(), Coral, 8f); ButtonLabel(rt2, "ULANGI"); }
            if (ls != null) { StyleButton(ls.GetComponent<Button>(), Teal, 8f); ButtonLabel(ls, "PILIH LEVEL"); }
            // Endless adds its two end-of-run stats; pull them inside the plate
            string[] statNames = { "Sun Value End", "Flag End" };
            float[] statX = { -95f, 95f };
            for (int i = 0; i < statNames.Length; i++)
            {
                Transform st = death.Find(statNames[i]);
                if (st == null) continue;
                RectTransform sr = (RectTransform)st;
                sr.localScale = Vector3.one;
                sr.anchorMin = sr.anchorMax = new Vector2(0.5f, 0.5f);
                sr.pivot = new Vector2(0.5f, 0.5f);
                sr.sizeDelta = new Vector2(190f, 20f);
                sr.anchoredPosition = new Vector2(statX[i], 4f);
                TextMeshProUGUI stt = st.GetComponent<TextMeshProUGUI>();
                stt.font = Retro; stt.color = TextLight; stt.enableAutoSizing = false; stt.fontSize = 8f;
                stt.alignment = TextAlignmentOptions.Midline;
                stt.textWrappingMode = TextWrappingModes.NoWrap; stt.overflowMode = TextOverflowModes.Overflow;
                EditorUtility.SetDirty(stt);
            }
            sb.Append("death ");
        }
        return sb.ToString();
    }

    private static void PlaceButton(Transform t, Vector2 center, Vector2 size, Color accent, string label)
    {
        if (t == null) return;
        RectTransform rt = (RectTransform)t;
        float ps = Mathf.Max(0.0001f, ScaleToCanvas((RectTransform)t.parent));
        float s = Mathf.Max(0.0001f, Mathf.Abs(rt.localScale.x));
        rt.localScale = new Vector3(s, s, 1f);
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = size / (s * ps);
        rt.anchoredPosition = center / ps;
        StyleButton(t.GetComponent<Button>(), accent, 8f);
        ButtonLabel(t, label);
    }

    public static Sprite Trophy { get { return Load("Assets/Art/UI Kit/UITrophy.png"); } }

    /// <summary>
    /// Re-skins a leaderboard canvas (the "HighScore Table" prefab-like block): background, title banner,
    /// plate, new column labels/positions, zebra row plates and a trophy slot in the rank cell.
    /// </summary>
    public static void StyleLeaderboard(Transform canvas, string bannerText, float scale = 0.72f, float centerY = -32f)
    {
        Transform bg = canvas.Find("background");
        if (bg != null) StyleBackground(bg.GetComponent<Image>(), 0.72f);
        ClearKit(canvas);
        StyleBackButtons(canvas);

        Transform table = canvas.Find("HighScore Table");
        if (table == null) return;
        RectTransform tr = (RectTransform)table;
        tr.localScale = new Vector3(scale, scale, 1f);
        float sc = ScaleToCanvas(tr);

        // plate geometry from the real header / row positions (they differ per scene)
        Transform headerRef = table.Find("POS Table");
        Transform container = table.Find("HighscoreEntryContainer");
        float headerY = headerRef != null ? ((RectTransform)headerRef).anchoredPosition.y : 189f;
        float row0 = 141f;
        Transform tmplRef = container != null ? container.Find("HighScoreEntryTemplate") : null;
        if (container != null && tmplRef != null && tmplRef.Find("Pos") != null)
            row0 = ((RectTransform)container).anchoredPosition.y + ((RectTransform)tmplRef.Find("Pos")).anchoredPosition.y;
        float top = headerY + 28f, bottom = row0 - 360f - 24f;
        float plateCenter = (top + bottom) * 0.5f;
        tr.anchoredPosition = new Vector2(0f, centerY - plateCenter * scale);

        Transform nb = table.Find("New Background");
        if (nb != null)
        {
            RectTransform nr = (RectTransform)nb;
            nr.localScale = Vector3.one;
            nr.anchorMin = nr.anchorMax = new Vector2(0.5f, 0.5f);
            nr.pivot = new Vector2(0.5f, 0.5f);
            nr.sizeDelta = new Vector2(920f, top - bottom);
            nr.anchoredPosition = new Vector2(0f, plateCenter);
            MakePanel(nb.GetComponent<Image>(), new Color(0.05f, 0.07f, 0.18f, 0.82f), Teal, 14f, true);
        }
        Transform hs = table.Find("High Score");
        if (hs != null) hs.gameObject.SetActive(false);

        // header name, row name, label, x, width, alignment
        object[][] cols = new object[][]
        {
            new object[]{ "POS Table",   "Pos",     "RANK",    -372f,  90f, TextAlignmentOptions.Midline },
            new object[]{ "Name TABLE",  "Name",    "NAME",    -215f, 210f, TextAlignmentOptions.MidlineLeft },
            new object[]{ "Flag Table",  "Flag",    "WAVE",     -50f,  90f, TextAlignmentOptions.Midline },
            new object[]{ "Sun Table",   "Sun",     "ENERGY",    60f, 120f, TextAlignmentOptions.Midline },
            new object[]{ "Nucleus",     "Nucleus", "NUCLEUS",  185f, 130f, TextAlignmentOptions.Midline },
            new object[]{ "Score",       "Score",   "SCORE",    345f, 150f, TextAlignmentOptions.Midline },
        };
        Transform template = table.Find("HighscoreEntryContainer/HighScoreEntryTemplate");
        foreach (object[] c in cols)
        {
            Transform h = table.Find((string)c[0]);
            if (h != null) StyleCell(h, (string)c[2], (float)c[3], (float)c[4], (TextAlignmentOptions)c[5], Retro, Teal, 8.5f / sc, true);
            if (template != null)
            {
                Transform cell = template.Find((string)c[1]);
                bool name = (string)c[1] == "Name";
                if (cell != null) StyleCell(cell, null, (float)c[3], (float)c[4], (TextAlignmentOptions)c[5], name ? BodyFont : Retro, name ? TextLight : Color.white, (name ? 11f : 8.5f) / sc, false);
            }
        }
        // header baseline stays, template rows keep y; add zebra plate + trophy slot
        if (template != null)
        {
            for (int i = template.childCount - 1; i >= 0; i--)
                if (template.GetChild(i).name == "Row BG" || template.GetChild(i).name == "Trophy")
                    Object.DestroyImmediate(template.GetChild(i).gameObject);
            float rowY = ((RectTransform)template.Find("Pos")).anchoredPosition.y;
            GameObject rb = new GameObject("Row BG", typeof(RectTransform));
            rb.layer = 5; rb.transform.SetParent(template, false); rb.transform.SetAsFirstSibling();
            RectTransform rbr = (RectTransform)rb.transform;
            rbr.anchorMin = rbr.anchorMax = new Vector2(0.5f, 0.5f);
            rbr.sizeDelta = new Vector2(880f, 34f); rbr.anchoredPosition = new Vector2(0f, rowY);
            Img(rbr, Plate, new Color(0.1f, 0.15f, 0.34f, 0.3f), 16f * sc / 8f);
            GameObject tg = new GameObject("Trophy", typeof(RectTransform));
            tg.layer = 5; tg.transform.SetParent(template, false);
            RectTransform tgr = (RectTransform)tg.transform;
            tgr.anchorMin = tgr.anchorMax = new Vector2(0.5f, 0.5f);
            tgr.sizeDelta = new Vector2(30f, 30f); tgr.anchoredPosition = new Vector2(-418f, rowY);
            Image ti = Img(tgr, Trophy, Color.white, 1f, false);
            ti.preserveAspect = true;
            tg.SetActive(false);
        }
        RectTransform title = Child(canvas, "Title");
        Img(title, Plate, Navy, 1f);
        StyleTitleBanner(title, bannerText, Teal);
        EditorUtility.SetDirty(canvas.gameObject);
    }

    private static void StyleCell(Transform cell, string text, float x, float w, TextAlignmentOptions align, TMP_FontAsset font, Color color, float size, bool header)
    {
        TextMeshProUGUI t = cell.GetComponent<TextMeshProUGUI>();
        if (t == null) return;
        RectTransform r = (RectTransform)cell;
        r.sizeDelta = new Vector2(w, header ? 40f : 40f);
        r.anchoredPosition = new Vector2(x, r.anchoredPosition.y);
        t.font = font; t.color = color; t.alignment = align;
        t.enableAutoSizing = false; t.fontSize = size;
        t.textWrappingMode = TextWrappingModes.NoWrap; t.overflowMode = TextOverflowModes.Ellipsis;
        if (text != null) t.text = text;
        EditorUtility.SetDirty(t);
    }

    /// <summary>Full-screen biotech background + soft vignette for a canvas background Image.</summary>
    public static void StyleBackground(Image bg, float dim = 0.72f)
    {
        bg.sprite = Background;
        bg.type = Image.Type.Simple;
        bg.preserveAspect = false;
        bg.color = new Color(dim, dim, dim, 1f);
        RectTransform rt = (RectTransform)bg.transform;
        ClearKit(rt);
        RectTransform v = Child(rt, "Vignette");
        Stretch(v, 0, 0, 0, 0);
        v.SetAsFirstSibling();   // behind the panel's own content, otherwise it dims the title/back button
        Img(v, Vignette, Color.white, 1f, false);
        EditorUtility.SetDirty(bg.gameObject);
    }
}
#endif
