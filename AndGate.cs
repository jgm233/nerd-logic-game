using UnityEngine;

public class AndGate : MonoBehaviour
{
    LineRenderer l_renderer;
    int _a, _b, _out;

    // Start is called before the first frame update
    void Start()
    {
        l_renderer = GetComponent<LineRenderer>();
        Debug.Log("In and gate start, line renderer = " + l_renderer.name);
        l_renderer.startColor = Color.black;
        l_renderer.endColor = Color.black;
        l_renderer.startWidth = 0.1f;
        l_renderer.endWidth = 0.1f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EvaluateGate()
    {
        if (_a == 1 && _b == 1)
        {
            _out = 1;
        } else
        {
            _out = 0;
        }
        GameObject _lightbulb = GameObject.Find("LightBulb"); 
        _lightbulb.SendMessage("InputChanged", _out);
    }

    // New comment
    
    public void InputChanged_a(int input_value)
    {
        Debug.Log("In " + this.name + " InputChanged_a, input_value = " + input_value);
        _a = input_value;
        EvaluateGate();
    }

    public void InputChanged_b(int input_value)
    {
        Debug.Log("In " + this.name + " InputChanged_b, input_value = " + input_value);
        _b = input_value;
        EvaluateGate();
    }
}
