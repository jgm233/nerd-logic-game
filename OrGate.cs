using System.Threading;
using UnityEngine;



public class OrGate : BasicGate
{
    public override void EvaluateGate()
    {
        bool new_out = (_a | _b);
        // Debug.Log("In " + this.name + " EvaluateGate new_out =  " + new_out);
        if (new_out == _out) return;
        _out = new_out;
        // Debug.Log("In " + this.name + " EvaluateGate out =  " + _out);
        PropagateOutput();
    }
}


