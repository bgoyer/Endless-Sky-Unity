using Assets.Resources.Data.ModelData.Services;
using UnityEngine;

namespace Assets.Resources.Data.ModelData
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
