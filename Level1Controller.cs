using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public abstract class BaseLevelController : MonoBehaviour
{
    // String describing login in each level
    // Format :  <Output gate> <output pin> <dest gate> <dest pin>"
    // One set of four values for each connection
    // The instantiations for each component is handled in the
    // scene manager (for now).  I might add code to instantiate each
    // component and draw the wires at a later time.
    [SerializeField] protected string  _wire_list = "";
    [SerializeField] protected string _parts_list = "";
    [SerializeField] protected static int _level = 1;
    protected const int _maxLevel = 14;
    [SerializeField] protected int _clock_period = 0;
    protected static int _totalCoins = 0;
    protected int _centiCoinsThisLevel = 10000;
    protected Text tc_text, cc_text, glitch_text;
    protected bool _scoreAdded = true;
    protected GameObject myGO;
    protected GameObject myText;
    protected Canvas myCanvas;
    protected RectTransform rectTransform;

    public int GetLevel() { return _level; }

    public void AdvanceClock()
    {
        _clock_period++;
    }

    public int GetClockPeriod()
    {
        return _clock_period;
    }

    public string[] GetWireList()
    {
        // Debug.Log("wire list for level " + _level + " = '" +  _wire_list + "'");
        return ( _wire_list.Split(' '));
    }

    public virtual void SetLogicString() {
        // Debug.Log("In base SetLogicString");
    }

    // Awake is called as soon as the scripts are loaded
    public void Awake()
    {
        // Debug.Log("In LC Awake()");
        GameMode mygamemode = FindObjectOfType<GameMode>();
        mygamemode.SetPlayModeOnly();
        SetLogicString();
    }

    public void SetPartsList(string new_parts_list)
    {
        _parts_list = new_parts_list;
    }

    public void AddToWireList(string wire_element)
    {
        _wire_list = _wire_list + " " + wire_element;
        _wire_list = _wire_list.Trim(' ');
        // Debug.Log(" in AddToWireList = " + wire_element);
    }

    public void InstantiatePartsList()
    {
        // if (_parts_list == "") return;
        string[] parts = _parts_list.Split(' ');
        // Debug.Log("in instantiate parts list " + this.name + ", part list length = " + parts.Length + "parts list = " + _parts_list);
        // 5 fields in parts list
        // InstanceName SpriteName x y scale
        for (int i = 0; i < parts.Length; i += 5)
        {
            GameObject partsGO = new GameObject();
            partsGO.name = parts[i];
            BasicGate mycomp;
            // Debug.Log("in instantiate parts[" + i + "] = " + parts[i]);

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
                case "Inverter":
                    partsGO.AddComponent<Inverter>();
                    mycomp = partsGO.GetComponent<Inverter>();
                    break;
                case "FF":
                    partsGO.AddComponent<FF>();
                    mycomp = partsGO.GetComponent<FF>();
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

    public void WireCircuit()
    {
        string[] wire_list = GetWireList();
        if (wire_list.Length < 4) return;
        // Debug.Log("in wire circuit, length = " + wire_list.Length);
        // Debug.Log("in wire circuit, _wire_list = '" + _wire_list + "'");
        // Debug.Log("in wire circuit, wire_list[] = '" + wire_list[0] + "', '" +
        //                                           wire_list[1] + "', '" +
        //                                           wire_list[2] + "', '" +
        //                                           wire_list[3] + "'");

        for (int i = 0; i < wire_list.Length; i += 4)
        {
            // Debug.Log("in wire circuit source = " + wire_list[i] + " dest = " + wire_list[i+2]);
            GameObject mysource = GameObject.Find(wire_list[i]);
            GameObject mydestination = GameObject.Find(wire_list[i + 2]);
            BasicGate mysource_gate = mysource.GetComponent(typeof(BasicGate)) as BasicGate;
            BasicGate mydestination_gate = mydestination.GetComponent(typeof(BasicGate)) as BasicGate;
            // Debug.Log("in wire circuit mysoure_gate = " + mysource_gate.name);
            // Debug.Log("       dest = " + mydestination.name);
            // Debug.Log("       dest gate = " + mydestination_gate.name);

            float startx_adjust = mysource_gate.output_x_adjust();
            float starty_adjust = mysource_gate.output_y_adjust();

            float startx = mysource.transform.position.x + startx_adjust;
            float starty = mysource.transform.position.y + starty_adjust;
            float endx_adjust = -1;
            float endy_adjust = 0;
            switch (wire_list[i + 3])
            {
                case "a":
                    endx_adjust = mydestination_gate.inputa_x_adjust();
                    endy_adjust = mydestination_gate.inputa_y_adjust();
                    //Debug.Log("Destination " + wire_list[i + 2] + "/" + wire_list[i + 3]
                    //    + " endx adjust = " + endx_adjust
                    //     + " endy adjust = " + endy_adjust);
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
            float middlex = (float)(startx + endx) / 2.0f;

            Vector3[] positions;
            LineRenderer mylr = mysource.GetComponent<LineRenderer>();
            if (!mylr || mylr.positionCount < 2)
            {
                // Create a new route from the source to the destination
                // Debug.Log("    First segment destination = " + mydestination_gate.name);
                if (!mylr) mysource.AddComponent<LineRenderer>();
                mylr = mysource.GetComponent<LineRenderer>();
                positions = new Vector3[4];
                positions[0] = new Vector3(startx, starty, 0);
                positions[1] = new Vector3(middlex, starty, 0);
                positions[2] = new Vector3(middlex, endy, 0);
                positions[3] = new Vector3(endx, endy, 0);
                mylr.positionCount = 4;
            }
            else
            {
                // Adding onto an existing route by backtracing one segment, then adding two more 
                // segments to the line
                // Debug.Log("    Next segment destination = " + mydestination_gate.name);
                // Debug.Log("     old_position_count = " + mylr.positionCount);
                int old_position_count = mylr.positionCount;
                positions = new Vector3[old_position_count + 3];
                mylr.GetPositions(positions);
                positions[old_position_count] = positions[old_position_count - 2];
                positions[old_position_count + 1] = new Vector3(positions[old_position_count - 2].x, endy, 0);
                positions[old_position_count + 2] = new Vector3(endx, endy, 0);
                mylr.positionCount = old_position_count + 3;
            }
            mylr.SetPositions(positions);
            // mylr.material = AssetDatabase.GetBuiltinExtraResource<Material>("Default-Line.mat");
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

    protected string _hint_string;
    public string GetHintString()
    {
        return _hint_string;
    }

    public void SetHintString(string input_string)
    {
        _hint_string = input_string;
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
        GameMode mygamemode = FindObjectOfType<GameMode>();
        if (!mygamemode.IsInPlayMode()) return;
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
