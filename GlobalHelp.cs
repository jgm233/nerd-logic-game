using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class GlobalHelp : MonoBehaviour
{
    private bool _global_help_on = false;
    Text _hint_text;

    public void Awake()
    {
        GameObject myGO = new GameObject();
        myGO.name = "HintCanvas";
        myGO.AddComponent<Canvas>();

        Canvas myCanvas = myGO.GetComponent<Canvas>();
        // myCanvas.renderMode = RenderMode.ScreenSpaceOverlay; 
        myCanvas.renderMode = RenderMode.WorldSpace;
        myGO.AddComponent<CanvasScaler>();
        myGO.AddComponent<GraphicRaycaster>();

        GameObject hintGO = new GameObject();
        hintGO.transform.parent = myGO.transform;
        hintGO.name = "Hint String";
        // Debug.Log("hintText.transform = " + hintText.transform);
        // Hint Text Canvas

        _hint_text = hintGO.AddComponent<Text>();
        _hint_text.font = (Font)Resources.Load("Anton");
        _hint_text.text = "";
        _hint_text.fontSize = 30;
        // Debug.Log("hint_Text.text = " + hint_Text.text);

        RectTransform rectTransform = _hint_text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(10.0f, 2.0f, 0);
        rectTransform.sizeDelta = new Vector2(900, 200);
        rectTransform.localScale = new Vector3(0.02f, 0.02f, 0.02f);

    }
    public void OnMouseDown()
    {
        _global_help_on = !_global_help_on;
        if (_global_help_on)
        {
            BaseLevelController _lc = FindObjectOfType<BaseLevelController>();
            string hint_string = _lc.GetHintString();
            _hint_text.text = "Hint: " + hint_string;
        } else
        {
            _hint_text.text = "";
        }
        // Debug.Log("In " + this.name + " global help is now on: " + _global_help_on);
    }

    // Update is called once per frame
    public bool IsGlobalHelpOn()
    {
        return _global_help_on;
    }

    public void SetGlobalHelpOff()
    {
        _hint_text.text = ""; 
        _global_help_on = false;
    }
}
