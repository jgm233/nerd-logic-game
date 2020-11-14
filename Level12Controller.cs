using UnityEngine;

public class Level12Controller : BaseLevelController
{
    public override void SetLogicString()
    {
        _logic_in_level =
          "InputSwitch1 out FF1 ck " +
            "InputSwitch1 out FF2 ck " +
            "FF1 q AndGate1 a " +
            "FF2 q Inverter1 a " +
            "Inverter1 out AndGate1 b " +
            "AndGate1 out LightBulb a " +
            "FF1 q Inverter2 a " +
            "FF1 q OrGate1 a " +
            "Inverter2 out AndGate3 a " +
            "InputSwitch2 out AndGate3 b " +
            "InputSwitch2 out Inverter3 a " +
            "Inverter3 out AndGate2 a " +
            "FF2 q AndGate2 b " +
            "AndGate3 out OrGate2 b " +
            "AndGate2 out OrGate2 a " +
            "AndGate2 out OrGate1 b " +
            "OrGate2 out FF2 d " +
            "OrGate1 out FF1 d";
        SetHintString("Single input go-no go 4-state state-machine using edge-triggered flip-flops");
        Debug.Log("In SLS L12C, _logic_in_level = " + _logic_in_level);
    }

}