using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    // String describing login in each level
    // Format :  <Output gate> <output pin> <dest gate> <dest pin>"
    // One set of four values for each connection
    // The instantiations for each component is handled in the
    // scene manager (for now).  I might add code to instantiate each
    // component and draw the wires at a later time.
    private string[] _logic_in_level =
        {"dummy string for index0",
         "InputSwitch1 switch_value AndGate1 a " +
            "InputSwitch2 switch_value AndGate1 b " +
            "AndGate1 _out LightBulb input",
         "InputSwitch1 _out AndGate1 a " +
            "InputSwitch2 _out AndGate1 b " +
            "InputSwitch3 _out NandGate1 b " +
            "AndGate1 _out NandGate1 a " +
            "NandGate1 _out LightBulb input" };

  
    private static int _nextLevelIndex = 2;

    public string[] GetThisLevelsComponents()
    {
        return (_logic_in_level[_nextLevelIndex].Split(' '));
    }

    public LevelController Find() { return this; }

    // Start is called before the first frame update
    void OnEnable()
    {
       Debug.Log("_logic_in_level["+ _nextLevelIndex + "] = "+ _logic_in_level[_nextLevelIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        /* foreach (Enemy enemy in _enemies)
        {
            if (enemy != null)
                return;
        }

        Debug.Log("You killed all the enemies in Level" + _nextLevelIndex);
        if (_nextLevelIndex > 2) return;
        _nextLevelIndex++;
        string nextLevelName = "Level" + _nextLevelIndex;
        SceneManager.LoadScene(nextLevelName);
        */
    }
}
