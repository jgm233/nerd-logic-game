using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Text.RegularExpressions;

public abstract class BasicGate : MonoBehaviour
{
    [SerializeField] protected bool _a, _b, _out;
    protected bool _show_output_value = false;
    protected BasicGate _newGate;
    protected int _newGateCount = 1;
    [SerializeField] protected static BasicGate _output_gate = null;

    public virtual void Start()
    {
        // Debug.Log("In " + this.name + " start out =  " + _out);

        EvaluateGate();
    }

    public virtual string GateInputs()
    {
        return "a b";
    }
    
    public bool IsInputAvailable(string input_name)
    {
        BaseLevelController lc = FindObjectOfType<BaseLevelController>();
        string[] wire_list = lc.GetWireList();       
        if (wire_list.Length < 4) return true;
        // Debug.Log("in IsInputAvailable, name = " + this.name);
        for (int i = 0; i < wire_list.Length; i += 4) {
            // Debug.Log("in IsInputAvailable, wl[2] = " + wire_list[2] + ", wl[3] =" + wire_list[3]);
            if (wire_list[i + 2] == this.name && wire_list[i + 3] == input_name)
                return false;
        }
        return true;
    }
    // All placed gates must have a number at the end of the name
    public bool IsPlacedGate()
    {
        string pattern = @"\d$";
        Match m = Regex.Match(this.name, pattern);
        return (m.Success);
    }

    public virtual void OnMouseDown()
    {
        GameMode mygamemode = FindObjectOfType<GameMode>();
        if (mygamemode.IsInPlacementMode())
        {
            if (IsPlacedGate())
            {
                Destroy(gameObject);
                return;
            }
            _newGate = Instantiate(this);
            _newGate.name = this.name + _newGateCount.ToString();
            _newGate.transform.localScale = new Vector3(MyScale(),MyScale(),MyScale());
            _newGateCount++;       
        } else if (mygamemode.IsInPlayMode())
        {
            _show_output_value = !_show_output_value;
        } else // Must be in route
        {
            LineRenderer mylr = GetComponent<LineRenderer>();
            Vector3[] positions = new Vector3[2];
            positions[0] = this.transform.position;
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0;
            positions[1] = newPosition;
            // Debug.Log("In OnMouseDown for Route, this = " + this.name);
            mylr.startWidth = 0.2f;
            mylr.endWidth = 0.2f;
            mylr.widthMultiplier = 0.2f;
            mylr.startColor = Color.yellow;
            mylr.endColor = Color.yellow;
            mylr.material = AssetDatabase.GetBuiltinExtraResource<Material>("Default-Line.mat");
            mylr.positionCount = 2;
            mylr.SetPositions(positions);
            _output_gate = this;
        }

    }

    public void OnMouseEnter()
    {
        GameMode mygamemode = FindObjectOfType<GameMode>();
        if (mygamemode.IsInRouteMode() && _output_gate && _output_gate != this)
        {
            // Debug.Log("In OnMouseEnter, this = " + name);
            string[] myinputs = GateInputs().Split(' ');
            string found_input = "BadInput";
            BaseLevelController lc = FindObjectOfType<BaseLevelController>();
            lc.GetWireList();
            foreach (string myinput in myinputs)
            {
                if (IsInputAvailable(myinput)) {
                    // Debug.Log("In OnMouse Enter Found available input " + this.name + "/" + myinput);
                    found_input = myinput;
                    break;
                }
            }
            if (found_input == "BadInput")
            {
                Debug.Log("Non inputs available on " + this);
                return;
            }
            // BaseLevelController lc = FindObjectOfType<BaseLevelController>();
            string wire_element = _output_gate.name + " out " + this.name + " " + found_input;
            lc.AddToWireList(wire_element);
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0;
            LineRenderer mylr = _output_gate.GetComponent<LineRenderer>();
            int numPositions = mylr.positionCount;
            Vector3[] positions = new Vector3[numPositions + 1];
            mylr.GetPositions(positions);
            positions[numPositions-1] = transform.position; 
            positions[numPositions] = transform.position;
            mylr.positionCount += 1;
            mylr.SetPositions(positions);
            // Debug.Log("In OnMouseEnter, added this to wire list: " + wire_element);

        }
    }

