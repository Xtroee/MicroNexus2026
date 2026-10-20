#if UNITY_EDITOR
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Editor-only builder that extends RangerGroup.prefab (the Bio-File / Halaman Belajar content) with the
/// iterasi 2 characters (Ceftria, Clarith, Colistin, Doxy, Linezo, Merope, Penicilin) and the seven stage 7-13
/// bacteria, and rebuilds every ranger page with the 14-bacteria effectiveness grid.
/// Re-running it is safe: objects are looked up by name and reused/overwritten.
/// </summary>
public static class BioFileBuilder
{
    private const string PrefabPath = "Assets/Prefab/RangerBio/RangerGroup.prefab";

    // ------------------------------------------------------------------ data
    public class Bacterium
    {
        public string key;          // used in object names ("Bacteria <key>")
        public string display;      // name shown in the game
        public Color tone;
        public string uiController; // Assets/Animation/Bio UI/<x>BioIdle.controller (new bacteria only)
        public string firstFrame;   // sprite for the list card (new bacteria only)
    }

    public class Ranger
    {
        public string key, display, icon;
        public string lore, profile;
        public int[] stars;         // 14, order = AllBacteria
        public Color tone;
    }

    // 14 bacteria: the original 7 first (same order as the old baked grid), then the 7 new ones
    public static readonly Bacterium[] AllBacteria =
    {
        new Bacterium{ key="Purple", display="Streptococcus pneumoniae", tone=MicroNexusUIKit.Violet },
        new Bacterium{ key="Yellow", display="Staphylococcus aureus (MSSA)", tone=MicroNexusUIKit.Amber },
        new Bacterium{ key="Red", display="Moraxella catarrhalis", tone=new Color(1f,0.45f,0.5f,0.95f) },
        new Bacterium{ key="Orange", display="MRSA", tone=new Color(1f,0.65f,0.3f,0.95f) },
        new Bacterium{ key="Blue", display="Haemophilus influenzae", tone=new Color(0.4f,0.7f,1f,0.95f) },
        new Bacterium{ key="Pink", display="Escherichia coli", tone=new Color(1f,0.62f,0.82f,0.95f) },
        new Bacterium{ key="Green", display="Pseudomonas aeruginosa", tone=MicroNexusUIKit.Mint },
        new Bacterium{ key="Klebsiella", display="Klebsiella pneumoniae (ESBL)", tone=new Color(0.2f,0.75f,0.75f,0.95f), uiController="KlebsiellapneumoniaeESBL", firstFrame="Klebsiella pneumoniae ESBL" },
        new Bacterium{ key="Neisseria", display="Neisseria gonorrhoeae", tone=new Color(0.8f,0.6f,0.4f,0.95f), uiController="Neisseriagonorrhoeae", firstFrame="Neisseria gonorrhoeae" },
        new Bacterium{ key="Vre", display="Enterococcus faecium (VRE)", tone=new Color(0.45f,0.4f,0.85f,0.95f), uiController="EnterococcusfaeciumVRE", firstFrame="Enterococcus faecium VRE" },
        new Bacterium{ key="HPylori", display="Helicobacter pylori", tone=new Color(0.72f,0.84f,0.25f,0.95f), uiController="Helicobacterpylori", firstFrame="Helicobacter pylori" },
        new Bacterium{ key="SPyogenes", display="Streptococcus pyogenes", tone=new Color(1f,0.55f,0.48f,0.95f), uiController="Streptococcuspyogenes", firstFrame="Streptococcus pyogenes" },
        new Bacterium{ key="Crab", display="Acinetobacter baumannii (CRAB)", tone=new Color(0.55f,0.62f,0.72f,0.95f), uiController="AcinetobacterbaumanniiCRAB", firstFrame="Acinetobacter baumannii CRAB" },
        new Bacterium{ key="Chlamydia", display="Chlamydia trachomatis", tone=new Color(0.55f,0.92f,0.8f,0.95f), uiController="Chlamydiatrachomatis", firstFrame="Chlamydia trachomatis" },
    };

    private static string Profile(string cls, string indications, string target, string adr, string link)
    {
        return "<b>Class:</b> " + cls + "\r\n\r\n<b>Indications:</b>\r\n" + indications + "\r\n\r\n<b>Target Bacteria:</b>\r\n" + target +
               "\r\n\r\n<b>Adverse Drug Reactions (ADR):</b>\r\n" + adr + "\r\n\r\n<b>LINK:</b>\r\n<link=\"" + link + "\">Click here to see more</link>\r\n";
    }

