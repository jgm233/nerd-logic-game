using UnityEngine;
using UnityEngine.UI;

public class FF : BasicGate
{
    [SerializeField] private bool _ck, _d, _locked_d; 
    
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

    public override float output_x_adjust() { return 0.38f; }
    public override float output_y_adjust() { return -.2f; }
    public override float inputd_x_adjust() { return -0.95f; }
    public override float inputd_y_adjust() { return 0.25f; }
    public override float inputck_x_adjust() { return -0.3f; }
    public override float inputck_y_adjust() { return 0.8f; }

}
