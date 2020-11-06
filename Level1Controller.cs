using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class BaseLevelController : MonoBehaviour
{
    // String describing login in each level
    // Format :  <Output gate> <output pin> <dest gate> <dest pin>"
    // One set of four values for each connection
    // The instantiations for each component is handled in the
    // scene manager (for now).  I might add code to instantiate each
    // component and draw the wires at a later time.
    protected string _logic_in_level = "";
    protected static int _level = 1;
    protected const int _maxLevel = 12;
    protected int _clock_period = 0;
    protected static int _totalCoins = 0;
    protected int _centiCoinsThisLevel = 10000;
    protected Text tc_text, cc_text, glitch_text;
    protected bool _scoreAdded = true;
    protected GameObject myGO;
    protected GameObject myText;
    protected Canvas myCanvas;
    protected RectTransform rectTransform;

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
        // Debug.Log("logic string for level " + _level + " = " + _logic_in_level);
        return (_logic_in_level.Split(' '));
    }

    public virtual void SetLogicString() {
        // Debug.Log("In base SetLogicString");
    }

    public void Awake()
    {
        // Debug.Log("In LC Awake()");
        SetLogicString();
    }

    // Start is called before the first frame update
    protected void Start()
    {

        
        // Debug.Log("in LC start, level = " + _level);
        _scoreAdded = false;
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

        AddText();
    }

    public virtual void AddText()
    {
        // Total coins Text
        myText = new GameObject();
        myText.transform.parent = myGO.transform;
        myText.name = "TotalCoins";
        // Debug.Log("mytext.transform = " + myText.transform);

        tc_text = myText.AddComponent<Text>();
        tc_text.font = (Font)Resources.Load("Anton");
        tc_text.text = "Total Coins:  " + _totalCoins;
        tc_text.fontSize = 30;
        // Debug.Log("tc_text.text = " + tc_text.text);

        // Total coins Text position
        rectTransform = tc_text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(19.3f, -8.7f, 0);
        rectTransform.sizeDelta = new Vector2(400, 200);
        rectTransform.localScale = new Vector3(0.02f, 0.02f, 0.02f);

        // Current Coins Text
        myText = new GameObject();
        myText.transform.parent = myGO.transform;
        myText.name = "CurrentCoins";
        // Debug.Log("mytext.transform = " + myText.transform);

        cc_text = myText.AddComponent<Text>();
        cc_text.font = (Font)Resources.Load("Anton");
        cc_text.text = "Current Coins:  " + ((int)((float)_centiCoinsThisLevel / 100.0));
        cc_text.fontSize = 30;
        // Debug.Log("text.text = " + cc_text.text);

        // Current Coins Text position
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

        // Glitch Text position
        rectTransform = glitch_text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(19.3f, -7.0f, 0);
        rectTransform.sizeDelta = new Vector2(400, 200);
        rectTransform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
    }


    public virtual void OnMouseDown()
    {
        _scoreAdded = true;
        _level++;
        // Debug.Log("LC mouse down _level == " + _level + " total coins = " + _totalCoins);
        string nextLevelName = "Level" + _level;
        SceneManager.LoadScene(nextLevelName);
        // Debug.Log("Loaded scene " + nextLevelName + " total coins = " + _totalCoins);
    }


    // Update is called once per frame
    public virtual void Update()
    {
        LightBulb _lb = FindObjectOfType<LightBulb>();
        if (!_lb) return;
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
        }
        else
        {
            if (_centiCoinsThisLevel > 0) _centiCoinsThisLevel--;
        }
        cc_text.text = "Current Coins:  " + (int)((float)_centiCoinsThisLevel / 100.0);
    }
}

public class Level1Controller : BaseLevelController
{
    private Text intro_text;

    public override void AddText () { 
        // Intro Text
        myText = new GameObject();
        myText.transform.parent = myGO.transform;
        myText.name = "IntroText";
        // Debug.Log("Intro mytext.transform = " + myText.transform);

        intro_text = myText.AddComponent<Text>();
        intro_text.font = (Font)Resources.Load("Anton");
        intro_text.text = "This is a digital logic game.  Move the switches on the left up and\ndown to activate the logic and change the state of the light bulb.\nSome levels generate glitches.  The coin counter runs until the\nlight bulb is switched or glitched, and then the current coins is\nadded to your total coins.  Earn coins to buy fabulous prizes\nand additions to the game.\n\nJohn\njgm23333@gmail.com";
        intro_text.fontSize = 36;

        // Text position
        rectTransform = intro_text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(8.0f, -6.8f, 0);
        rectTransform.sizeDelta = new Vector2(1000, 1000);
        rectTransform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
    }    

        // Update is called once per frame, don't do anything for intro screens
    public override void Update() { }

}
