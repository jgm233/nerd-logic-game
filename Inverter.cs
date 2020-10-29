using System.Runtime.InteropServices;
using UnityEngine;

public class Inverter : BasicGate
{
    void Start()
    {
        EvaluateGate();
    }

    public override void EvaluateGate()
    {
        _out = !_a;
        // Debug.Log("In " + this.name + " EvaluateGate out =  " + _out);
        PropagateOutput();
    }
}

