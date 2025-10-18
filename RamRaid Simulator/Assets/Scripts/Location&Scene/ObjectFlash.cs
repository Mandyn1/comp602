using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFlash : MonoBehaviour
{

    public GameObject targetObject;

    public float repeatTime = 1f;

    // Makes target object flash on screen (enable and disable)
    // Cannot be inside target object lest it disable itself
    void Start()
    {
        InvokeRepeating("ChangeActiveState", repeatTime, repeatTime);
    }

    void ChangeActiveState()
    {
        targetObject.SetActive(!targetObject.activeInHierarchy);
    }
}
