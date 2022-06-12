using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private int numberOfShakes = 10;
    private float shakeAmplitude = 10f;
    private float shakeDuration = 0.25f;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }


    public void PlayScreenShake()
    {
        StartCoroutine(TranslateToRandomVector());
    }

    IEnumerator TranslateToRandomVector()
    {
        for (int i = 0; i < numberOfShakes; i++)
        {
            //Créer un Vector2D random
            Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0).normalized;
            //Déplacer la caméra depuis le centre dans la direction de ce vecteur sur x unités (shakeAmplitude) pendant x sec
            float movingTimeTerm = Time.time + (shakeDuration / numberOfShakes / 2);
            while (Time.time < movingTimeTerm)
            {
                transform.Translate(randomDirection * Time.deltaTime * shakeAmplitude); //1u par secondes * shakeAmplitude
                yield return null;
            }
            //Déplacer la caméra depuis l'extrémité en direction du centre
            movingTimeTerm = Time.time + (shakeDuration / numberOfShakes / 2);
            while (Time.time < movingTimeTerm)
            {
                transform.Translate(-randomDirection * Time.deltaTime * shakeAmplitude); //1u par secondes
                yield return null;
            }

            //remettre la caméra dans sa position d'origine
            transform.position = initialPosition;
        }
    }
}
