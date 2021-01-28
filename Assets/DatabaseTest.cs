using Assets.Scripts.Data.Imports.Engines;
using Assets.Scripts.Data.Services.Engines;
using UnityEngine;

public class DatabaseTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
