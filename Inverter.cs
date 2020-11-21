using System.Runtime.InteropServices;
using UnityEngine;

public class Inverter : BasicGate
{
  public override void EvaluateGate()
    {
        _out = !_a;
        // Debug.Log("In " + this.name + " EvaluateGate out =  " + _out);
        PropagateOutput();
    }

    public override float inputa_x_adjust() { return -0.83f; }
    public override float inputa_y_adjust() { return -0.0f; }
    public override float output_x_adjust() { return 0.8f; }

    public override string GateInputs()
    {
        return "a";
    }
}

