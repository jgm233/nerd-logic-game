using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level3Controller : BaseLevelController
{
    public override void SetLogicString()
    {
        _logic_in_level =
            "InputSwitch1 switch_value AndGate1 a " +
            "InputSwitch2 switch_value AndGate1 b " +
            "AndGate1 _out LightBulb input";
        Debug.Log("In SLS L3C, _logic_in_level = " + _logic_in_level);
    }
}
