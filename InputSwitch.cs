using UnityEngine;

public class InputSwitch : MonoBehaviour
{
    // private string spriteNames = "switch down"; 
    private int switch_value;
    private Sprite mysprite;
    LineRenderer l_renderer;

 
    private void Start ()
    {
        switch_value = 0;
        //GetComponent<SpriteRenderer>().color = Color.green;
        Debug.Log("In '" + this.name + "' start, mysprite = " + GetComponent<SpriteRenderer>().sprite.name);
        l_renderer = GetComponent<LineRenderer>();
        Debug.Log("In switch start, line renderer = " + l_renderer.name);
        l_renderer.startColor = Color.black;
        l_renderer.endColor = Color.black;
        l_renderer.startWidth = 0.1f;
        l_renderer.endWidth = 0.1f;
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        switch_value = (switch_value + 1) % 2;           
        Debug.Log("in switch mouse down value is now " + switch_value);
        if (switch_value == 0) { 
            mysprite = Resources.Load<Sprite>("Switch down");
            GetComponent<SpriteRenderer>().sprite = mysprite;
        } else
        {
            mysprite = Resources.Load<Sprite>("Switch up");
            GetComponent<SpriteRenderer>().sprite = mysprite;
        }
        Debug.Log("In switch mouse down, mysprite = " + GetComponent<SpriteRenderer>().sprite.name);
        //gameObject.SendMessage("InputChanged_" + this.name, switch_value);
        GameObject _andgate1 = GameObject.Find("AndGate1");
        if (this.name == "InputSwitch1")
           _andgate1.SendMessage("InputChanged_a", switch_value);
        else
            _andgate1.SendMessage("InputChanged_b", switch_value);
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }


}
