using UnityEngine;
using System.Collections.Generic;

public class PoliceAlert : MonoBehaviour
{
    public float sirenRadius = 30f;      // siren range

    public AudioSource sirenSource;
    public AudioClip sirenClip;

    private List<Transform> raiders = new List<Transform>();
    private List<Transform> policeUnits = new List<Transform>();
    private bool sirenPlaying = false;

    void Start()
    {
        //find all raider stages inside gameloop
        foreach (Transform child in GameObject.Find("GameLoop").transform)
        {
            if (child.name.StartsWith("Raider_"))
                raiders.Add(child);
            if (child.name.StartsWith("Police_"))
                policeUnits.Add(child);
        }

        if (sirenSource == null)
            sirenSource = gameObject.AddComponent<AudioSource>();

        sirenSource.clip = sirenClip;
        sirenSource.loop = true;
        sirenSource.playOnAwake = false;
    }

    void Update()
    {
        bool closeEnough = false;

        foreach (Transform raider in raiders)
        {
            foreach (Transform police in policeUnits)
            {
                float distance = Vector3.Distance(raider.position, police.position);
                if (distance <= sirenRadius)
                    closeEnough = true;
            }
        }

        if (closeEnough && !sirenPlaying)
        {
            sirenSource.Play();
            sirenPlaying = true;
        }
        else if (!closeEnough && sirenPlaying)
        {
            sirenSource.Stop();
            sirenPlaying = false;
        }
    }
}