    public static readonly Ranger[] NewRangers =
    {
        new Ranger{ key="Ceftria", display="Ceftriaxone", icon="Assets/Animation/Character 2/Ceftria/ceftriaIcon.png", tone=new Color(0.62f,0.45f,0.9f,0.95f),
            stars=new[]{5,3,4,0,5,5,0, 1,5,0,0,5,0,0},
            lore="Ceftria is a precise long-range marksman of the frontline, armed with a syringe-fed injector. Built from a third-generation cephalosporin, it breaks down the cell walls of fast-growing bacteria before they can regroup. It is deadly against the gonorrhea-causing Neisseria and most gram-negative invaders, yet it can do nothing against Pseudomonas or MRSA. Strike early with Ceftria and the lane stays quiet.",
            profile=Profile("β-lactam antibiotic; third-generation cephalosporin (injectable)",
                "Gonorrhea, bacterial meningitis, community-acquired and nosocomial pneumonia, complicated urinary tract infections, septicemia, skin and soft tissue infections",
                "Neisseria gonorrhoeae, Streptococcus pneumoniae, Haemophilus influenzae, Moraxella catarrhalis, Escherichia coli, Streptococcus pyogenes",
                "Diarrhea, nausea, pain at the injection site, biliary sludge (pseudolithiasis), hypersensitivity reactions", "https://www.rxlist.com/rocephin-drug.htm") },
        new Ranger{ key="Clarith", display="Clarithromycin", icon="Assets/Animation/Character 2/Clarith/clarithIcon.png", tone=new Color(1f,0.55f,0.4f,0.95f),
            stars=new[]{4,2,3,0,2,0,0, 0,1,0,5,3,0,2},
            lore="Clarith is a patient hunter that goes after the foes nobody else can reach. As a macrolide antibiotic it halts protein production inside bacteria, and it shines against Helicobacter pylori, the spiral invader of the stomach. On its own its range of targets is narrow, but fighting beside Amoxicillin it becomes part of the strongest combination on the field.",
            profile=Profile("Macrolide antibiotic",
                "Respiratory tract infections, skin infections, combination therapy (with a PPI + Amoxicillin) to eradicate Helicobacter pylori",
                "Helicobacter pylori, Streptococcus pneumoniae, Haemophilus influenzae, Moraxella catarrhalis, Mycobacterium avium complex",
                "Gastrointestinal disturbances (a metallic taste is common), QT interval prolongation, drug interactions (CYP3A4 inhibitor), hepatotoxicity (rare)", "https://www.rxlist.com/biaxin-drug.htm") },
        new Ranger{ key="Colistin", display="Colistin", icon="Assets/Animation/Character 2/Colistin/colistinIcon.png", tone=new Color(0.58f,0.72f,0.3f,0.95f),
            stars=new[]{0,0,1,0,1,1,2, 2,0,0,0,0,5,0},
            lore="Colistin is the last resort of the battlefield, a veteran that was almost retired before the world needed it again. It barely scratches ordinary bacteria, but against the carbapenem-resistant Acinetobacter baumannii nothing hits harder. Its power comes at a cost, so call on Colistin only when every other defender has failed.",
            profile=Profile("Polymyxin antibiotic (last-line drug)",
                "Serious infections caused by multidrug-resistant Gram-negative bacteria, including carbapenem-resistant Acinetobacter baumannii (CRAB), when other options have failed",
                "Acinetobacter baumannii (CRAB), Pseudomonas aeruginosa (MDR), Klebsiella pneumoniae (carbapenem-resistant)",
                "Nephrotoxicity (common, dose-limiting), neurotoxicity (paresthesia, dizziness), respiratory depression (rare), hypersensitivity reactions", "https://www.rxlist.com/coly-mycin-m-drug.htm") },
        new Ranger{ key="Doxy", display="Doxycycline", icon="Assets/Animation/Character 2/Doxy/doxyIcon.png", tone=new Color(0.85f,0.32f,0.26f,0.95f),
            stars=new[]{3,2,3,3,2,1,0, 1,1,1,2,1,1,5},
            lore="Doxy is a tireless support fighter with a wide but modest reach. As a tetracycline antibiotic it shuts down the machinery bacteria use to build proteins, which lets it follow enemies even inside cells. Its true specialty is Chlamydia trachomatis, a foe that hides where most defenders cannot attack.",
            profile=Profile("Tetracycline antibiotic",
                "Chlamydia trachomatis infections, acne, respiratory tract infections, Lyme disease, malaria prophylaxis",
                "Chlamydia trachomatis, Mycoplasma pneumoniae, Rickettsia spp., Borrelia burgdorferi",
                "Photosensitivity, gastrointestinal disturbances, esophageal irritation, tooth discoloration (contraindicated in children and pregnancy), dizziness (vestibular effects)", "https://www.rxlist.com/vibramycin-drug.htm") },
        new Ranger{ key="Linezo", display="Linezolid", icon="Assets/Animation/Character 2/Linezo/linezoIcon.png", tone=new Color(0.4f,0.75f,0.98f,0.95f),
            stars=new[]{4,4,0,5,0,0,0, 0,0,5,0,4,0,0},
            lore="Linezo is the gram-positive specialist, a cool-headed researcher armed with an oxazolidinone shot. Gram-negative bacteria shrug off its attacks completely, but MRSA and the vancomycin-resistant Enterococcus fall quickly. When vancomycin stops working, Linezo is the one the squad calls.",
            profile=Profile("Oxazolidinone antibiotic",
                "Vancomycin-resistant Enterococcus faecium (VRE) infections, community-acquired and nosocomial pneumonia, complicated skin and soft tissue infections including MRSA",
                "Enterococcus faecium (VRE), Staphylococcus aureus (including MRSA), Streptococcus pneumoniae, Streptococcus pyogenes",
                "Thrombocytopenia, peripheral/optic neuropathy (prolonged use), serotonin syndrome (with serotonergic drugs), gastrointestinal disturbances", "https://www.rxlist.com/zyvox-drug.htm") },
        new Ranger{ key="Merope", display="Meropenem", icon="Assets/Animation/Character 2/Merope/meropeIcon.png", tone=new Color(1f,0.7f,0.2f,0.95f),
            stars=new[]{3,4,4,0,5,5,5, 5,2,0,0,5,0,0},
            lore="Merope is the heavy hitter of the squad, a broad-spectrum carbapenem with a punch that reaches almost every gram-negative enemy. ESBL-producing Klebsiella, E. coli and Pseudomonas all fall in a few hits. Even so, MRSA, VRE and CRAB are immune to it, so Merope needs allies who can cover those weaknesses.",
            profile=Profile("β-lactam antibiotic; carbapenem (broad-spectrum)",
                "Severe or complicated infections including nosocomial pneumonia, complicated intra-abdominal infections, complicated urinary tract infections, bacterial meningitis, and infections caused by ESBL/MDR Gram-negative organisms",
                "Klebsiella pneumoniae (ESBL), Escherichia coli (ESBL), Pseudomonas aeruginosa, Haemophilus influenzae, Streptococcus pneumoniae, Enterobacterales",
                "Nausea, vomiting, diarrhea, headache, injection-site reactions, seizures (rare, dose-related), hypersensitivity reactions", "https://www.rxlist.com/merrem-drug.htm") },
        new Ranger{ key="Penicilin", display="Penicillin V", icon="Assets/Animation/Character 2/Penicilin/penicilinIcon.png", tone=new Color(0.7f,0.48f,0.3f,0.95f),
            stars=new[]{4,0,0,0,1,0,0, 0,0,0,0,5,0,0},
            lore="Penicilin is the old veteran of the roster, the narrowest but sharpest fighter on the field. Natural penicillin has been the gold standard against Streptococcus pyogenes, the cause of strep throat, for decades. Outside that one specialty its reach is tiny, so place Penicilin where it counts.",
            profile=Profile("β-lactam antibiotic; natural penicillin (narrow spectrum)",
                "Streptococcal pharyngitis (\"strep throat\"), prophylaxis against recurrence of rheumatic fever, mild skin infections caused by susceptible streptococci",
                "Streptococcus pyogenes (Group A Streptococcus), Streptococcus pneumoniae (susceptible strains)",
                "Nausea, diarrhea, skin rash, hypersensitivity reactions including anaphylaxis (rare but serious)", "https://www.rxlist.com/penicillin-vk-drug.htm") },
    };

