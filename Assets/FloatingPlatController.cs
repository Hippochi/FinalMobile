// FLOATINGPLATCONTROLLER  ALEX DINE 101264627 DECEMBER 17th 2021 MAKE PLATFORM FLOAT and shrink


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloatingPlatController : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public bool playerActive;
    public float platformTimer;
    public float threshold;
    public float scaleChange;

    public PlayerBehaviour player;

    private Vector3 distance;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();

        platformTimer = 0;
        playerActive = false;
        distance = end.position - start.position;
        scaleChange = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(Vector3.Distance(transform.position, end.position) < threshold))
        {
            platformTimer += Time.deltaTime;
            _Float();
        }

        if (playerActive == true)
        {
            _Shrink();
        }
        else
        {
            _Grow();
        }
    }

    private void _Float()
    {
        var distanceX = (distance.x > 0) ? start.position.x + Mathf.PingPong(platformTimer, distance.x) : start.position.x;
        var distanceY = (distance.y > 0) ? start.position.y + Mathf.PingPong(platformTimer, distance.y) : start.position.y;

        transform.position = new Vector3(distanceX, distanceY, 0.0f);
    }

    private void _Shrink()
    {
        var curScale = transform.localScale.x - scaleChange * Time.deltaTime;
        if (curScale >= 0.01)
        {
            transform.localScale = new Vector3(curScale, curScale, 1f);
            
        }
    }

    private void _Grow()
    {
        var curScale = transform.localScale.x + scaleChange * Time.deltaTime;
        if (curScale <= 1)
        {
            transform.localScale = new Vector3(curScale, curScale, 1f);
        }
    }

    public void Reset()
    {
        transform.position = start.position;
        platformTimer = 0;
    }
}