using UnityEngine;
using System.Collections;

public class Choice : MonoBehaviour {

    public string choiceName;
    public string function;
    public string classString;

    public string ChoiceName { get { return this.choiceName; } }
    public string Action { get { return this.function; } }
    public string ClassString { get { return this.classString; } }

    public Choice(string choiceName, string classString, string function)
    {
        this.choiceName = choiceName;
        this.function = function;
        this.classString = classString;
    }
}
