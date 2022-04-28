using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Vehicles : MonoBehaviour
{
    // INHERITED - Caracteristics of vehicles
    protected float healthPoint;
    protected float speedFire;
    protected int pointValue;
    private bool isReadyToShoot = true;

    [SerializeField] protected Transform shotOrigin;
    [SerializeField] protected GameObject ammoPrefab;
    [SerializeField] protected Slider HealthbarSliderPrefab;
    protected Slider HealthbarInstance;

    //used in scene entry
    private IEnumerator entryCoroutine;
    protected bool isOnStage = false;
    private float entryDelay = 2.0f;
    private float entryTimeLeft;
    private float zUnits = 9.0f;


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
        entryCoroutine = EntryRoutine();
        StartCoroutine(entryCoroutine);
    }

    private void Update()
    {
        if (isOnStage) { Behaviour(); } // the vehicle can't shoot or move before his entry is  not finished
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
        if (isReadyToShoot)
        {
            var ammoInstance = Instantiate(ammoType, origin.position, origin.rotation);
            ammoInstance.transform.rotation = Quaternion.FromToRotation(Vector3.forward, direction - transform.position);
            isReadyToShoot = false;
            StartCoroutine(ShootCooldown());
        }
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

    IEnumerator EntryRoutine()
    {
        
        entryTimeLeft = Time.time + entryDelay;
        while (true)
        {
            ComeOnStage();
            if (isOnStage) { StopCoroutine(entryCoroutine); }
            yield return null;
        }
    }

    protected virtual void ComeOnStage()
    {
        //called every frame 
        transform.Translate(-Vector3.forward * Time.deltaTime * zUnits / entryDelay);
        if (entryTimeLeft < Time.time)
        {
            isOnStage = true;
        }
    }
}
