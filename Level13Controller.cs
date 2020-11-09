using UnityEngine;

public class Level13Controller : BaseLevelController
{

    public override void SetLogicString()
    {
        _logic_in_level =
            "InputSwitch1 out FF1 ck " +
            "InputSwitch1 out FF2 ck " +
            "InputSwitch2 out FF1 d " +
            "InputSwitch2 out FF2 d " +
            "FF1 out Inverter1 a " +
            "InputSwitch2 out Inverter3 a " +
            "Inverter1 out LightBulb a";
        _parts_list = "InputSwitch1 SwitchDown 0.0 3.0 0.5 " +
           "InputSwitch2 SwitchDown 0.0 -3.0 0.5 " +
           "FF1 FF 7.0 0.0 .3 " +
           "Inverter1 Inverter 10.0 0.0 .3 " +
           "FF2 FF 7.0 -4.0 .3 " +
           "Inverter3 Inverter 10.0 -2.0 .3 " +
            "LightBulb LightBulbOff 14.0 2.0 0.1";
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


