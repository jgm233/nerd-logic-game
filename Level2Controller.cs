using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level2Controller : BaseLevelController
{
    private Text intro_text;

    public override void AddText () { 
        // Intro Text
        myText = new GameObject();
        myText.transform.parent = myGO.transform;
        myText.name = "IntroText";
        // Debug.Log("Intro mytext.transform = " + myText.transform);

        intro_text = myText.AddComponent<Text>();
        intro_text.font = (Font)Resources.Load("Anton");
        intro_text.text = "Clicking on a gate highlights the outputs with green = 1, and red = 0.\nClicking on the 'Help' button highlights the outputs of all the gates and switches.";
        intro_text.fontSize = 36;

        // Text position
        rectTransform = intro_text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(8.0f, -6.8f, 0);
        rectTransform.sizeDelta = new Vector2(1000, 1000);
        rectTransform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
    }    

        // Update is called once per frame, don't do anything for intro screens
    public override void Update() { }

}
