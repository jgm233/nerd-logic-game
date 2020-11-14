using System.IO.Compression;
using System.Security.Permissions;
using UnityEngine;
using System.Text.RegularExpressions;

public abstract class BasicGate : MonoBehaviour
{
    [SerializeField] protected bool _a, _b, _out;
    protected bool _show_output_value = false;
    protected BasicGate _newGate;
    protected int _newGateCount = 1;

    public virtual void Start()
    {
        // Debug.Log("In " + this.name + " start out =  " + _out);

        EvaluateGate();
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
            _newGateCount++;       
        } else if (mygamemode.IsInRouteMode())
        {

        } else  // Must be in Play mode
        {
            _show_output_value = !_show_output_value;
        }

    }


    public void OnMouseDrag()
    {
        // No dragging in play mode, only in Placement and route.
        GameMode mygamemode = FindObjectOfType<GameMode>();
        if (mygamemode.IsInPlayMode()) return;

        Vector3 newGatePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newGatePosition.z = 0;
        // Debug.Log("new gate position = " + newGatePosition);
        _newGate.transform.position = newGatePosition;
    }


    public void Update()
    {
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

    public virtual void EvaluateGate() { }

    protected void PropagateOutput()
    {
        BaseLevelController _levelController = FindObjectOfType<BaseLevelController>();

        if (_levelController == null)
        {
            Debug.Log("didn't find LevelController");
        }
        else
        {
            string[] _logic_components = _levelController.GetThisLevelsComponents();
            Debug.Log("in propagate outputs for " + this.name);
            bool first_clock = true;
            for (int i = 0; i < _logic_components.Length; i += 4)
            {
                if (_logic_components[i] == this.name)
                {
                    Debug.Log(" matched source name:  " + _logic_components[i] + ", dest = " + _logic_components[i+2]) ;
                    // On the first clock being driven, lock the FFs' inputs for the coming edge.
                    if (_logic_components[i + 3] == "ck" && first_clock)
                    {
                        FF[] ffs = FindObjectsOfType<FF>();
                        foreach (FF myff in ffs)
                            myff.LockDInput();
                        first_clock = false;
                    }
                    GameObject _destination = GameObject.Find(_logic_components[i + 2]);
                    if (_destination)
                    {
                        _destination.SendMessage("InputChanged_" + _logic_components[i + 3],
                                             _out);
                    }
                }
                else
                {
                    // Debug.Log("not matched name:  " + _logic_components[i]);
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
        Debug.Log("In " + this.name + " InputChanged_b, input_value = " + input_value);
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
