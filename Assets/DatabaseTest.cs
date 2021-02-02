using Assets.Scripts.Data.Services;
using UnityEngine;

namespace Assets
{
    public class DatabaseTest : MonoBehaviour
    {
        public void OnClick()
        {
            Import();
        }

        private void Import()
        {
            var service = new EnginesService();
            var ab = service.GetByName("Afterburner");

            print(ab.Name);
        }
    }
}