    // iterasi 1: the stars that used to be baked into the old panels (original 7 bacteria) + new 7 from the matrix
    private static readonly string[] OldKeys = { "Amoxicillin", "Vancomisin", "Cefixime", "Azitromisin", "Ciprofloxacin", "Levofloksasin", "Cotrimoxazole" };
    private static readonly int[][] OldStars =
    {
        new[]{4,4,1,0,2,1,0, 0,1,0,3,5,0,2},
        new[]{5,5,0,5,0,0,0, 0,0,0,0,5,0,0},
        new[]{1,1,3,0,4,5,0, 1,3,0,0,3,0,0},
        new[]{4,3,3,0,2,0,0, 1,2,0,1,3,0,4},
        new[]{1,1,1,0,3,5,5, 1,1,1,2,1,1,1},
        new[]{5,2,2,2,2,3,3, 1,1,1,4,3,1,3},
        new[]{2,3,2,3,2,4,0, 2,0,1,0,0,1,0},
    };
    private static readonly Color[] OldTones =
    {
        new Color(0.88f,0.95f,1f,0.95f), new Color(0.4f,0.7f,1f,0.95f), new Color(1f,0.62f,0.82f,0.95f), new Color(1f,0.45f,0.5f,0.95f),
        MicroNexusUIKit.Mint, MicroNexusUIKit.Amber, MicroNexusUIKit.Violet
    };

    // bacteria detail data -----------------------------------------------------------
    public class NewBacteriumText { public string lore, detail, panelKey; }

    private static string Bullets(string header, params string[] items)
    {
        var sb = new StringBuilder("<b>" + header + "</b>\r\n\r\n");
        foreach (var i in items) sb.Append("- " + i + "\r\n\r\n");
        return sb.ToString();
    }

