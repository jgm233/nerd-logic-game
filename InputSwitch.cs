using UnityEngine;

public class InputSwitch : BasicGate
{
    // private string spriteNames = "switch down"; 

    public override void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        _out = !_out;
        Debug.Log("in switch mouse down value is now " + _out);
        if (_out == false)
        {
            Sprite mysprite = Resources.Load<Sprite>("SwitchDown");
            GetComponent<SpriteRenderer>().sprite = mysprite;
        }
        else
        {
            Sprite mysprite = Resources.Load<Sprite>("SwitchUp");
            GetComponent<SpriteRenderer>().sprite = mysprite;
        }
        Debug.Log("In switch mouse down, mysprite = " + 
                GetComponent<SpriteRenderer>().sprite.name);
        BaseLevelController _lc = FindObjectOfType<BaseLevelController>();
        _lc.AdvanceClock();

        PropagateOutput();
    }

    
    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    
}
