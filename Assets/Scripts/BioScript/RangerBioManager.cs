using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using TMPro;

public class RangerBioManager : MonoBehaviour
{
    [System.Serializable]
    public class CharacterData
    {
        public GameObject pictureObj;
        public GameObject button;

        public string name;
        [TextArea(3, 6)] public string description;
    }

    [Header("Display Slot (UI Output)")]
    [SerializeField] private TMP_Text slotName;
    [SerializeField] private TMP_Text slotDescription;

    [Header("Characters Data (Fill in Inspector)")]
    [FormerlySerializedAs("White")] [SerializeField] private CharacterData Amoxicillin;
    [FormerlySerializedAs("Blue")] [SerializeField] private CharacterData Vancomisin;
    [FormerlySerializedAs("Pink")] [SerializeField] private CharacterData Cefixime;
    [FormerlySerializedAs("Red")] [SerializeField] private CharacterData Azitromisin;
    [FormerlySerializedAs("Green")] [SerializeField] private CharacterData Ciprofloxacin;
    [FormerlySerializedAs("Yellow")] [SerializeField] private CharacterData Levofloksasin;
    [FormerlySerializedAs("Black")] [SerializeField] private CharacterData Cotrimoxazole;

    private CharacterData[] allCharacters;

    private void Start()
    {
        allCharacters = new CharacterData[]
        {
            Amoxicillin,
            Vancomisin,
            Cefixime,
            Azitromisin,
            Ciprofloxacin,
            Levofloksasin,
            Cotrimoxazole
        };

        ShowAmoxicillin();
    }

    private void ApplyCharacter(CharacterData data)
    {
        if (data == null) return;

        // matikan semua picture + button
        foreach (var c in allCharacters)
        {
            if (c != null)
            {
                if (c.pictureObj != null) c.pictureObj.SetActive(false);
                if (c.button != null) c.button.SetActive(false);
            }
        }

        // aktifkan yang dipilih
        if (data.pictureObj != null) data.pictureObj.SetActive(true);
        if (data.button != null) data.button.SetActive(true);

        // ubah text
        if (slotName != null) slotName.text = data.name;
        if (slotDescription != null) slotDescription.text = data.description;
    }

    // ===== BUTTON CALLS =====

    public void ShowAmoxicillin() => ApplyCharacter(Amoxicillin);
    public void ShowVancomisin() => ApplyCharacter(Vancomisin);
    public void ShowCefixime() => ApplyCharacter(Cefixime);
    public void ShowAzitromisin() => ApplyCharacter(Azitromisin);
    public void ShowCiprofloxacin() => ApplyCharacter(Ciprofloxacin);
    public void ShowLevofloksasin() => ApplyCharacter(Levofloksasin);
    public void ShowCotrimoxazole() => ApplyCharacter(Cotrimoxazole);
}