    private static string BacDetail(string title, string[] brief, string[] habitat, string[] diseases, string[] antibiotics)
    {
        return "<b>" + title + "</b>\r\n\r\n" + Bullets("Brief Characteristics:", brief) + Bullets("Habitat:", habitat) +
               Bullets("Major Diseases:", diseases) + Bullets("Effective Antibiotics:", antibiotics);
    }

    public static readonly NewBacteriumText[] NewBacteriaText =
    {
        new NewBacteriumText{ panelKey="Klebsiella",
            lore="Klebsiella pneumoniae (ESBL) enters the battlefield wrapped in a thick protective capsule, shrugging off almost every beta-lactam attack. It lurks in hospitals, riding on ventilators and catheters, waiting for weakened patients. Once it takes hold, it spreads through the lungs and urinary tract while its enzymes dismantle the weapons built to stop it. Only the strongest carbapenem can break through, reminding players that resistance can turn ordinary enemies into critical threats.",
            detail=BacDetail("Klebsiella pneumoniae – ESBL-producing (Gram-negative)",
                new[]{"Encapsulated rod","Non-motile","Produces Extended-Spectrum Beta-Lactamase (ESBL) that inactivates most penicillins and cephalosporins"},
                new[]{"Normal gut flora","Hospital environment (ventilators, catheters)"},
                new[]{"Nosocomial pneumonia","Urinary tract infection","Bacteremia","Liver abscess"},
                new[]{"Meropenem (carbapenem, first-line for ESBL strains)","Colistin (if carbapenem-resistant)","Amikacin (certain cases)"}) },
        new NewBacteriumText{ panelKey="Neisseria",
            lore="Neisseria gonorrhoeae moves in pairs, two coffee-bean shaped cells that cling together as they cross the battlefield. It slips inside the host's own white blood cells and settles in the mucous membranes, spreading quietly from one host to the next. Resistance grows with every season, so only a swift, well-aimed strike can stop it before it spreads. Treat it early, and the pair never makes it past the first lane.",
            detail=BacDetail("Neisseria gonorrhoeae (Gram-negative)",
                new[]{"Gram-negative diplococci (\"coffee bean\" shape)","Intracellular inside neutrophils","No capsule"},
                new[]{"Mucosal surfaces of the urogenital tract, rectum and pharynx"},
                new[]{"Gonorrhea (urethritis, cervicitis)","Pharyngeal and rectal gonorrhea","Disseminated gonococcal infection","Pelvic inflammatory disease"},
                new[]{"Ceftriaxone (single IM dose, CDC first-line)","Cefixime (oral alternative)"}) },
        new NewBacteriumText{ panelKey="Vre",
            lore="Enterococcus faecium (VRE) marches as a tight chain of tough little cells that survive almost anywhere. It lives quietly in the gut but turns dangerous during long hospital stays, striking catheters and the bloodstream. Its armor was built to defeat vancomycin, the weapon once trusted against every gram-positive foe. Only a newer class of antibiotic can crack its defenses.",
            detail=BacDetail("Enterococcus faecium – VRE (Gram-positive)",
                new[]{"Cocci in pairs or short chains","Extremely tolerant of harsh environments","vanA/vanB genes make it vancomycin-resistant"},
                new[]{"Normal gut flora","Persists on hospital surfaces and equipment"},
                new[]{"Bacteremia","Catheter-associated urinary tract infection","Endocarditis","Intra-abdominal infection (usually in hospitalized or immunocompromised patients)"},
                new[]{"Linezolid (first-line, FDA-approved for VRE)","Daptomycin (not for pneumonia)"}) },
        new NewBacteriumText{ panelKey="HPylori",
            lore="Helicobacter pylori twists across the battlefield like a living corkscrew, driving itself forward with a bundle of flagella. It survives the harsh acid of the stomach by producing urease, neutralizing the world around it. Left alone, it carves ulcers into the stomach wall and quietly raises the risk of cancer. Defeating it takes teamwork: more than one antibiotic must strike together.",
            detail=BacDetail("Helicobacter pylori (Gram-negative)",
                new[]{"Spiral (helical) rod","Motile with flagella","Produces urease to survive stomach acid"},
                new[]{"Gastric mucosa"},
                new[]{"Chronic gastritis","Gastric and duodenal ulcers","MALT lymphoma","Risk factor for gastric cancer"},
                new[]{"Clarithromycin + Amoxicillin (standard triple therapy with a PPI)","Levofloxacin (salvage regimen)"}) },
        new NewBacteriumText{ panelKey="SPyogenes",
            lore="Streptococcus pyogenes advances as a curved chain of round cells, each one armed with toxins and surface proteins that blind the host's defenses. It starts in the throat, where it causes the sharp pain known as strep throat. If ignored, it can leave lasting damage to the heart and kidneys long after the battle seems won. Fortunately, an old and reliable weapon still brings it down every time.",
            detail=BacDetail("Streptococcus pyogenes – Group A Strep / GABHS (Gram-positive)",
                new[]{"Cocci in chains","Beta-hemolytic","Many virulence factors (M protein, streptolysin)"},
                new[]{"Nasopharynx","Skin"},
                new[]{"Strep throat","Scarlet fever","Impetigo","Cellulitis","Necrotizing fasciitis","Post-infection rheumatic fever and glomerulonephritis"},
                new[]{"Penicillin V (first-line, GABHS remains universally susceptible)","Amoxicillin (equally effective alternative)"}) },
        new NewBacteriumText{ panelKey="Crab",
            lore="Acinetobacter baumannii (CRAB) is the armored heavyweight of the infection ward, a hospital survivor that can live for weeks on dry surfaces. It has learned to resist even carbapenems, the strongest antibiotics most defenders can offer. Ventilators, wounds and medical devices are its hunting ground. When every normal weapon fails, only an old last-resort drug is left to face it.",
            detail=BacDetail("Acinetobacter baumannii – CRAB (Gram-negative)",
                new[]{"Coccobacillus","Survives for weeks on dry surfaces","Carbapenem-resistant strains are the WHO critical priority pathogen #1 (2024)"},
                new[]{"Hospital environment (ventilators, medical devices, surfaces)","Water"},
                new[]{"Ventilator-associated pneumonia","Bacteremia","Wound infections (especially war and trauma wounds)","Device-associated meningitis"},
                new[]{"Colistin (last-line drug)","Ampicillin–sulbactam (certain susceptible strains)"}) },
        new NewBacteriumText{ panelKey="Chlamydia",
            lore="Chlamydia trachomatis is a quiet infiltrator that cannot live outside a host cell, so it hides inside the very cells it attacks. Often it causes no symptoms at all, spreading unseen until it leaves damage behind. Because it lives inside cells, only antibiotics that can follow it in can reach it. Once found, the right drug clears it completely.",
            detail=BacDetail("Chlamydia trachomatis (obligate intracellular)",
                new[]{"Obligate intracellular bacterium (cannot replicate outside host cells)","No classic peptidoglycan wall","Two forms (elementary body / reticulate body)"},
                new[]{"Columnar epithelial cells of the urogenital tract, conjunctiva and rectum"},
                new[]{"Chlamydia (urethritis, cervicitis – often without symptoms)","Pelvic inflammatory disease","Infertility","Trachoma (ocular, endemic regions)","Neonatal conjunctivitis and pneumonia"},
                new[]{"Doxycycline (first-line, CDC since 2021)","Azithromycin (alternative, e.g. in pregnancy)"}) },
    };

