using UnityEngine;

public class LightBulb : MonoBehaviour
{
    private int input_value;
    private Sprite mysprite;

    // Start is called before the first frame update
    void Start()
    {
        input_value = 0;
    }

  
    public void InputChanged(int new_value)
    {
        if (input_value == new_value) return;
        input_value = new_value;
        Debug.Log("In " + this.name + " InputChanged, input_value = " + input_value);
        if (input_value == 0)
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
