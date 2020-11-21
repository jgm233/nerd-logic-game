using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level7Controller : BaseLevelController
{
    public override void SetLogicString()
    {
         _wire_list =
            "InputSwitch1 _out Inverter1 a " +
            "Inverter1 _out NandGate1 a " +
            "InputSwitch2 _out NandGate1 b " +
            "InputSwitch3 _out Inverter2 a " +
            "Inverter2 _out OrGate1 b " +
            "NandGate1 _out OrGate1 a " +
            "OrGate1 _out LightBulb a";
        SetHintString("The final gate is an OR gate with Demorgans transformation");
        // Debug.Log("In SLS L7C,  _wire_list = " +  _wire_list);
    }
}
