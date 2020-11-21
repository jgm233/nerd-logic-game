using UnityEngine;

public class Level14Controller : BaseLevelController
{

    public override void SetLogicString()
    {
        // This level lets the user create his/her own circuit and debug it.
        GameMode mygamemode = FindObjectOfType<GameMode>();
        mygamemode.SetPlacementMode();
    }

    public override void OnMouseDown()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        return;
#else
        Application.Quit();
#endif
    }


}
