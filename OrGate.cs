using System.Threading;
using UnityEngine;



public class OrGate : BasicGate
{

    void Start()
    {
        EvaluateGate();
    }


    public override void EvaluateGate()
    {
        _out = (_a | _b);
        // Debug.Log("In " + this.name + " EvaluateGate out =  " + _out);
        PropagateOutput();
    }
}