    // Only really needed for Route mode
    public void OnMouseUp()
    {
        // Only really needed for Route mode
        _output_gate = null;
        // Only really need for InputSwitch
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void OnMouseDrag()
    {
        // No dragging in play mode, only in Placement and Route.
        GameMode mygamemode = FindObjectOfType<GameMode>();
        if (mygamemode.IsInPlacementMode())
        {
            Vector3 newGatePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newGatePosition.z = 0;
            // Debug.Log("new gate position = " + newGatePosition);
            _newGate.transform.position = newGatePosition;
        }
        else if (mygamemode.IsInRouteMode())
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0;
            LineRenderer mylr = GetComponent<LineRenderer>();
            int numPositions = mylr.positionCount;
            Vector3[] positions = new Vector3[numPositions];
            mylr.GetPositions(positions);
            positions[numPositions - 1] = newPosition;
            mylr.SetPositions(positions);
        }

    }


    public void Update()
    {
        GameMode mygamemode = FindObjectOfType<GameMode>();
        if (!mygamemode.IsInPlayMode()) return;
        LineRenderer _lr = GetComponent<LineRenderer>();
        if (!_lr) return;
        _lr.startWidth = 0.1f;
        _lr.endWidth = 0.1f;
        _lr.widthMultiplier = 0.1f;
        GlobalHelp _gh = FindObjectOfType<GlobalHelp>();
        if (_gh.IsGlobalHelpOn() || _show_output_value)
        {
            if (_out)
            {
                _lr.startColor = Color.green;
                _lr.endColor = Color.green;
            }
            else
            {
                _lr.startColor = Color.red;
                _lr.endColor = Color.red;
            }
        }
        else
        {
            _lr.startColor = Color.black;
            _lr.endColor = Color.black;
        }
    }

    public virtual float MyScale()
    {
        return 0.3f;
    }

    public virtual void EvaluateGate() { }

    public void PropagateOutput()
    {
        BaseLevelController _levelController = FindObjectOfType<BaseLevelController>();

        if (_levelController == null)
        {
            Debug.Log("didn't find LevelController");
        }
        else
        {
            string[] wire_list = _levelController.GetWireList();
            // Debug.Log("in propagate outputs for " + this.name);
            bool first_clock = true;
            for (int i = 0; i < wire_list.Length; i += 4)
            {
                if (wire_list[i] == this.name)
                {
                    // Debug.Log(" matched source name:  " + wire_list[i] + ", dest = " + wire_list[i+2]) ;
                    // On the first clock being driven, lock the FFs' inputs for the coming edge.
                    if (wire_list[i + 3] == "ck" && first_clock)
                    {
                        FF[] ffs = FindObjectsOfType<FF>();
                        foreach (FF myff in ffs)
                            myff.LockDInput();
                        first_clock = false;
                    }
                    GameObject _destination = GameObject.Find(wire_list[i + 2]);
                    if (_destination)
                    {
                        _destination.SendMessage("InputChanged_" + wire_list[i + 3],
                                             _out);
                    }
                }
                else
                {
                    // Debug.Log("not matched name:  " + wire_list[i]);
                }
            }
        }
    }


   // public virtual void InputChanged_ck(bool input_value) { }
   // public virtual void InputChanged_d(bool input_value) { }

    public virtual void InputChanged_a(bool input_value)
    {
        // Debug.Log("In " + this.name + " InputChanged_a, input_value = " + input_value);
        _a = input_value;
        EvaluateGate();
    }

    public void InputChanged_b(bool input_value)
    {
        // Debug.Log("In " + this.name + " InputChanged_b, input_value = " + input_value);
        _b = input_value;
        EvaluateGate();
    }

    public virtual float inputa_x_adjust() { return -0.95f; }
    public virtual float inputa_y_adjust() { return 0.35f; }
    public virtual float inputb_x_adjust() { return -0.95f; }
    public virtual float inputb_y_adjust() { return -0.35f; }
    public virtual float output_x_adjust() { return 0.95f; }
    public virtual float output_y_adjust() { return 0.0f; }
    public virtual float inputd_x_adjust() { return -0.95f; }
    public virtual float inputd_y_adjust() { return 0.5f; }
    public virtual float inputck_x_adjust() { return -0.95f; }
    public virtual float inputck_y_adjust() { return 0.5f; }

}


public class NandGate : BasicGate
{
    public override void EvaluateGate()
    {
        bool new_out = !(_a & _b);
        // Debug.Log("In " + this.name + " EvaluateGate new_out =  " + new_out);
        if (new_out == _out) return;
        _out = new_out;
        // Debug.Log("In " + this.name + " EvaluateGate out =  " + _out);
        PropagateOutput();
    }

}
