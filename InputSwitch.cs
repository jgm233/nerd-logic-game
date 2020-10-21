using UnityEngine;

public class InputSwitch : MonoBehaviour
{
    // private string spriteNames = "switch down"; 
    private bool switch_value;
    private Sprite mysprite;

 
    private void Start ()
    {
        switch_value = true;
        //GetComponent<SpriteRenderer>().color = Color.green;
        // Debug.Log("In '" + this.name + "' start, mysprite = " + GetComponent<SpriteRenderer>().sprite.name);
        // Debug.Log("In switch start");
        // Force evaluation of the starting state
        OnMouseDown();
        OnMouseUp();
        // GameObject _mygo = this.gameObject;
        // Debug.Log("_mygo name = " + _mygo.name);
        LightBulb _lb = FindObjectOfType<LightBulb>();
        _lb.SetStartingValue();
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        switch_value = !switch_value;           
        // Debug.Log("in switch mouse down value is now " + switch_value);
        if (switch_value == false) { 
            mysprite = Resources.Load<Sprite>("Switch down");
            GetComponent<SpriteRenderer>().sprite = mysprite;
        } else
        {
            mysprite = Resources.Load<Sprite>("Switch up");
            GetComponent<SpriteRenderer>().sprite = mysprite;
        }
        //Debug.Log("In switch mouse down, mysprite = " + 
        //          GetComponent<SpriteRenderer>().sprite.name);

        LevelController _levelController = FindObjectOfType<LevelController>();
        _levelController.AdvanceClock();

        string[] _logic_components = _levelController.GetThisLevelsComponents();
        // Debug.Log("in evaluate for " + this.name);
        for (int i = 0; i < _logic_components.Length; i += 4)
        {
            if (_logic_components[i] == this.name)
            {
                // Debug.Log("matched name:  " + this.name);
                GameObject _destination = GameObject.Find(_logic_components[i + 2]);
                _destination.SendMessage("InputChanged_" + _logic_components[i + 3],
                                         switch_value);
            }
        }
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }


}
