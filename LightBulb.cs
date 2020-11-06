using UnityEngine;

public class LightBulb : MonoBehaviour
{
    [SerializeField] private bool _input_value = false;
    private Sprite _mysprite;
    [SerializeField] private bool _starting_value;
    [SerializeField] private bool _switched = false;
    [SerializeField] private int _clock_last_switched = 0;
    [SerializeField] private bool _glitched = false;
    [SerializeField] private bool _last_value = false;

    //This has to be done after the switches
    // have all evaluated the first time
    public void Start()
    {
        //Debug.Log("in LB Start");
        _starting_value = _input_value;
        _last_value = _input_value;
        _switched = false;
        _glitched = false;
        //Debug.Log("in SSV, switched = " + _switched);
        //Debug.Log("in SSV, glitch = " + _glitched);
    }

    public bool Glitched()
    {
        // Debug.Log("glitch = " + _glitched);
        return (_glitched);
    }

    public bool Switched()
    {
        return (_switched);
        // Debug.Log("switched = " + _switched);
    }

    public void InputChanged_input(bool new_value)
    {
        if (_input_value == new_value) return;
        _switched = true;
        _input_value = new_value;
        BaseLevelController _lc = FindObjectOfType<BaseLevelController>();
        int clock_period = _lc.GetClockPeriod();
        if (clock_period == _clock_last_switched && !_glitched)
           _glitched = (_last_value != _input_value);
        // Debug.Log("In " + this.name + " InputChanged, input_value = " + _input_value + ", clock = " + _lc.GetClockPeriod());
        // Debug.Log("In " + this.name + " InputChanged, last_value = " + _last_value + " glitched = " + _glitched);
        // Debug.Log("In " + this.name + " InputChanged, clock_last_switched = " + _clock_last_switched);
        if (_input_value == false)
        {
            _mysprite = Resources.Load<Sprite>("Light bulb off transparent");
            GetComponent<SpriteRenderer>().sprite = _mysprite;
        }
        else
        {
            _mysprite = Resources.Load<Sprite>("Light bulb on transparent");
            GetComponent<SpriteRenderer>().sprite = _mysprite;
        }
        _last_value = _input_value;
        _clock_last_switched = clock_period;
        // Debug.Log("In lightbulb, mysprite = " + GetComponent<SpriteRenderer>().sprite.name);
    }
}
