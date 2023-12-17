using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public Vector3 finishpos = Vector3.zero;
    public float speed = 0.5f;

    private Vector3 startpos;
    private float percent = 0f;
    private int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        percent += direction * speed * Time.deltaTime;
        float x = (finishpos.x - startpos.x) * percent + startpos.x;
        float y = (finishpos.y - startpos.y) * percent + startpos.y;
        transform.position = new Vector3(x, y, startpos.z);

        if ((direction == 1 && percent > .9f) || (direction == -1 && percent < .1f))
        {
            direction *= -1;
        }
    }
}
