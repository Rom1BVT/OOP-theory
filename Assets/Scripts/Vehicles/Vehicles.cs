using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Vehicles : MonoBehaviour
{
    // INHERITANCE - Caracteristics of vehicles
    protected float healthPoint;
    protected float speedFire;
    protected int pointValue;
    private bool isReadyToShoot = true;

    [SerializeField] protected Transform shotOrigin;
    [SerializeField] protected GameObject ammoPrefab;
    [SerializeField] protected Slider HealthbarSliderPrefab;
    protected Slider HealthbarInstance;
    protected GameManager gameManager;

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

    public void TakeDamage(int damage) //INHERITANCE
    {
        healthPoint -= damage;
        if (healthPoint <= 0)
        {
            gameManager.Score += pointValue;
            Destroy(HealthbarInstance.gameObject);
            Destroy(gameObject);
        }
    }
    protected void Shoot(GameObject ammoType, Transform origin) //POLYMORPHISM
    {
        if (isReadyToShoot && !gameManager.isGameOver)
        {
            Instantiate(ammoType, origin.position, origin.rotation);
            isReadyToShoot = false;
            StartCoroutine(ShootCooldown());
        }
    }

    protected void Shoot(GameObject ammoType, Transform origin, float spreadRadius) //POLYMORPHISM
    {      
        float randomSpreadRange = Random.Range(-spreadRadius, spreadRadius);
        origin.Rotate(0, randomSpreadRange, 0);

        if (isReadyToShoot && !gameManager.isGameOver)
        {
            Instantiate(ammoType, origin.position, origin.rotation);
            isReadyToShoot = false;
            StartCoroutine(ShootCooldown());
        }
        origin.rotation = new Quaternion(0, 0, 0, 0);
    }

    protected void Shoot(GameObject ammoType, Transform origin, Vector3 direction) //POLYMORPHISM
    {
        if (isReadyToShoot && !gameManager.isGameOver)
        {
            var ammoInstance = Instantiate(ammoType, origin.position, origin.rotation);
            ammoInstance.transform.rotation = Quaternion.FromToRotation(Vector3.forward, direction - transform.position);
            isReadyToShoot = false;
            StartCoroutine(ShootCooldown());
        }
    }

    protected abstract void Behaviour(); //POLYMORPHISM


    protected virtual void HandlingHealthbar() //INHERITANCE
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

    protected virtual void ComeOnStage() //INHERITANCE
    {
        //called every frame 
        transform.Translate(-Vector3.forward * Time.deltaTime * zUnits / entryDelay);
        if (entryTimeLeft < Time.time)
        {
            isOnStage = true;
        }
    }

    protected Vector3 FindPlayerPosition() //INHERITANCE
    {
        
        if (gameManager.isGameOver == false)
        {
            return GameObject.Find("Player").transform.position;
        }
        else
        {
            return Vector3.zero;
        }
        
    }
}
