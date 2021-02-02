using Assets.Scripts.Ship;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private GameObject ship;
        private Transform weaponClasses;

        private void Start()
        {
            ship = this.transform.GetChild(0).gameObject;
            weaponClasses = ship.transform.GetChild(0).gameObject.transform;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            foreach (Transform weapons in weaponClasses)
            {
                foreach (GameObject weapon in weapons)
                {
                    if (weapon.transform.GetChild(0).childCount > 1)
                    {
                        weapon.transform.GetChild(0).GetChild(0).GetComponent<WeaponController>().Shoot(ship.GetComponent<Rigidbody2D>().velocity.magnitude);
                    }
                }
            }
        }
    }
}