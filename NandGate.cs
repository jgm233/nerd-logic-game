using UnityEngine;

public abstract class BasicGate : MonoBehaviour
{
    [SerializeField] protected bool _a, _b, _out;
    protected bool _show_output_value = false;

    void Start()
    {
        Debug.Log("In " + this.name + " start out =  " + _out);
        EvaluateGate();
    }

    public void OnMouseDown()
    {
        _show_output_value = !_show_output_value;
    }

    public void Update()
    {
        LineRenderer _lr = GetComponent<LineRenderer>();
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

    public abstract void EvaluateGate();

    protected void PropagateOutput()
    {
        LevelController _levelController = FindObjectOfType<LevelController>();

        if (_levelController == null)
        {
            Debug.Log("didn't find LevelController");
        }
        else
        {
            string[] _logic_components = _levelController.GetThisLevelsComponents();
            Debug.Log("in propagate outputs for " + this.name);
            for (int i = 0; i < _logic_components.Length; i += 4)
            {
                if (_logic_components[i] == this.name)
                {
                    Debug.Log("     matched name:  " + this.name);
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

    public void InputChanged_a(bool input_value)
    {
        Debug.Log("In " + this.name + " InputChanged_a, input_value = " + input_value);
        _a = input_value;
        EvaluateGate();
    }

    public void InputChanged_b(bool input_value)
    {
        Debug.Log("In " + this.name + " InputChanged_b, input_value = " + input_value);
        _b = input_value;
        EvaluateGate();
    }

}


public class NandGate : BasicGate
{
    public override void EvaluateGate()
    {
        bool new_out = !(_a & _b);
        Debug.Log("In " + this.name + " EvaluateGate new_out =  " + new_out);
        if (new_out == _out) return;
        _out = new_out;
        Debug.Log("In " + this.name + " EvaluateGate out =  " + _out);
        PropagateOutput();
    }

}
