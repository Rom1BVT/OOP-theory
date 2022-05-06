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
    private GameManager gameManager;

    //used in scene entry
    private IEnumerator entryCoroutine;
    protected float entryTimeLeft;
    protected bool isOnStage = false;
    protected float entryDelay;
    protected float zUnits;


    //Variables HandlingHealthbar()
    private RectTransform canvasRectTrans;
    protected Vector3 offsetBarPosition;
    protected float maxHealthPoint; 



    private void Awake()
    {

    }
    private void Start()
    {
        canvasRectTrans = GameObject.Find("Canvas").GetComponent<RectTransform>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
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
            gameManager.Score += pointValue;
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
        HealthbarInstance.gameObject.transform.position = new Vector3(screenPosition.x * canvasRectTrans.rect.width * canvasRectTrans.lossyScale.x, screenPosition.y * canvasRectTrans.rect.height * canvasRectTrans.lossyScale.y, 0);
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