    // ------------------------------------------------------------------ helpers
    private static Transform Need(Transform parent, string path)
    {
        Transform t = parent.Find(path);
        if (t == null) throw new System.Exception("Missing '" + path + "' under " + parent.name);
        return t;
    }

    private static T Clone<T>(T source, Transform parent, string newName) where T : Object
    {
        T clone = Object.Instantiate(source, parent);
        clone.name = newName;
        return clone;
    }

    /// <summary>Re-point the n-th persistent call of a Button to another method on the same target (optionally a new target).</summary>
    private static void SetCall(Button b, int index, string method, Object newTarget = null, Object objectArg = null)
    {
        SerializedObject so = new SerializedObject(b);
        SerializedProperty calls = so.FindProperty("m_OnClick.m_PersistentCalls.m_Calls");
        SerializedProperty call = calls.GetArrayElementAtIndex(index);
        call.FindPropertyRelative("m_MethodName").stringValue = method;
        if (newTarget != null) call.FindPropertyRelative("m_Target").objectReferenceValue = newTarget;
        if (objectArg != null)
        {
            call.FindPropertyRelative("m_Mode").intValue = 2; // Object
            call.FindPropertyRelative("m_Arguments.m_ObjectArgument").objectReferenceValue = objectArg;
            call.FindPropertyRelative("m_Arguments.m_ObjectArgumentAssemblyTypeName").stringValue = "UnityEngine.GameObject, UnityEngine.CoreModule";
        }
        so.ApplyModifiedPropertiesWithoutUndo();
    }

    private static void SetManagerData(SerializedObject so, string field, GameObject picture, GameObject button, string name, string description)
    {
        SerializedProperty p = so.FindProperty(field);
        p.FindPropertyRelative("pictureObj").objectReferenceValue = picture;
        p.FindPropertyRelative("button").objectReferenceValue = button;
        p.FindPropertyRelative("name").stringValue = name;
        p.FindPropertyRelative("description").stringValue = description;
    }

    private static void SetController(GameObject go, string uiControllerName)
    {
        Animator an = go.GetComponent<Animator>();
        an.runtimeAnimatorController = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>("Assets/Animation/Bio UI/" + uiControllerName + "BioIdle.controller");
    }

