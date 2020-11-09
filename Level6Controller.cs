using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level6Controller : BaseLevelController
{
    public override void SetLogicString()
    {
        _logic_in_level =
         "InputSwitch1 _out AndGate1 a " +
         "InputSwitch1 _out Inverter1 a " +
         "Inverter1 _out AndGate1 b " +
         "AndGate1 _out LightBulb a";
        Debug.Log("In SLS L6C, _logic_in_level = " + _logic_in_level);
    }
}
