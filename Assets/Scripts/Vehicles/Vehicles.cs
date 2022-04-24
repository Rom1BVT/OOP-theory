using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Vehicles : MonoBehaviour
{
    protected float healthPoint;
    protected float speedFire;
    protected int pointValue;
    [SerializeField] protected Transform shotOrigin;
    [SerializeField] protected GameObject ammoPrefab;
    [SerializeField] protected Slider HealthbarSliderPrefab;
    protected Slider HealthbarInstance;
    private bool isReadyToShoot = true;

    //Variables HandlingHealthbar()
    private float xResolution;
    private float yResolution;
    protected Vector3 offsetBarPosition;
    protected float maxHealthPoint; 


    private void Awake()
    {
        xResolution = GameObject.Find("Canvas").GetComponent<RectTransform>().rect.width;
        yResolution = GameObject.Find("Canvas").GetComponent<RectTransform>().rect.height;
    }
    private void Start()
    {
        maxHealthPoint = healthPoint;
    }

    private void Update()
    {
        Behaviour();
        HandlingHealthbar();
    }

    public void TakeDamage(int damage)
    {
        healthPoint -= damage;
        if (healthPoint <= 0)
        {
            Destroy(HealthbarInstance.gameObject);
            Destroy(gameObject);
        }
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

    protected abstract void Behaviour();


    protected virtual void HandlingHealthbar()
    {
        if(HealthbarInstance == null)
        {
            HealthbarInstance = Instantiate(HealthbarSliderPrefab, GameObject.Find("Canvas").transform);
        }
        Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform.position + offsetBarPosition);
        HealthbarInstance.gameObject.transform.position = new Vector3(screenPosition.x * xResolution, screenPosition.y * yResolution, 0);
        HealthbarInstance.value = healthPoint / maxHealthPoint;
        
    }

    IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(1 / speedFire);
        isReadyToShoot = true;
    }
}
