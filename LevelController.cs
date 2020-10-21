using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelController : MonoBehaviour
{
    // String describing login in each level
    // Format :  <Output gate> <output pin> <dest gate> <dest pin>"
    // One set of four values for each connection
    // The instantiations for each component is handled in the
    // scene manager (for now).  I might add code to instantiate each
    // component and draw the wires at a later time.
    private string[] _logic_in_level =
    {"dummy string for index0",
         "InputSwitch1 switch_value AndGate1 a " +
            "InputSwitch2 switch_value AndGate1 b " +
            "AndGate1 _out LightBulb input",
         "InputSwitch1 _out AndGate1 a " +
            "InputSwitch2 _out AndGate1 b " +
            "InputSwitch3 _out NandGate1 b " +
            "AndGate1 _out NandGate1 a " +
            "NandGate1 _out LightBulb input",
         "InputSwitch1 _out Inverter1 a " +
            "Inverter1 _out AndGate1 a " +
            "InputSwitch2 _out AndGate1 b " +
            "InputSwitch3 _out NandGate1 b " +
            "AndGate1 _out NandGate1 a " +
            "NandGate1 _out LightBulb input",
         "InputSwitch1 _out AndGate1 a " +
            "InputSwitch1 _out Inverter1 a " +
            "Inverter1 _out AndGate1 b " +            
            "AndGate1 _out LightBulb input"
    };

  
    private static int _nextLevelIndex = 1;
    private const int _maxLevel = 4;
    private int _clock_period = 0;
    private static int _totalCoins = 0;
    private int _centiCoinsThisLevel = 10000;
    private Text tc_text, cc_text, glitch_text;
    private bool _scoreAdded = false;

    public void AdvanceClock()
    {
        _clock_period++;
    }

    public int GetClockPeriod()
    {
        return _clock_period;
    }

    public string[] GetThisLevelsComponents()
    {
        //Debug.Log("logic string for level " + _nextLevelIndex + " " + _logic_in_level[_nextLevelIndex]);
        return (_logic_in_level[_nextLevelIndex].Split(' '));
    }

    void Start()
    {
        GameObject myGO;
        GameObject myText;
        Canvas myCanvas;

        RectTransform rectTransform;

        // Canvas
        myGO = new GameObject();
        myGO.name = "ScoreCanvas";
        myGO.AddComponent<Canvas>();

        myCanvas = myGO.GetComponent<Canvas>();
        // myCanvas.renderMode = RenderMode.ScreenSpaceOverlay; 
        myCanvas.renderMode = RenderMode.WorldSpace;
        myGO.AddComponent<CanvasScaler>();
        myGO.AddComponent<GraphicRaycaster>();
        // Debug.Log("myGO.transform = " + myGO.transform);

        // Text
        myText = new GameObject();
        myText.transform.parent = myGO.transform;
        myText.name = "TotalCoins";
        // Debug.Log("mytext.transform = " + myText.transform);

        tc_text = myText.AddComponent<Text>();
        tc_text.font = (Font)Resources.Load("Anton");
        tc_text.text = "Total Coins:  " + _totalCoins;
        tc_text.fontSize = 30;
        // Debug.Log("tc_text.text = " + tc_text.text);

        // Text position
        rectTransform = tc_text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(19.3f, -8.7f, 0);
        rectTransform.sizeDelta = new Vector2(400, 200);
        rectTransform.localScale = new Vector3(0.02f, 0.02f, 0.02f);

        // Text
        myText = new GameObject();
        myText.transform.parent = myGO.transform;
        myText.name = "CurrentCoins";
        // Debug.Log("mytext.transform = " + myText.transform);

        cc_text = myText.AddComponent<Text>();
        cc_text.font = (Font)Resources.Load("Anton");
        cc_text.text = "Current Coins:  " + ((int)((float)_centiCoinsThisLevel / 100.0));
        cc_text.fontSize = 30;
        // Debug.Log("text.text = " + cc_text.text);

        // Text position
        rectTransform = cc_text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(19.3f, -9.4f, 0);
        rectTransform.sizeDelta = new Vector2(400, 200);
        rectTransform.localScale = new Vector3(0.02f, 0.02f, 0.02f);

        // Glitch Text
        myText = new GameObject();
        myText.transform.parent = myGO.transform;
        myText.name = "GlitchAdvisement";
        // Debug.Log("mytext.transform = " + myText.transform);

        glitch_text = myText.AddComponent<Text>();
        glitch_text.font = (Font)Resources.Load("Anton");
        glitch_text.text = "Glitch Detected";
        glitch_text.fontSize = 30;
        glitch_text.color = Color.gray;

        // Text position
        rectTransform = glitch_text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(19.3f, -7.0f, 0);
        rectTransform.sizeDelta = new Vector2(400, 200);
        rectTransform.localScale = new Vector3(0.02f, 0.02f, 0.02f);

    }

    public void OnMouseDown()
    {
        if (_nextLevelIndex >= _maxLevel) return;
        _nextLevelIndex++;
        string nextLevelName = "Level" + _nextLevelIndex;
        SceneManager.LoadScene(nextLevelName);
        Debug.Log("Loaded scene " + nextLevelName);
        if (_nextLevelIndex == _maxLevel)
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        // Debug.Log("_logic_in_level["+ _nextLevelIndex + "] = "+ _logic_in_level[_nextLevelIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        LightBulb _lb = FindObjectOfType<LightBulb>();
        if (_lb.Switched() || _lb.Glitched())
        {
            // Debug.Log(" in update LC, switched = " + _lb.Switched() + " glitched = " + _lb.Glitched());

            if (!_scoreAdded)
            {
                _totalCoins += (int)((float)_centiCoinsThisLevel / 100.0);
                tc_text.text = "Total Coins:  " + _totalCoins;
                _scoreAdded = true;  
                if (_lb.Glitched())
                {
                    glitch_text.color = Color.white;
                }
            }
        } else
        {
            if (_centiCoinsThisLevel > 0) _centiCoinsThisLevel--;
        }
        cc_text.text = "Current Coins:  " + (int)((float)_centiCoinsThisLevel / 100.0);
    }


}
