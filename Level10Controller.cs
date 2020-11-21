using UnityEngine;

public class Level10Controller : BaseLevelController
{
    public override void SetLogicString()
    {
         _wire_list =
         "InputSwitch1 _out NorGate1 a " +
            "InputSwitch2 _out NorGate2 b " +
            "NorGate1 _out NorGate2 a " +
            "NorGate2 _out NorGate1 b " +
            "NorGate1 _out LightBulb a";
        SetHintString("SR latch, high inputs set and reset the latch");
        // Debug.Log("In SLS L10C,  _wire_list = " +  _wire_list);
    }
}
