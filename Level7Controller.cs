using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level7Controller : BaseLevelController
{
    public override void SetLogicString()
    {
        _logic_in_level =
            "InputSwitch1 _out Inverter1 a " +
            "Inverter1 _out NandGate1 a " +
            "InputSwitch2 _out NandGate1 b " +
            "InputSwitch3 _out Inverter2 a " +
            "Inverter2 _out OrGate1 b " +
            "NandGate1 _out OrGate1 a " +
            "OrGate1 _out LightBulb a";
        Debug.Log("In SLS L7C, _logic_in_level = " + _logic_in_level);
    }
}
