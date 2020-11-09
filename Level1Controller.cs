using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections.Specialized;
using System.Reflection.Emit;
using System;

public abstract class BaseLevelController : MonoBehaviour
{
    // String describing login in each level
    // Format :  <Output gate> <output pin> <dest gate> <dest pin>"
    // One set of four values for each connection
    // The instantiations for each component is handled in the
    // scene manager (for now).  I might add code to instantiate each
    // component and draw the wires at a later time.
    protected string _logic_in_level = "";
    protected string _parts_list = "";
    protected static int _level = 1;
    protected const int _maxLevel = 13;
    [SerializeField] protected int _clock_period = 0;
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

    // Awake is called as soon as the scripts are loaded
    public void Awake()
    {
        // Debug.Log("In LC Awake()");
        SetLogicString();
    }

    protected InputSwitch InputSwitch1;

    public void InstantiatePartsList()
    {
        // if (_parts_list == "") return;
        string[] parts = _parts_list.Split(' ');
        Debug.Log("in instantiate parts list " + this.name + ", part list length = " + parts.Length + "parts list = " + _parts_list);
        // 5 fields in parts list
        // InstanceName SpriteName x y scale
        for (int i = 0; i < parts.Length; i += 5)
        {
            GameObject partsGO = new GameObject();
            partsGO.name = parts[i];
            BasicGate mycomp;
            Debug.Log("in instantiate parts[" + i + "] = " + parts[i]);

            switch (parts[i + 1])
            {
                case "SwitchDown":
                    partsGO.AddComponent<InputSwitch>();
                    mycomp = partsGO.GetComponent<InputSwitch>();
                    break;
                case "NandGate":
                    partsGO.AddComponent<NandGate>();
                    mycomp = partsGO.GetComponent<NandGate>();
                    break;
                case "AndGate":
                    partsGO.AddComponent<AndGate>();
                    mycomp = partsGO.GetComponent<AndGate>();
                    break;
                case "NorGate":
                    partsGO.AddComponent<NorGate>();
                    mycomp = partsGO.GetComponent<NorGate>();
                    break;
                case "OrGate":
                    partsGO.AddComponent<OrGate>();
                    mycomp = partsGO.GetComponent<OrGate>();
                    break;
                case "LightBulbOff":
                    partsGO.AddComponent<LightBulb>();
                    mycomp = partsGO.GetComponent<LightBulb>();
                    break;
                default:
                    Debug.Log("Bad component type:  " + parts[i + 1]);
                    break;
            }
            partsGO.transform.Translate(float.Parse(parts[i + 2]), float.Parse(parts[i + 3]), 0);
            float myscale = float.Parse(parts[i + 4]);
            partsGO.transform.localScale = new Vector3(myscale,myscale,myscale);
            partsGO.AddComponent<SpriteRenderer>();
            Sprite mysprite = Resources.Load<Sprite>(parts[i+1]);
            partsGO.GetComponent<SpriteRenderer>().sprite = mysprite;
            partsGO.AddComponent<BoxCollider2D>();
        }
    }

    protected void WireCircuit()
    {
        string[] logic_components = GetThisLevelsComponents();
        for (int i = 0; i < logic_components.Length; i += 4)
        {
            Debug.Log("in wire circuit source = " + logic_components[i] + " dest = " + logic_components[i+2]);
            GameObject mysource = GameObject.Find(logic_components[i]);
            GameObject mydestination = GameObject.Find(logic_components[i + 2]);
            BasicGate mysource_gate = mysource.GetComponent(typeof(BasicGate)) as BasicGate;
            BasicGate mydestination_gate = mydestination.GetComponent(typeof(BasicGate)) as BasicGate;
            Debug.Log("in wire circuit mysoure_gate = " + mysource_gate.name);
            Debug.Log("       dest = " + mydestination.name);
            Debug.Log("       dest gate = " + mydestination_gate.name);

            mysource.AddComponent<LineRenderer>();
            LineRenderer mylr = mysource.GetComponent<LineRenderer>();
            mylr.material = AssetDatabase.GetBuiltinExtraResource<Material>("Default-Line.mat");
            mylr.positionCount = 4;
            float startx_adjust = 1.0f;
            float starty_adjust = 0.0f;
            startx_adjust = mysource_gate.output_x_adjust();
            starty_adjust = mysource_gate.output_y_adjust();

            float startx = mysource.transform.position.x + startx_adjust;
            float starty = mysource.transform.position.y + starty_adjust;
            float endx_adjust = -1;
            float endy_adjust = 0;
            switch (logic_components[i + 3])
            {
                case "a":
                    endx_adjust = mydestination_gate.inputa_x_adjust();
                    endy_adjust = mydestination_gate.inputa_y_adjust();
                    break;
                case "b":
                    endx_adjust = mydestination_gate.inputb_x_adjust();
                    endy_adjust = mydestination_gate.inputb_y_adjust();
                    break;
                case "d":
                    endx_adjust = mydestination_gate.inputd_x_adjust();
                    endy_adjust = mydestination_gate.inputd_y_adjust();
                    break;
                case "ck":
                    endx_adjust = mydestination_gate.inputck_x_adjust();
                    endy_adjust = mydestination_gate.inputck_y_adjust();
                    break;
            }
            float endx = mydestination.transform.position.x + endx_adjust;
            float endy = mydestination.transform.position.y + endy_adjust;

            Vector3[] positions = new Vector3[4];
            float middlex = (float)(startx + endx) / 2.0f;
            positions[0] = new Vector3(startx, starty,0);
            positions[1] = new Vector3(middlex, starty, 0);
            positions[2] = new Vector3(middlex, endy, 0);
            positions[3] = new Vector3(endx, endy,0);
            mylr.SetPositions(positions);
            
        }
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
            cc_text.color = Color.gray;
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
