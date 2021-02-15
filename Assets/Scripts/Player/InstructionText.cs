using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class InstructionText : MonoBehaviour
    {
        private KeyMap keyMap;
        // Start is called before the first frame update
        void Start()
        {
            keyMap = GameObject.Find("/SceneScripts").GetComponent<KeyMap>();
            this.GetComponent<Text>().text = 
                @$"
{keyMap.Foreward}: Forward

{keyMap.TurnLeft}: Rotate Left

{keyMap.TurnAround}: Rotate Behind

{keyMap.TurnRight}: Rotate Right

{keyMap.Land}: Land

{keyMap.Fire}: Fire Cannon
        "
                ;
        }

        // Update is called once per frame
        void Update()
        {
            this.GetComponent<Text>().text =
                @$"
{keyMap.Foreward}: Forward

{keyMap.TurnLeft}: Rotate Left

{keyMap.TurnAround}: Rotate Behind

{keyMap.TurnRight}: Rotate Right

{keyMap.Land}: Land

{keyMap.Fire}: Fire Cannon
        ";
        }
    }
}
