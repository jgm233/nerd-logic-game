using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level4Controller : BaseLevelController
{
    public override void SetLogicString()
    {
        _logic_in_level =
         "InputSwitch1 _out AndGate1 a " +
         "InputSwitch2 _out AndGate1 b " +
         "InputSwitch3 _out NandGate1 b " +
         "AndGate1 _out NandGate1 a " +
         "NandGate1 _out LightBulb a";
        Debug.Log("In SLS L4C, _logic_in_level = " + _logic_in_level);
    }
}
