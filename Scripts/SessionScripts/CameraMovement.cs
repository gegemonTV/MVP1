using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private bool rightMove, leftMove, upMove, downMove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 nextPos = camera.transform.position;
        if (rightMove)
        {
            nextPos.x += 5 * Time.deltaTime;
        }
        if (leftMove)
        {
            nextPos.x -= 5 * Time.deltaTime;
        }
        if (upMove)
        {
            nextPos.z += 5 * Time.deltaTime;
        }
        if (downMove)
        {
            nextPos.z -= 5 * Time.deltaTime;
        }
        camera.transform.position = nextPos;
    }

    public void SetRight(bool b)
    {
        rightMove = b;
    }

    public void SetLeft(bool b)
    {
        leftMove = b;
    }

    public void SetUp(bool b)
    {
        upMove = b;
    }

    public void SetDown(bool b)
    {
        downMove = b;
    }
}
