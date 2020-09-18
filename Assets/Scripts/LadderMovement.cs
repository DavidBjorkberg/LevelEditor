using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{

    internal bool Onladder;
    Character movement;
    Ladder ladder;
    Rigidbody2D rb;

    void Start()
    {
        movement = GetComponent<Character>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GetComponent<Character>().hasControl)
        {
            Laddermovement();
        }
    }
    void Laddermovement()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, 8, Onladder);
        if (Onladder)
        {
            if (Input.GetAxis(movement.allowedinput + "Ver") > 0)
            {

                transform.Translate(Vector2.up * Input.GetAxis(movement.allowedinput + "Ver") * Time.deltaTime * movement.movementSpeed * 0.7f);
                if (ladder.TopCheck.y <= movement.bottom.transform.position.y)
                {
                    LeaveLadder();
                }
            }          
            if (Input.GetAxis(movement.allowedinput + "Ver") < 0 && ladder.BottomCheck.y <= movement.bottom.transform.position.y)
            {
                transform.Translate(Vector2.up * Input.GetAxis(movement.allowedinput + "Ver") * Time.deltaTime * movement.movementSpeed * 0.7f);
            }

            if (Input.GetAxis(movement.allowedinput + "Hor") > 0)
            {

                transform.Translate(Vector2.right * Input.GetAxis(movement.allowedinput + "Hor") * Time.deltaTime * movement.movementSpeed / 2 * 0.5f);
                RaycastHit2D DownLadderCheck = Physics2D.Raycast(transform.position, Vector2.down, 1, 1 << 22);
                if (!DownLadderCheck)
                {
                    LeaveLadder();
                }
            }
            if (Input.GetAxis(movement.allowedinput + "Hor") < 0)
            {
                transform.Translate(Vector2.right * Input.GetAxis(movement.allowedinput + "Hor") * Time.deltaTime * movement.movementSpeed / 2 * 0.5f);
                RaycastHit2D DownLadderCheck = Physics2D.Raycast(transform.position, Vector2.down, 1, 1 << 22);

                if (!DownLadderCheck)
                {
                    LeaveLadder();
                }
            }


            if (ladder.BottomCheck.y + 0.2f >= movement.bottom.transform.position.y)
            {
                ladder = null; 
                LeaveLadder();

            }
            if (Input.GetButtonDown(movement.allowedinput + "Jump"))
            {
                LadderJump();
            }


        }
        else if (rb.velocity.x == 0)
        {
            if (Input.GetAxis(movement.allowedinput + "Ver") > 0)
            {
                RaycastHit2D UpLadderCheck = Physics2D.Raycast(transform.position, Vector2.up, 1, 1 << 22);
                if (UpLadderCheck)
                {
                    Onladder = true;
                    rb.velocity = Vector2.zero;
                    ladder = UpLadderCheck.collider.GetComponent<Ladder>();
                    rb.bodyType = RigidbodyType2D.Kinematic;
                }
            }
            RaycastHit2D DownLadderCheck = Physics2D.Raycast(transform.position, Vector2.down, 1, 1 << 22);

            if (Input.GetAxis(movement.allowedinput + "Ver") < 0)
            {
                if (DownLadderCheck && DownLadderCheck.collider.GetComponent<Ladder>().BottomCheck.y + 0.2f <= movement.bottom.transform.position.y)
                {
                    Onladder = true;
                    rb.velocity = Vector2.zero;
                    ladder = DownLadderCheck.collider.GetComponent<Ladder>();
                    rb.bodyType = RigidbodyType2D.Kinematic;
                }
            }
        }
    }
    void LadderJump()
    {

        if (Input.GetAxis(movement.allowedinput + "Hor") > 0)
        {
            LeaveLadder();
            rb.AddForce(new Vector2(2f, 1));

        }
        if (Input.GetAxis(movement.allowedinput + "Hor") < 0)
        {
            LeaveLadder();
            rb.AddForce(new Vector2(-2f, 1));
        }

    }
    void LeaveLadder()
    {
        Onladder = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
