using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 250f;

    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D box;
    public float moveX;
    public float jumpSpeed = 12f;
    float jumpVal = 0;
    public movingPlatform platform = null;

    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveX = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        Vector2 movement = new Vector2(moveX, body.velocity.y);
        body.velocity = movement;

        animator.SetFloat("speed", Mathf.Abs(moveX));
        
        if (!Mathf.Approximately(moveX, 0f))
        {
            transform.localScale = new Vector3(Mathf.Sign(moveX), 1, 1);
        }
    }
    private void Update()
    {
        //checks if on the ground and applies jump 
        box = GetComponent<BoxCollider2D>();
        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - 0.1f);
        Vector2 corner2 = new Vector2(max.x, min.y - 0.2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);//crates points under the player 

        bool grounded = false;//sets grounded value based on collision 
        if (hit != null)
        {
            grounded = true;
            jumpVal = 0;
            body.gravityScale = 0;
        }
        else
        {
            grounded = false;
            body.gravityScale = 1;
        }
        //body.gravityScale = grounded && moveX == 0 ? 0 : 1;//if im not moving and am gorunded turns off gravity 
        if (grounded && Input.GetKeyDown(KeyCode.Space) || jumpVal <= 2f && Input.GetKeyDown(KeyCode.Space))
        {
            jumpVal++;
            if (jumpVal > 0)
            {
                body.AddForce(Vector2.up * jumpSpeed * 0.3f, ForceMode2D.Impulse);
            }
            body.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }//jumps when u press space 

        //maks player child of the platform so it moves with it 
        if (hit != null)
        {
            platform = hit.GetComponent<movingPlatform>();
        }
        if (platform != null)
        {
            transform.parent = platform.transform;
        }
        else
        {
            transform.parent = null;
        }

        Vector3 pScale = Vector3.one;
        if (platform != null)
        {
            pScale = platform.transform.localScale;
        }
        if (moveX != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveX) / pScale.x, 1 / pScale.y, 1);
        }//sets the scale of the player when on a moving platform 
    }
}
