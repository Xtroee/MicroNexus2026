using UnityEngine;
using UnityEngine.Serialization;

public class RangerCanvasManager : MonoBehaviour
{
    [Header("All Canvases")]
    [SerializeField] private GameObject defaultCanvas;
    [FormerlySerializedAs("whiteCanvas")] [SerializeField] private GameObject amoxicillinCanvas;
    [FormerlySerializedAs("blueCanvas")] [SerializeField] private GameObject vancomisinCanvas;
    [FormerlySerializedAs("pinkCanvas")] [SerializeField] private GameObject cefiximeCanvas;
    [FormerlySerializedAs("redCanvas")] [SerializeField] private GameObject azitromisinCanvas;
    [FormerlySerializedAs("greenCanvas")] [SerializeField] private GameObject ciprofloxacinCanvas;
    [FormerlySerializedAs("yellowCanvas")] [SerializeField] private GameObject levofloksasinCanvas;
    [FormerlySerializedAs("blackCanvas")] [SerializeField] private GameObject cotrimoxazoleCanvas;

    [Header("Canvases - iterasi 2")]
    [SerializeField] private GameObject ceftriaCanvas;
    [SerializeField] private GameObject clarithCanvas;
    [SerializeField] private GameObject colistinCanvas;
    [SerializeField] private GameObject doxyCanvas;
    [SerializeField] private GameObject linezoCanvas;
    [SerializeField] private GameObject meropeCanvas;
    [SerializeField] private GameObject penicilinCanvas;

    [Header("Settings")]
    [SerializeField] private bool showDefaultOnStart = true;

    private GameObject[] allCanvases;

    private void Start()
    {
        allCanvases = new GameObject[]
        {
            defaultCanvas,
            amoxicillinCanvas,
            vancomisinCanvas,
            cefiximeCanvas,
            azitromisinCanvas,
            ciprofloxacinCanvas,
            levofloksasinCanvas,
            cotrimoxazoleCanvas,
            ceftriaCanvas,
            clarithCanvas,
            colistinCanvas,
            doxyCanvas,
            linezoCanvas,
            meropeCanvas,
            penicilinCanvas
        };

        if (showDefaultOnStart)
        {
            ShowDefaultCanvas();
        }
    }

    private void ShowOnly(GameObject canvasToShow)
    {
        foreach (var canvas in allCanvases)
        {
            if (canvas != null)
                canvas.SetActive(canvas == canvasToShow);
        }
    }

    public void ShowDefaultCanvas()        => ShowOnly(defaultCanvas);
    public void ShowAmoxicillinCanvas()    => ShowOnly(amoxicillinCanvas);
    public void ShowVancomisinCanvas()     => ShowOnly(vancomisinCanvas);
    public void ShowCefiximeCanvas()       => ShowOnly(cefiximeCanvas);
    public void ShowAzitromisinCanvas()    => ShowOnly(azitromisinCanvas);
    public void ShowCiprofloxacinCanvas()  => ShowOnly(ciprofloxacinCanvas);
    public void ShowLevofloksasinCanvas()  => ShowOnly(levofloksasinCanvas);
    public void ShowCotrimoxazoleCanvas()  => ShowOnly(cotrimoxazoleCanvas);
    public void ShowCeftriaCanvas() => ShowOnly(ceftriaCanvas);
    public void ShowClarithCanvas() => ShowOnly(clarithCanvas);
    public void ShowColistinCanvas() => ShowOnly(colistinCanvas);
    public void ShowDoxyCanvas() => ShowOnly(doxyCanvas);
    public void ShowLinezoCanvas() => ShowOnly(linezoCanvas);
    public void ShowMeropeCanvas() => ShowOnly(meropeCanvas);
    public void ShowPenicilinCanvas() => ShowOnly(penicilinCanvas);
}