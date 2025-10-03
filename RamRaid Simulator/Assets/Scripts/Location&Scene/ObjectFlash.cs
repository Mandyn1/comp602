using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFlash : MonoBehaviour
{

    public GameObject targetObject;

    public float repeatTime = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("ChangeActiveState", repeatTime, repeatTime);
    }

    void ChangeActiveState()
    {
        targetObject.SetActive(!targetObject.activeInHierarchy);
    }
}
