using UnityEngine;

public class Inverter : MonoBehaviour
{
    [SerializeField] bool _a, _out;
 
    private void EvaluateGate()
    {
        _out = !_a;
        LevelController _levelController = FindObjectOfType<LevelController>();
        string[] _logic_components = _levelController.GetThisLevelsComponents();
        Debug.Log("in evaluate gate for " + this.name);
        for (int i = 0; i < _logic_components.Length; i += 4)
        {
            if (_logic_components[i] == this.name)
            {
                GameObject _destination = GameObject.Find(_logic_components[i + 2]);
                _destination.SendMessage("InputChanged_" + _logic_components[i + 3],
                                         _out);
            }
        }
    }

    // New comment

    public void InputChanged_a(bool input_value)
    {
        Debug.Log("In " + this.name + " InputChanged_a, input_value = " + input_value);
        _a = input_value;
        EvaluateGate();
    }

}

