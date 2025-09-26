using UnityEngine;

public class PointCounter : MonoBehaviour
{

    //instance varibles
    public int score;
    public GameObject ValueObj;
    private LocationValue value;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //initialize the score and get the value for the location
        score = 0;
        value = ValueObj.GetComponent<LocationValue>();
    }

    public void Increment()
    {
        //increment the points for the player
        score += value.pointsPerItem;

        //log output
        Debug.Log("counter incremented to: "+score);
    }

    public void Decrement()
    {
        //derecrement the points
        score -= value.pointsPerItem;

        //log output
        Debug.Log("counter decremented to: "+score);
    }

    void Update()
    {

    }
}
