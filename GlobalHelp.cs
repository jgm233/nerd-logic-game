using UnityEngine;

public class GlobalHelp : MonoBehaviour
{
    private bool _global_help_on = false;

    public void OnMouseDown()
    {
        _global_help_on = !_global_help_on;
        Debug.Log("In " + this.name + " global help is now on: " + _global_help_on);
    }

    // Update is called once per frame
    public bool IsGlobalHelpOn()
    {
        return _global_help_on;
    }
}
