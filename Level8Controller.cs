using UnityEngine;

public class Level8Controller : BaseLevelController
{
    public override void SetLogicString()
    {
         _wire_list =
         "InputSwitch1 _out Inverter1 a " +
            "Inverter1 _out NandGate1 a " +
            "InputSwitch2 _out NandGate1 b " +
            "InputSwitch3 _out NandGate2 a " +
            "InputSwitch4 _out NandGate2 b " +
            "NandGate1 _out AndGate1 a " +
            "NandGate2 _out AndGate1 b " +
            "AndGate1 _out LightBulb a";
        SetHintString("The final gate is an AND gate with Demorgans transformation");
        // Debug.Log("In SLS L8C,  _wire_list = " +  _wire_list);
    }
}
