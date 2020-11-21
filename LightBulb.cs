using UnityEngine;

public class LightBulb : BasicGate
{
    [SerializeField] private bool _starting_value;
    [SerializeField] private bool _switched = false;
    [SerializeField] private int _clock_last_switched = 0;
    [SerializeField] private bool _glitched = false;
    [SerializeField] private bool _last_value = false;

    //This has to be done after the switches
    // have all evaluated the first time
    public override void Start()
    {
        //Debug.Log("in LB Start");
        _starting_value = _a;
        _last_value = _a;
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

    public override void InputChanged_a(bool new_value)
    {
        if (_a == new_value) return;
        BaseLevelController _lc = FindObjectOfType<BaseLevelController>();
        int clock_period = _lc.GetClockPeriod();
        if (clock_period != 0) 
            _switched = true;
        _a = new_value;
        if (clock_period == _clock_last_switched && !_glitched && clock_period != 0)
           _glitched = (_last_value != _a);
        // Debug.Log("In " + this.name + " InputChanged, input_value = " + _a + ", clock = " + _lc.GetClockPeriod());
        // Debug.Log("In " + this.name + " InputChanged, last_value = " + _last_value + " glitched = " + _glitched);
        // Debug.Log("In " + this.name + " InputChanged, clock_last_switched = " + _clock_last_switched);
        if (_a == false)
        {
            Sprite mysprite = Resources.Load<Sprite>("LightBulbOff");
            GetComponent<SpriteRenderer>().sprite = mysprite;
        }
        else
        {
            Sprite mysprite = Resources.Load<Sprite>("LightBulbOn");
            GetComponent<SpriteRenderer>().sprite = mysprite;
        }
        _last_value = _a;
        _clock_last_switched = clock_period;
        // Debug.Log("In lightbulb, mysprite = " + GetComponent<SpriteRenderer>().sprite.name);
    }

    public override float MyScale()
    {
        return 0.1f;
    }


    public override float inputa_x_adjust() { return 0.0f; }
    public override float inputa_y_adjust() { return -2.0f; }

    public override string GateInputs()
    {
        return "a";
    }
}
