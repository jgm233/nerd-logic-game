using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level4Controller : BaseLevelController
{
    public override void SetLogicString()
    {
         _wire_list =
         "InputSwitch1 _out AndGate1 a " +
         "InputSwitch2 _out AndGate1 b " +
         "InputSwitch3 _out NandGate1 b " +
         "AndGate1 _out NandGate1 a " +
         "NandGate1 _out LightBulb a";
        SetHintString("NAND gates: same as AND + inverter on the output");
        // Debug.Log("In SLS L4C,  _wire_list = " +  _wire_list);
    }
}
