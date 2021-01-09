using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform ship;
    private GameObject player;
    private void Start()
    {
        player = GameObject.Find("/Player").gameObject;
    }

    private void Update()
    {
        if (player.transform.childCount > 0)
        {
            ship = player.transform.GetChild(0).transform;
            this.transform.position = new Vector3(ship.position.x, ship.position.y, -10);
        }
    }
}