using System.Threading;
using UnityEngine;

public class OrGate : MonoBehaviour
{
    [SerializeField] bool _a, _b, _out;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("In and gate start");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void EvaluateGate()
    {
        _out = (_a | _b);
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
                                             _out);
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

