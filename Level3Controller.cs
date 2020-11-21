using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level3Controller : BaseLevelController
{
    public override void SetLogicString()
    {
         _wire_list =
            "InputSwitch1 out AndGate1 a " +
            "InputSwitch2 out AndGate1 b " +
            "AndGate1 out LightBulb a";
        SetHintString("AND gates: both input have to be one for the output to be one");
        // Debug.Log("In SLS L3C,  _wire_list = " +  _wire_list);
    }
}
