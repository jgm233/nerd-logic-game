using UnityEngine;

public class Level13Controller : BaseLevelController
{

    public override void SetLogicString()
    {
        _logic_in_level =
            "InputSwitch1 out AndGate1 a " +
            "InputSwitch2 out AndGate1 b " +
            "AndGate1 out LightBulb a";
        _parts_list = "InputSwitch1 SwitchDown 0.0 3.0 0.5 " +
           "InputSwitch2 SwitchDown 0.0 -3.0 0.5 " +
           "AndGate1 AndGate 8.0 0.0 .3 " +
            "LightBulb LightBulbOff 12.0 2.0 0.1";
        InstantiatePartsList();
        WireCircuit();
        Debug.Log("In SLS L13C, _logic_in_level = " + _logic_in_level);
        Debug.Log("In SLS L13C, _parts_list = " + _parts_list);

    }

    public override void OnMouseDown()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        return;
#else
        Application.Quit();
#endif
    }
}


