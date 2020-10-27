using System.Threading;
using UnityEngine;


public class AndGate : BasicGate
{
    public override void EvaluateGate()
    {
        _out = (_a & _b);
        // Debug.Log("In " + this.name + " EvaluateGate out =  " + _out);
        PropagateOutput();
    }
}
