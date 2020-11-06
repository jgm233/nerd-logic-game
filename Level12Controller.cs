using UnityEngine;

public class Level12Controller : BaseLevelController
{
    public override void SetLogicString()
    {
        _logic_in_level =
          "InputSwitch1 _out FF1 ck " +
            "InputSwitch1 _out FF2 ck " +
            "FF1 q AndGate1 a " +
            "FF2 q Inverter1 a " +
            "Inverter1 _out AndGate1 b " +
            "AndGate1 _out LightBulb input " +
            "FF1 q Inverter2 a " +
            "FF1 q OrGate1 a " +
            "Inverter2 _out AndGate3 a " +
            "InputSwitch2 _out AndGate3 b " +
            "InputSwitch2 _out Inverter3 a " +
            "Inverter3 _out AndGate2 a " +
            "FF2 q AndGate2 b " +
            "AndGate3 _out OrGate2 b " +
            "AndGate2 _out OrGate2 a " +
            "AndGate2 _out OrGate1 b " +
            "OrGate2 _out FF2 d " +
            "OrGate1 _out FF1 d";
        Debug.Log("In SLS L12C, _logic_in_level = " + _logic_in_level);
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
