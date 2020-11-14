using UnityEngine;

public class Level13Controller : BaseLevelController
{

    public override void SetLogicString()
    {
        _logic_in_level =
            "InputSwitch1 out FF1 ck " +
            "InputSwitch2 out Inverter1 a " +
            "Inverter1 out FF1 d " +
            "FF1 out LightBulb1 a";
        // All placed gates must have a number at the end of the name
        _parts_list = "InputSwitch1 SwitchDown 0.0 1.0 0.5 " +
           "InputSwitch2 SwitchDown 0.0 -5.0 0.5 " +
           "FF1 FF 10.0 0.0 .3 " +
           "Inverter1 Inverter 7.0 -3.0 .3 " +
            "LightBulb1 LightBulbOff 18.0 0 0.1";
        InstantiatePartsList();
        WireCircuit();
        Debug.Log("In SLS L13C, _logic_in_level = " + _logic_in_level);
        Debug.Log("In SLS L13C, _parts_list = " + _parts_list);

    }

}


