using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicles : MonoBehaviour
{
    protected int healthPoint;
    protected float speedFire;
    protected int pointValue;
    [SerializeField] protected Transform shotOrigin;
    [SerializeField] protected GameObject ammoPrefab;
    private bool isReadyToShoot = true;

    public void TakeDamage(int damage)
    {
        healthPoint -= damage;
    }
    protected void Shoot(GameObject ammoType, Transform origin)
    {
        if (isReadyToShoot)
        {
            Instantiate(ammoType, origin.position, origin.rotation);
            isReadyToShoot = false;
            StartCoroutine(ShootCooldown());
        }
    }
    protected void Shoot(GameObject ammoType, Transform origin, Vector3 direction)
    {

    }

    private void UpdateLifebar()
    {

    }

    IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(1 / speedFire);
        isReadyToShoot = true;
    }
}
