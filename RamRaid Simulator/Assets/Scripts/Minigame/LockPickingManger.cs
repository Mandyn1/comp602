using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Splines.ExtrusionShapes;
using UnityEngine.UI;

public class LockPickingManger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Slider stressBar;
    public Image cylinder;
    public Image pick;

    //PICKING MENCHANICS VALUIES 
    //(set as public for getting correct feel)


    //slider
    public float maxStress;
    public float stressRegen;
    public float stressInc;
    private float currentStress;

    //pick
    public float picRotateSpeed;
    public float picReturnSpeed;
    private float picCurrentAngle;
    private Quaternion picOriginalRotation;

    //cylinder
    public float cylRotateSpeed;
    public float cylReturnSpeed;
    private float cylCurrentAngle;
    private Quaternion cylOriginalRotation;


    void Start()
    {

        //stress bar values
        stressBar.maxValue = maxStress;
        stressBar.minValue = 0f;
        stressBar.interactable = false;

        //store the pics original rotation position to rotate it back to the centre
        picOriginalRotation = pick.rectTransform.rotation;


        //store cylinders original rotation also
        cylOriginalRotation = cylinder.rectTransform.rotation;


    }
    // Update is called once per frame
    void Update()
    {
        //ROTATION UPDATES
        //current rotaion angles
        cylCurrentAngle = cylRotateSpeed * Time.deltaTime; //cylinder
        picCurrentAngle = picRotateSpeed * Time.deltaTime; //pic
        //STRESS BAR
        //----------------------------------------------------------------------------------------------
        //slowly release the stress if user is not applying space
        if (!Input.GetKey(KeyCode.Space))
        {
            currentStress -= stressRegen * Time.deltaTime; //use delta time to slowly decrease stress

            //use quaternion and rotate towards the original saved position
            pick.rectTransform.rotation = Quaternion.RotateTowards(
            pick.rectTransform.rotation, picOriginalRotation, picReturnSpeed * Time.deltaTime);

        }
        //user is trying to open lock
        if (Input.GetKey(KeyCode.Space))
        {
            if (currentStress >= maxStress)
            {
                //breaks
            }

            //increment stress and increase slider
            currentStress += stressInc;

            //limit the picks rotation to 50 degrees, first get the distance from the original rotation using its current rotation then check
            float picAngleFromOriginal = Quaternion.Angle(pick.rectTransform.rotation, picOriginalRotation);
            if (picAngleFromOriginal < 100f) //like 50 degrees
            {
                pick.rectTransform.Rotate(0f, 0f, -picCurrentAngle); //rotate pic
            }

        }

        //restrict the stress between 0 and max stress 
        currentStress = Mathf.Clamp(currentStress, 0f, maxStress);

        //update the bar
        stressBar.value = currentStress;
        //----------------------------------------------------------------------------------------------

        //CYLINDER
        //----------------------------------------------------------------------------------------------
        //user is not rotating cylinder
        if (!Input.GetKey(KeyCode.LeftArrow))
        {
            //rotate the cylinder back to its original position
            cylinder.rectTransform.rotation = Quaternion.RotateTowards(
            cylinder.rectTransform.rotation, cylOriginalRotation, cylReturnSpeed * Time.deltaTime);
        }

        //user is rotating cyliner
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //limit the cyl rotation to 45 degrees, first get the distance from the original rotation using its current rotation then check
            float cylAngleFromOriginal = Quaternion.Angle(cylinder.rectTransform.rotation, cylOriginalRotation);
            if (cylAngleFromOriginal < 85f) //closest to 45 degrees
            {
                cylinder.rectTransform.Rotate(0f, 0f, -cylCurrentAngle); //rotate cyliner
            }
            
        }

        //----------------------------------------------------------------------------------------------
    }
}


