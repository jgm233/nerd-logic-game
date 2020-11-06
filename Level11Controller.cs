using UnityEngine;

public class Level11Controller : BaseLevelController
{
    public override void SetLogicString()
    {
        _logic_in_level =
        "InputSwitch1 _out FF1 ck " +
            "InputSwitch1 _out FF2 ck " +
            "InputSwitch2 _out FF1 d " +
            "FF1 q Inverter1 a " +
            "Inverter1 _out AndGate1 a " +
            "FF1 q FF2 d " +
            "FF2 q AndGate1 b " +
            "AndGate1 _out LightBulb input";
        Debug.Log("In SLS L11C, _logic_in_level = " + _logic_in_level);
    }
}
