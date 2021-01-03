using UnityEngine;
using UnityEngine.UI;

public class SetBoxIndicatorName : MonoBehaviour
{
    public Text Text;

    public void SetName(string name)
    {
        Text.text = name;
    }
}