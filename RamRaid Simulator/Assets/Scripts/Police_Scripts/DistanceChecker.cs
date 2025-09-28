using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;

public class DistanceChecker : MonoBehaviour
{
    public GameObject location1;
    public GameObject location2;
    public GameObject location3;
    public GameObject location4;

    public GameObject location1Closest;
    public GameObject location2Closest;
    public GameObject location3Closest;
    public GameObject location4Closest;

    public double location1Time = 9999;
    public double location2Time = 9999;
    public double location3Time = 9999;
    public double location4Time = 9999;

    public double timeModifier = 2.5;

    public void CalculateDistances()
    {
        List<GameObject> carArray = this.gameObject.GetComponent<PolicePlacementManager>().placedUnits;

        double distance, calcX, calcY, calcZ;

        foreach (GameObject car in carArray)
        {
            // location 1
            calcX = math.square(car.GetComponent<Transform>().position.x - location1.GetComponent<Transform>().position.x);
            calcY = math.square(car.GetComponent<Transform>().position.y - location1.GetComponent<Transform>().position.y);
            calcZ = math.square(car.GetComponent<Transform>().position.z - location1.GetComponent<Transform>().position.z);
            distance = math.sqrt(calcX + calcY + calcZ) * timeModifier;
            if (distance < location1Time)
            {
                location1Time = distance;
                location1Closest = car;
            }

            // location 2
            calcX = math.square(car.GetComponent<Transform>().position.x - location2.GetComponent<Transform>().position.x);
            calcY = math.square(car.GetComponent<Transform>().position.y - location2.GetComponent<Transform>().position.y);
            calcZ = math.square(car.GetComponent<Transform>().position.z - location2.GetComponent<Transform>().position.z);
            distance = math.sqrt(calcX + calcY + calcZ) * timeModifier;
            if (distance < location2Time)
            {
                location2Time = distance;
                location2Closest = car;
            }

            // location 3
            calcX = math.square(car.GetComponent<Transform>().position.x - location3.GetComponent<Transform>().position.x);
            calcY = math.square(car.GetComponent<Transform>().position.y - location3.GetComponent<Transform>().position.y);
            calcZ = math.square(car.GetComponent<Transform>().position.z - location3.GetComponent<Transform>().position.z);
            distance = math.sqrt(calcX + calcY + calcZ) * timeModifier;
            if (distance < location3Time)
            {
                location3Time = distance;
                location3Closest = car;
            }

            // location 4
            calcX = math.square(car.GetComponent<Transform>().position.x - location4.GetComponent<Transform>().position.x);
            calcY = math.square(car.GetComponent<Transform>().position.y - location4.GetComponent<Transform>().position.y);
            calcZ = math.square(car.GetComponent<Transform>().position.z - location4.GetComponent<Transform>().position.z);
            distance = math.sqrt(calcX + calcY + calcZ) * timeModifier;
            if (distance < location4Time)
            {
                location4Time = distance;
                location4Closest = car;
            }
        }
    }
}