    private static void Place(RectTransform rt, Vector2 center, float w, float h)
    {
        float ps = MicroNexusUIKit.ScaleToCanvas((RectTransform)rt.parent);
        float sx = rt.localScale.x, sy = rt.localScale.y;
        float s = Mathf.Abs((sx + sy) * 0.5f); if (s < 0.0001f) s = 1f;
        rt.localScale = new Vector3(sx < 0 ? -s : s, s, 1f);
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f); rt.pivot = new Vector2(0.5f, 0.5f);
        rt.sizeDelta = new Vector2(w / (s * ps), h / (s * ps));
        rt.anchoredPosition = center / ps;
    }

    // ------------------------------------------------------------------ ranger detail pages
    /// <summary>Rebuilds the 14-bacteria effectiveness grid of one ranger page.</summary>
    public static void BuildEffectivenessGrid(Transform page, Color accent, int[] stars)
    {
        Sprite starSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Art/UI Kit/UIStar.png");
        for (int c = page.childCount - 1; c >= 0; c--)
            if (page.GetChild(c).name.StartsWith("UIK Card ")) Object.DestroyImmediate(page.GetChild(c).gameObject);

        // make sure every bacterium has its animated icon on this page
        Transform template = Need(page, "Bacteria Red");
        foreach (Bacterium b in AllBacteria)
        {
            Transform img = page.Find("Bacteria " + b.key);
            if (img == null)
            {
                img = Clone(template, page, "Bacteria " + b.key);
                SetController(img.gameObject, b.uiController);
                img.GetComponent<Image>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Art/Bacteria/New/" + b.firstFrame + "/frame_00.png");
            }
        }

        // caption: tighter, the grid is wider now
        Transform cap = page.Find("UIK Caption");
        if (cap != null) { ((RectTransform)cap).sizeDelta = new Vector2(250f, 14f); ((RectTransform)cap).anchoredPosition = new Vector2(-129f, 100f); }
        Transform leg = page.Find("UIK Legend");
        if (leg != null) ((RectTransform)leg).anchoredPosition = new Vector2(-174f, -176f);

        const float cellW = 47f, cellH = 74f, stepX = 52.75f, stepY = 80f;
        float[] colX = { -222.5f, -222.5f + stepX, -222.5f + 2 * stepX, -222.5f + 3 * stepX, -222.5f + 4 * stepX };
        float[] rowY = { 51f, 51f - stepY, 51f - 2 * stepY };
        int plateIndex = 2;
        for (int k = 0; k < AllBacteria.Length; k++)
        {
            Bacterium b = AllBacteria[k];
            Vector2 center = new Vector2(colX[k % 5], rowY[k / 5]);
            RectTransform card = MicroNexusUIKit.Child(page, "Card " + b.key);
            card.anchorMin = card.anchorMax = new Vector2(0.5f, 0.5f); card.pivot = new Vector2(0.5f, 0.5f);
            card.sizeDelta = new Vector2(cellW, cellH); card.anchoredPosition = center;
            MicroNexusUIKit.Img(card, MicroNexusUIKit.Plate, new Color(0.07f, 0.09f, 0.21f, 0.9f), 16f / 10f);
            RectTransform ring = MicroNexusUIKit.Child(card, "Ring");
            MicroNexusUIKit.Stretch(ring, 0, 0, 0, 0);
            MicroNexusUIKit.Img(ring, MicroNexusUIKit.Ring, b.tone, 16f / 10f);
            for (int s = 0; s < 5; s++)
            {
                RectTransform sr = MicroNexusUIKit.Child(card, "Star");
                sr.anchorMin = sr.anchorMax = new Vector2(0.5f, 0.5f);
                sr.sizeDelta = new Vector2(7f, 7f); sr.anchoredPosition = new Vector2((s - 2) * 8.2f, -28f);
                MicroNexusUIKit.Img(sr, starSprite, s < stars[k] ? MicroNexusUIKit.Amber : new Color(1f, 1f, 1f, 0.16f), 1f, false);
            }
            card.SetSiblingIndex(plateIndex);
            Place((RectTransform)Need(page, "Bacteria " + b.key), center + new Vector2(0f, 12f), 40f, 24f);
        }
    }

    // ------------------------------------------------------------------ main entry
    public static string Run()
    {
        StringBuilder log = new StringBuilder();
        GameObject root = PrefabUtility.LoadPrefabContents(PrefabPath);
        try
        {
            Transform bc = Need(root.transform, "Bio Ranger Canvas");
            RangerBioManager rbm = root.GetComponentInChildren<RangerBioManager>(true);
            RangerCanvasManager rcm = root.GetComponentInChildren<RangerCanvasManager>(true);
            QuestionMarkManager qmm = root.GetComponentInChildren<QuestionMarkManager>(true);
            EnemyBacteriaBioManager ebm = root.GetComponentInChildren<EnemyBacteriaBioManager>(true);
            SerializedObject rbmSo = new SerializedObject(rbm);
            SerializedObject rcmSo = new SerializedObject(rcm);
            SerializedObject qmmSo = new SerializedObject(qmm);
            SerializedObject ebmSo = new SerializedObject(ebm);
            List<GameObject> extra = new List<GameObject>();

            // ---------- iterasi 1 pages: just the new grid
            for (int i = 0; i < OldKeys.Length; i++)
            {
                Transform page = Need(root.transform, "Ranger_" + OldKeys[i]);
                BuildEffectivenessGrid(page, OldTones[i], OldStars[i]);
            }
            log.AppendLine("iterasi 1 pages rebuilt with 14-bacteria grid");

            // ---------- iterasi 2: list cards, detail items, detail pages
            Transform content = Need(bc, "Scroll View/Viewport/Content");
            Transform cardTemplate = content.GetChild(0);
            Transform charImgTemplate = Need(bc, "Character Amoxicillin");
            Transform sideTemplate = Need(bc, "AmoxicillinSideButton");
            Transform detailTemplate = Need(root.transform, "Ranger_Amoxicillin");
            int insertAfter = sideTemplate.GetSiblingIndex();
            foreach (Ranger r in NewRangers)
            {
                Sprite icon = AssetDatabase.LoadAssetAtPath<Sprite>(r.icon);
                // list card
                Transform card = content.Find("card " + r.key);
                if (card == null) card = Clone(cardTemplate, content, "card " + r.key);
                Image cim = card.GetComponent<Image>();
                MicroNexusUIKit.MakePanel(cim, new Color(0.07f, 0.09f, 0.21f, 0.9f), r.tone, 10f, false);
                Transform cap = Need(card, "Capsule");
                Image capImg = cap.GetComponent<Image>(); capImg.sprite = icon; capImg.preserveAspect = true;
                float cw = 160f * MicroNexusUIKit.ScaleToCanvas((RectTransform)card);
                Place((RectTransform)cap, new Vector2(0f, cw * 0.06f), cw * 0.86f, cw * 0.86f);
                foreach (TextMeshProUGUI t in card.GetComponentsInChildren<TextMeshProUGUI>(true)) if (t.name.StartsWith("Text ")) t.gameObject.SetActive(false); // no price outside a level
                SetCall(card.GetComponent<Button>(), 0, "Show" + r.key);

                // big animated character + arrow in the list canvas
                Transform ch = bc.Find("Character " + r.key);
                if (ch == null) ch = Clone(charImgTemplate, bc, "Character " + r.key);
                SetController(ch.gameObject, r.key);
                Image chImg = ch.GetComponent<Image>(); chImg.sprite = icon; chImg.preserveAspect = true;
                ch.gameObject.SetActive(false);
                Transform side = bc.Find(r.key + "SideButton");
                if (side == null) side = Clone(sideTemplate, bc, r.key + "SideButton");
                SetCall(side.GetComponent<Button>(), 0, "Show" + r.key + "Canvas");
                side.gameObject.SetActive(false);
                ch.SetSiblingIndex(++insertAfter); side.SetSiblingIndex(++insertAfter);
                SetManagerData(rbmSo, r.key, ch.gameObject, side.gameObject, r.display, r.lore);

                // detail page
                Transform page = root.transform.Find("Ranger_" + r.key);
                if (page == null) page = Clone(detailTemplate, root.transform, "Ranger_" + r.key);
                page.gameObject.SetActive(false);
                Transform pch = Need(page, "Character Amoxicillin"); pch.name = "Character " + r.key;
                SetController(pch.gameObject, r.key);
                Image pimg = pch.GetComponent<Image>(); pimg.sprite = icon; pimg.preserveAspect = true;
                Transform pside = Need(page, "AmoxicillinSideButton"); pside.name = r.key + "SideButton";
                TextMeshProUGUI t1 = Need(page, "Text 1").GetComponent<TextMeshProUGUI>(); t1.text = r.display;
                TextMeshProUGUI body = null;
                foreach (TextMeshProUGUI t in Need(page, "Scroll Text").GetComponentsInChildren<TextMeshProUGUI>(true)) if (t.name == "Text (TMP)") body = t;
                body.text = r.profile;
                BuildEffectivenessGrid(page, r.tone, r.stars);
                // accent of plates/ring on the page
                foreach (string pn in new[] { "UIK Info Plate", "UIK Name Pill" })
                {
                    Transform pl = page.Find(pn + "/UIK Ring");
                    if (pl != null) pl.GetComponent<Image>().color = r.tone;
                }
                MicroNexusUIKit.StyleScrollText(Need(page, "Scroll Text"), r.tone);
                extra.Add(page.gameObject);
                rcmSo.FindProperty(char.ToLower(r.key[0]) + r.key.Substring(1) + "Canvas").objectReferenceValue = page.gameObject;
                log.AppendLine("ranger " + r.key + " added");
            }
            rbmSo.ApplyModifiedPropertiesWithoutUndo();
            rcmSo.ApplyModifiedPropertiesWithoutUndo();

            // ---------- iterasi 2 bacteria
            Transform general = Need(root.transform, "General Canvas");
            Transform listPanel = Need(general, "Bio Enemy Bacteria Panel");
            Transform bacContent = Need(listPanel, "Scroll View/Viewport/Content");
            Transform bacCardTemplate = bacContent.GetChild(0);
            Transform enemyImgTemplate = Need(listPanel, "Enemy Purple");
            Transform enemySideTemplate = Need(listPanel, "PurpleSideButton");
            Transform enemyDetailTemplate = Need(general, "Bio Enemy Purple");
            int bi = 0, bInsert = enemySideTemplate.GetSiblingIndex();
            foreach (NewBacteriumText nb in NewBacteriaText)
            {
                Bacterium bac = AllBacteria[7 + bi]; bi++;
                Sprite first = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Art/Bacteria/New/" + bac.firstFrame + "/frame_00.png");
                // list card
                Transform card = bacContent.Find("card " + bac.key);
                if (card == null) card = Clone(bacCardTemplate, bacContent, "card " + bac.key);
                MicroNexusUIKit.MakePanel(card.GetComponent<Image>(), new Color(0.07f, 0.09f, 0.21f, 0.9f), bac.tone, 10f, false);
                Image ci = Need(card, "Image").GetComponent<Image>(); ci.sprite = first; ci.preserveAspect = true;
                SetCall(card.GetComponent<Button>(), 0, "ShowBacteria" + bac.key);
                // animated picture + arrow in the list panel
                Transform pic = listPanel.Find("Enemy " + bac.key);
                if (pic == null) pic = Clone(enemyImgTemplate, listPanel, "Enemy " + bac.key);
                SetController(pic.gameObject, bac.uiController); pic.GetComponent<Image>().sprite = first;
                pic.gameObject.SetActive(false);
                Transform side = listPanel.Find(bac.key + "SideButton");
                if (side == null) side = Clone(enemySideTemplate, listPanel, bac.key + "SideButton");
                side.gameObject.SetActive(false);
                pic.SetSiblingIndex(++bInsert); side.SetSiblingIndex(++bInsert);
                // detail panel
                Transform detail = general.Find("Bio Enemy " + bac.key);
                if (detail == null) detail = Clone(enemyDetailTemplate, general, "Bio Enemy " + bac.key);
                detail.gameObject.SetActive(false);
                Transform dpic = Need(detail, "Enemy Purple"); dpic.name = "Enemy " + bac.key;
                SetController(dpic.gameObject, bac.uiController); dpic.GetComponent<Image>().sprite = first;
                Transform dside = Need(detail, "PurpleSideButton"); dside.name = bac.key + "SideButton";
                Need(detail, "Text 1").GetComponent<TextMeshProUGUI>().text = bac.display;
                Need(detail, "Text 2").GetComponent<TextMeshProUGUI>().text = nb.lore;
                TextMeshProUGUI body = null;
                foreach (TextMeshProUGUI t in Need(detail, "Scroll Text").GetComponentsInChildren<TextMeshProUGUI>(true)) if (t.name == "Text (TMP)") body = t;
                body.text = nb.detail;
                foreach (string pn in new[] { "UIK Info Plate" })
                {
                    Transform pl = detail.Find(pn + "/UIK Ring");
                    if (pl != null) pl.GetComponent<Image>().color = bac.tone;
                }
                MicroNexusUIKit.StyleScrollText(Need(detail, "Scroll Text"), bac.tone);
                extra.Add(detail.gameObject);
                // the arrow in the list opens the detail panel
                SetCall(side.GetComponent<Button>(), 0, "ShowEnemy", qmm, detail.gameObject);
                SetManagerData(ebmSo, "Bacteria" + bac.key, pic.gameObject, side.gameObject, bac.display, nb.lore);
                log.AppendLine("bacterium " + bac.key + " added");
            }
            ebmSo.ApplyModifiedPropertiesWithoutUndo();

            // everything new must be hidden by DisableAllContent
            SerializedProperty ex = qmmSo.FindProperty("extraContent");
            ex.arraySize = extra.Count;
            for (int i = 0; i < extra.Count; i++) ex.GetArrayElementAtIndex(i).objectReferenceValue = extra[i];
            qmmSo.ApplyModifiedPropertiesWithoutUndo();

            PrefabUtility.SaveAsPrefabAsset(root, PrefabPath);
            log.AppendLine("prefab saved; extraContent=" + extra.Count);
        }
        finally { PrefabUtility.UnloadPrefabContents(root); }
        return log.ToString();
    }
}
#endif
