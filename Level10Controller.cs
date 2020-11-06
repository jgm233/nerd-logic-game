using UnityEngine;

public class Level10Controller : BaseLevelController
{
    public override void SetLogicString()
    {
        _logic_in_level =
         "InputSwitch1 _out NorGate1 a " +
            "InputSwitch2 _out NorGate2 b " +
            "NorGate1 _out NorGate2 a " +
            "NorGate2 _out NorGate1 b " +
            "NorGate1 _out LightBulb input";
        Debug.Log("In SLS L10C, _logic_in_level = " + _logic_in_level);
    }
}
