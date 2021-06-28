using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.OffScreenIndicator
{
    public class SetBoxIndicatorName : MonoBehaviour
    {
        public Text Text;

        public void SetName(string name)
        {
            Text.text = name;
        }
    }
}