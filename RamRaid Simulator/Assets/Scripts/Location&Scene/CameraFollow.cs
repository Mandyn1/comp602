using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 2f;
    public Transform target;
    public Camera mainCam;

    void LateUpdate()
    {
        if (mainCam == null || target == null)
        {
            //print log thenb exit before attempting to follow
            Debug.Log("cannot follow because missing values");
        }
        Vector3 newPos = new Vector3(target.position.x, target.position.y, -10f);
        mainCam.transform.position = Vector3.Slerp(mainCam.transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
