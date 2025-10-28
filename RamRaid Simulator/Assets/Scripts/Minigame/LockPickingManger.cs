using TMPro;
using Unity.AppUI.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Splines.ExtrusionShapes;
using UnityEngine.UI;

public class LockPickingManger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("UI Components")]
    public UnityEngine.Canvas thisMinigame;
    public Slider stressBar;
    public Image cylinder;
    public Image pick;
    public Image indicator;
    public UnityEngine.UI.Button unlkButton;
    public UnityEngine.UI.Button failButton;
    public TextMeshProUGUI pickStatus;
    public TextMeshProUGUI successMess;
    public TextMeshProUGUI failMess;

    //PICKING MENCHANICS VALUIES 
    //(set as public for getting correct feel)

    //slider
    [Header("Stress Values")]
    public float maxStress;
    public float stressRegen;
    public float stressInc;
    private float currentStress;

    //pick
    [Header("Pick Values")]
    public float picRotateSpeed;
    public float picReturnSpeed;
    private float picCurrentAngle;
    private Quaternion picOriginalRotation;
    private bool isBreaking = false;

    //cylinder
    [Header("Cylinder Values")]
    public float cylRotateSpeed;
    public float cylReturnSpeed;
    private float cylCurrentAngle;
    private Quaternion cylOriginalRotation;

    //picking
    [Header("Picking Mechanic Values")]
    private float correctAngle;
    public float play;
    public float closePlay;
    public bool hasPicked = false;
    public bool hasFailed = false;
    public int numPicks;


    void Start()
    {
        //hide unlock buttons and add action listerner
        unlkButton.gameObject.SetActive(false);
        unlkButton.interactable = false;
        unlkButton.onClick.AddListener(buttonClickSuccessfull);
        successMess.gameObject.SetActive(false);

        failButton.gameObject.SetActive(false);
        failButton.interactable = false;
        failButton.onClick.AddListener(buttonClickFail);
        failMess.gameObject.SetActive(false);
        //set default status
        pickStatus.text = "Current Picks Remaining: " + numPicks.ToString();
        //indicator defualt
        indicator.color = Color.red;

        //stress bar values
        stressBar.maxValue = maxStress;
        stressBar.minValue = 0f;
        stressBar.interactable = false;

        //store the pics original rotation position to rotate it back to the centre
        picOriginalRotation = pick.rectTransform.rotation;


        //store cylinders original rotation also
        cylOriginalRotation = cylinder.rectTransform.rotation;

        //randomly generate the correct angle
        correctAngle = (float)Random.Range(15, 75); //max angle is 80 so generate a fair amout in between

        //burtto
        //----------------------------------------------------------------------------------------------

    }
    // Update is called once per frame
    void Update()
    {
        //check if updqtes should continue (if num picks is 0 or less or has picked is true)
        if (hasPicked || numPicks <= 0)
        {
            return;
        }

        //ROTATION UPDATES
        //current rotaion angles
        cylCurrentAngle = cylRotateSpeed * Time.deltaTime; //cylinder
        picCurrentAngle = picRotateSpeed * Time.deltaTime; //pic
        //STRESS BAR
        //----------------------------------------------------------------------------------------------
        //slowly release the stress if user is not applying space
        if (!Input.GetKey(KeyCode.Space))
        {
            //regen the stress if above 0
            if(currentStress > 0)
            {
                currentStress -= stressRegen * Time.deltaTime; //use delta time to slowly decrease stress
            }

            //use quaternion and rotate towards the original saved position
            pick.rectTransform.rotation = Quaternion.RotateTowards(
            pick.rectTransform.rotation, picOriginalRotation, picReturnSpeed * Time.deltaTime);

        }
        //user is trying to open lock
        if (Input.GetKey(KeyCode.Space))
        {
            //check if pick is at max stress
            if (currentStress >= maxStress && !isBreaking)
            {
                currentStress = maxStress;
                //breaks
                isBreaking = true;
                PickBreak();
            }
            else if (currentStress < maxStress)
            {
                //increment stress and increase slider
                currentStress += stressInc * Time.deltaTime;
            }

            //limit the picks rotation to 50 degrees, first get the distance from the original rotation using its current rotation then check
            float picAngleFromOriginal = Quaternion.Angle(pick.rectTransform.rotation, picOriginalRotation);
            if (picAngleFromOriginal <= 100f) //like 50 degrees
            {
                pick.rectTransform.Rotate(0f, 0f, -picCurrentAngle); //rotate pic
            }

        }
        else
        {
            isBreaking = false;
        }
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
            if (cylAngleFromOriginal <= 85f) //closest to 45 degrees
            {
                cylinder.rectTransform.Rotate(0f, 0f, -cylCurrentAngle); //rotate cyliner
            }

        }
        //----------------------------------------------------------------------------------------------

        //PICKING
        //----------------------------------------------------------------------------------------------
        //get the values of the pick and cylinder
        float cylAngle = Quaternion.Angle(cylinder.rectTransform.rotation, cylOriginalRotation);
        if (cylAngle >= (correctAngle - play) && cylAngle <= (correctAngle + play))
        {
            //Udate Color indicator
            indicator.color = Color.green;


            //if user picks lock at correct angle
            float picAngle = Quaternion.Angle(pick.rectTransform.rotation, picOriginalRotation);
            if (picAngle >= (100f - play))
            {
                PickSucessful();
            }
        }
        else if (cylAngle >= (correctAngle - closePlay) && cylAngle <= (correctAngle + closePlay))
        {
            //user is close to correct angle, update to yellow
            indicator.color = Color.yellow;
        }
        else
        {
            //red, user is far away
            indicator.color = Color.red;
        }
        //----------------------------------------------------------------------------------------------
    }

    private void PickBreak()
    {
        //guard to stop func being called inf
        if (hasPicked || numPicks <= 0)
        {
            return;
        }

        //return to original position (like adding a new pick)
        pick.rectTransform.rotation = picOriginalRotation;
        //decrement number of picks remaining and reset stress counter
        numPicks--;
        currentStress = 0;
        //update text
        pickStatus.text = "Current Picks Remaining: " + numPicks.ToString();

        if (numPicks == 0)
        {
            stressBar.gameObject.SetActive(false);
            cylinder.gameObject.SetActive(false);
            pick.gameObject.SetActive(false);
            indicator.gameObject.SetActive(false);
            pickStatus.gameObject.SetActive(false);

            //button
            failButton.gameObject.SetActive(true);
            failButton.interactable = true;
            failMess.gameObject.SetActive(true);
        }
    }

    private void PickSucessful()
    {
        //hide everything and show unlock button
        stressBar.gameObject.SetActive(false);
        cylinder.gameObject.SetActive(false);
        pick.gameObject.SetActive(false);
        indicator.gameObject.SetActive(false);
        pickStatus.gameObject.SetActive(false);

        //button
        unlkButton.gameObject.SetActive(true);
        unlkButton.interactable = true;
        successMess.gameObject.SetActive(true);
    }

    void buttonClickFail()
    {
        //update status
        hasFailed = true;
        //set entire object as inactive
        thisMinigame.gameObject.SetActive(false);
    }

    void buttonClickSuccessfull()
    {
        //update the status
        hasPicked = true;

        //set entire object as inactive
        thisMinigame.gameObject.SetActive(false);
    }
}


