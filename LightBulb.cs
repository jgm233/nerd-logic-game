using UnityEngine;

public class LightBulb : MonoBehaviour
{
    private bool input_value;
    private Sprite mysprite;
    private bool starting_value;
    private bool switched;

    // Start is called before the first frame update
    void Start()
    {
        input_value = false;
    }

    public void SetStartingValue()
    {
        starting_value = input_value;
    }

    public bool Switched()
    {
        if (switched) return (true);
        if (starting_value != input_value)
        {
            switched = true;
        }
        return (switched);
    }

    public void InputChanged_input(bool new_value)
    {
        if (input_value == new_value) return;
        input_value = new_value;
        // Debug.Log("In " + this.name + " InputChanged, input_value = " + input_value);
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
        // Debug.Log("In lightbulb, mysprite = " + GetComponent<SpriteRenderer>().sprite.name);
    }


 
}
