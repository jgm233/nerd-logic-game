using UnityEngine;

public class Level9Controller : BaseLevelController
{
    public override void SetLogicString()
    {
        _logic_in_level =
         "InputSwitch1 _out NandGate1 a " +
            "InputSwitch2 _out NandGate2 b " +
            "NandGate1 _out NandGate2 a " +
            "NandGate2 _out NandGate1 b " +
            "NandGate1 _out LightBulb a";
        SetHintString("SR latch, low inputs set and reset the latch");
        Debug.Log("In SLS L9C, _logic_in_level = " + _logic_in_level);
    }
}
