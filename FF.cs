using UnityEngine;
using UnityEngine.UI;

public class FF : BasicGate
{
    [SerializeField] private bool _ck, _d, _locked_d; 
    [SerializeField] private static bool _use_previous_d = false;
    
    public override void EvaluateGate()
    {
        _out = _locked_d;
        PropagateOutput();
    }
    
    public void LockDInput()
    {
        _locked_d = _d;
        Debug.Log("In LockDInput for FF " + this.name + " _locked_d = " + _locked_d);
    }

    public void InputChanged_ck(bool input_value)
    {
        //Debug.Log("In " + this.name + " InputChanged_ck, input_value = " + input_value);
        //Debug.Log("In " + this.name + " InputChanged_ck, _d = " + _d);
        if (!_ck && input_value)
            EvaluateGate();
        _ck = input_value;
    }

    public void InputChanged_d(bool input_value)
    {
        Debug.Log("In " + this.name + " InputChanged_d, input_value = " + input_value);
          _d = input_value;
    }
}
