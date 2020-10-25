using UnityEngine;
using UnityEngine.UI;

public class FF : MonoBehaviour
{
    [SerializeField] private bool _ck, _d, _previous_d; 
    [SerializeField] private bool _q = false;
    [SerializeField] private static bool _use_previous_d = false;
    
    private void EvaluateGate()
    {
        if (_use_previous_d)
        {
            _q = _previous_d;
        } else
        {
            _q = _d;
        }

        // LevelController _levelController = GetComponent<LevelController>();
        LevelController _levelController = FindObjectOfType<LevelController>();

        if (_levelController == null)
        {
            Debug.Log("didn't find LevelController");
        }
        else
        {
            string[] _logic_components = _levelController.GetThisLevelsComponents();
            // Debug.Log("in evaluate gate for " + this.name);
            for (int i = 0; i < _logic_components.Length; i += 4)
            {
                if (_logic_components[i] == this.name)
                {
                    // Debug.Log("matched name:  " + this.name);
                    GameObject _destination = GameObject.Find(_logic_components[i + 2]);
                    _destination.SendMessage("InputChanged_" + _logic_components[i + 3],
                                             _q);
                }
                else
                {
                    // Debug.Log("not matched name:  " + _logic_components[i]);
                }
            }
        }
        //        GameObject _lightbulb = GameObject.Find("LightBulb"); 
        //        _lightbulb.SendMessage("InputChanged_input", _out);
    }

    // New comment

    public void InputChanged_ck(bool input_value)
    {
        //Debug.Log("In " + this.name + " InputChanged_ck, input_value = " + input_value);
        //Debug.Log("In " + this.name + " InputChanged_ck, _d = " + _d);
        //Debug.Log("In " + this.name + " InputChanged_ck, _previous_d = " + _previous_d);
        //Debug.Log("In " + this.name + " InputChanged_ck, _use_previous_d = " + _use_previous_d);
        if (!_ck && input_value)
        {
            EvaluateGate();
            _use_previous_d = true;        
        } else
        {
            _use_previous_d = false;
        }
        _ck = input_value;
        _previous_d = _d;
    }

    public void InputChanged_d(bool input_value)
    {
        //Debug.Log("In " + this.name + " InputChanged_d, input_value = " + input_value);
        _previous_d = _d;
        //Debug.Log("In " + this.name + " InputChanged_d, _previous_d = " + _previous_d);
        _d = input_value;
        //Debug.Log("In " + this.name + " InputChanged_ck, _use_previous_d = " + _previous_d);

    }
}
