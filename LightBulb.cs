using UnityEngine;

public class LightBulb : MonoBehaviour
{
    private bool input_value;
    private Sprite mysprite;

    // Start is called before the first frame update
    void Start()
    {
        input_value = false;
    }

  
    public void InputChanged_input(bool new_value)
    {
        if (input_value == new_value) return;
        input_value = new_value;
        Debug.Log("In " + this.name + " InputChanged, input_value = " + input_value);
        if (input_value == false)
        {
            mysprite = Resources.Load<Sprite>("Light bulb off transparent");
            GetComponent<SpriteRenderer>().sprite = mysprite;
        }
        else
        {
            mysprite = Resources.Load<Sprite>("Light bulb on transparent");
            GetComponent<SpriteRenderer>().sprite = mysprite;
        }
        Debug.Log("In lightbulb, mysprite = " + GetComponent<SpriteRenderer>().sprite.name);
    }


 
}
