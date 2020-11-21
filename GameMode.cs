using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMode : MonoBehaviour
{
    private string _saved_hint_string; // Start is called before the first frame update
    enum MyGameMode { Play, Placement, Route };

    [SerializeField] MyGameMode _gameMode = MyGameMode.Play;

    public bool IsInPlacementMode()
    {
        return (_gameMode == MyGameMode.Placement);
    }

    public bool IsInRouteMode()
    {
        return (_gameMode == MyGameMode.Route);
    }

    public bool IsInPlayMode()
    {
        return (_gameMode == MyGameMode.Play);
    }

    public void SetPlacementMode()
    {

        BaseLevelController lc = FindObjectOfType<BaseLevelController>();
        lc.SetHintString("Make your own puzzle!  Drag and drop components to make a circuit, click again to delete, must place at least a switch and a light bulb");
            
        _gameMode = MyGameMode.Placement;
        SpriteRenderer mysr = GetComponent<SpriteRenderer>();
        if (mysr)
        {
            Sprite newsprite = Resources.Load<Sprite>("Route button");
            mysr.sprite = newsprite;
            mysr.color = Color.gray;
        }
    }

    public void SetRouteMode()
    {
        BaseLevelController lc = FindObjectOfType<BaseLevelController>();
        lc.SetHintString("Click and drag from outputs to inputs, click again to delete");
        _gameMode = MyGameMode.Route;
        string parts_list = "";
        BasicGate[] all_gates = FindObjectsOfType<BasicGate>();
        foreach (BasicGate mygate in all_gates) {
            if (!mygate.IsPlacedGate())
            {
                Destroy(mygate.gameObject);
            } else
            {
                parts_list = parts_list +
                    mygate.name + " DummySprite " +
                    mygate.transform.position.x + " " +
                    mygate.transform.position.y + " 0.5 ";
            }
        }
        parts_list.Trim(' ');
        lc.SetPartsList(parts_list);
        // Debug.Log(" in set route mode, parts_list = '" + parts_list + "'");
        SpriteRenderer mysr = GetComponent<SpriteRenderer>();
        if (mysr)
        {
            Sprite newsprite = Resources.Load<Sprite>("Play button");
            mysr.sprite = newsprite;
        }
     }

    // Called at the beginning of the level
    public void SetPlayModeOnly()
    {
        _gameMode = MyGameMode.Play;
    }

    // Called if transitioning from Route to Play
    public void SetPlayMode()
        {
        // Debug.Log("In SetPlayMode()");
        BasicGate[] all_gates = FindObjectsOfType<BasicGate>();
        // Clear out the fly line from Route Mode
        foreach (BasicGate mygate in all_gates)
        {
            LineRenderer mylr = mygate.GetComponent<LineRenderer>();
            if (mylr) mylr.positionCount = 0;
        }

        BaseLevelController lc = FindObjectOfType<BaseLevelController>();
        lc.SetHintString("Solve your own puzzle!");
        lc.WireCircuit();
        _gameMode = MyGameMode.Play;
        foreach (BasicGate mygate in all_gates)
        {
            mygate.PropagateOutput();
        }

        SpriteRenderer mysr = GetComponent<SpriteRenderer>();
        if (mysr)
        {
            Sprite newsprite = Resources.Load<Sprite>("Reset button");
            mysr.sprite = newsprite;
        }
    }

    public void OnMouseDown()
    {
        GlobalHelp mygh = FindObjectOfType<GlobalHelp>();
        mygh.SetGlobalHelpOff();
        switch (_gameMode) {
            case MyGameMode.Play:
                // Reset game by reloading
                BaseLevelController _lc = FindObjectOfType<BaseLevelController>();
                int level = _lc.GetLevel();
                string levelName = "Level" + level;
                SceneManager.LoadScene(levelName);
                break;
            case MyGameMode.Placement:
                LightBulb[] light_bulbs = FindObjectsOfType<LightBulb>();
                InputSwitch[] input_switches = FindObjectsOfType<InputSwitch>();
                if (input_switches.Length > 1 && light_bulbs.Length > 1)
                    SetRouteMode();
                break;
            case MyGameMode.Route:
                SetPlayMode();
                break;
        }
    }

    public void Update()
    {
        if (_gameMode == MyGameMode.Placement)
        {
            LightBulb[] light_bulbs = FindObjectsOfType<LightBulb>();
            InputSwitch[] input_switches = FindObjectsOfType<InputSwitch>();
            if (input_switches.Length > 1 && light_bulbs.Length > 1)
            {
                SpriteRenderer mysr = GetComponent<SpriteRenderer>();
                mysr.color = Color.white;
            }

        }
    }
}
