using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class InputSwitch : BasicGate
{
    // private string spriteNames = "switch down"; 

    public override void OnMouseDown()
    {
        GameMode mygamemode = FindObjectOfType<GameMode>();
        if (mygamemode.IsInPlacementMode())
        {
            if (IsPlacedGate())
            {
                Destroy(gameObject);
                return;
            }
            _newGate = Instantiate(this);
            _newGate.name = this.name + _newGateCount.ToString();
            _newGate.transform.localScale = new Vector3(MyScale(), MyScale(), MyScale());
            _newGateCount++;
        }
        else if (mygamemode.IsInRouteMode())
        {
            LineRenderer mylr = GetComponent<LineRenderer>();
            Vector3[] positions = new Vector3[2];
            positions[0] = this.transform.position;
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0;
            positions[1] = newPosition;
            // Debug.Log("In OnMouseDown for Route, this = " + this.name);
            mylr.startWidth = 0.2f;
            mylr.endWidth = 0.2f;
            mylr.widthMultiplier = 0.2f;
            mylr.startColor = Color.yellow;
            mylr.endColor = Color.yellow;
            mylr.material = AssetDatabase.GetBuiltinExtraResource<Material>("Default-Line.mat");
            mylr.positionCount = 2;
            mylr.SetPositions(positions);
            _output_gate = this;
            // Debug.Log("In OnMouseDown for InputSwitch, this = " + this.name);
        }
        else  // Must be in Play mode
        {

            GetComponent<SpriteRenderer>().color = Color.green;
            _out = !_out;
            // Debug.Log("in switch mouse down value is now " + _out);
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
            // Debug.Log("In switch mouse down, mysprite = " +
            //        GetComponent<SpriteRenderer>().sprite.name);
            BaseLevelController _lc = FindObjectOfType<BaseLevelController>();
            _lc.AdvanceClock();

            PropagateOutput();
        }
    }

    public override float MyScale()
    {
        return 0.5f;
    }

}
