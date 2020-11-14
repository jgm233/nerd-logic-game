using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level5Controller : BaseLevelController
{
    public override void SetLogicString()
    {
        _logic_in_level =
         "InputSwitch1 _out Inverter1 a " +
         "Inverter1 _out AndGate1 a " +
         "InputSwitch2 _out AndGate1 b " +
         "InputSwitch3 _out NandGate1 b " +
         "AndGate1 _out NandGate1 a " +
         "NandGate1 _out LightBulb a";
        SetHintString("Watch out for the inverter on one of the switches");
        Debug.Log("In SLS L5C, _logic_in_level = " + _logic_in_level);
    }
}
