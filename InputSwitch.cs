using System.Runtime.InteropServices;
using UnityEngine;

public class InputSwitch : MonoBehaviour
{
    // private string spriteNames = "switch down"; 
    [SerializeField] private bool switch_value;
    private Sprite mysprite;


    private void Start()
    {
        switch_value = true;
        //GetComponent<SpriteRenderer>().color = Color.green;
        Debug.Log("In '" + this.name + "' start, mysprite = " + GetComponent<SpriteRenderer>().sprite.name);
        Debug.Log("In switch start");
        // Force evaluation of the starting state and change switch_value to false in the process.
        OnMouseDown();
        OnMouseUp();
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        switch_value = !switch_value;
        Debug.Log("in switch mouse down value is now " + switch_value);
        if (switch_value == false)
        {
            mysprite = Resources.Load<Sprite>("Switch down");
            GetComponent<SpriteRenderer>().sprite = mysprite;
        }
        else
        {
            mysprite = Resources.Load<Sprite>("Switch up");
            GetComponent<SpriteRenderer>().sprite = mysprite;
        }
        Debug.Log("In switch mouse down, mysprite = " + 
                  GetComponent<SpriteRenderer>().sprite.name);

        LevelController _levelController = FindObjectOfType<LevelController>();
        _levelController.AdvanceClock();

        string[] _logic_components = _levelController.GetThisLevelsComponents();
        Debug.Log("in evaluate for " + this.name);
        bool first_clock = true;
        for (int i = 0; i < _logic_components.Length; i += 4)
        {
            if (_logic_components[i] == this.name)
            {
                Debug.Log("   matched name:  " + this.name);
                Debug.Log("   destination name:  " + _logic_components[i + 2] + "/" + _logic_components[i + 3]);
                // If this is the first FF to be clocked with this switch change, lock the D-inputs for all FFs
                if (_logic_components[i + 3] == "ck" && first_clock)
                {
                    FF[] ffs = FindObjectsOfType<FF>();
                    foreach (FF myff in ffs)
                        myff.LockDInput();
                    first_clock = false;
                }
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

    public void Update()
    {
        LineRenderer _lr = GetComponent<LineRenderer>();
        _lr.startWidth = 0.1f;
        _lr.endWidth = 0.1f;
        _lr.widthMultiplier = 0.1f;
        GlobalHelp _gh = FindObjectOfType<GlobalHelp>();
        if (_gh.IsGlobalHelpOn())
        {
            if (switch_value)
            {
                _lr.startColor = Color.green;
                _lr.endColor = Color.green;
            } else
            {
                _lr.startColor = Color.red;
                _lr.endColor = Color.red;
            }
        } else
        {
            _lr.startColor = Color.black;
            _lr.endColor = Color.black;

        }

    }
}
