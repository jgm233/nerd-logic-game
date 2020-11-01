using System;
using UnityEngine;
using UnityEngine.UI;

public class FF : BasicGate
{
    [SerializeField] private bool _ck, _d, _previous_d; 
    [SerializeField] private static bool _use_previous_d = false;

    public override void EvaluateGate()
    {
        if (_use_previous_d)
        {
            _out = _previous_d;
        }
        else
        {
            _out = _d;
        }
        PropagateOutput();
    }

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
        Debug.Log("In " + this.name + " InputChanged_d, input_value = " + input_value);
        _previous_d = _d;
        Debug.Log("In " + this.name + " InputChanged_d, _previous_d = " + _previous_d);
        _d = input_value;
        Debug.Log("In " + this.name + " InputChanged_ck, _use_previous_d = " + _previous_d);

    }
}
