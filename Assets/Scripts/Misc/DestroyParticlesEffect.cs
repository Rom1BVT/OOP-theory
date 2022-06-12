using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticlesEffect : MonoBehaviour
{
    private ParticleSystem particle;
    private GameObject cam;
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        cam = GameObject.Find("Main Camera");
        cam.GetComponent<ScreenShake>().PlayScreenShake();
    }

    // Update is called once per frame
    void Update()
    {
        if (!particle.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
