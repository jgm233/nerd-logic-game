using UnityEngine;

public class NorGate : BasicGate
{

    public override void EvaluateGate()
    {
        bool new_out = !(_a | _b);
        // Debug.Log("In " + this.name + " EvaluateGate new_out =  " + new_out);
        if (new_out == _out) return;
        _out = new_out;
        // Debug.Log("In " + this.name + " EvaluateGate out =  " + _out);
        PropagateOutput();
    }

    public override float inputa_x_adjust() { return -0.86f; }
    public override float inputb_x_adjust() { return -0.82f; }
    public override float output_x_adjust() { return 0.75f; }

}
