using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class character_picker : MonoBehaviour
{
    internal string allowedinput;
    public int MovementSpeed;
    public int JumpHeight;
    Rigidbody2D rb;
    internal int PlayerNR;
    internal TextMesh ReadyText;
    Vector3 Bottom;
    float HitboxWidth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ReadyText = GetComponentInChildren<TextMesh>();
        HitboxWidth = GetComponent<BoxCollider2D>().size.x;
    }

    void Update()
    {
        movement();
        CharacterPick();
        if (GameManager.characters[PlayerNR] != null)
        {
            if (Input.GetButtonDown($"{allowedinput}Ability"))
            {
                if (!CharacterSelect.ReadyPlayers.Contains(name))
                {
                    CharacterSelect.ReadyPlayers.Add(name);
                }
                else
                {
                    CharacterSelect.ReadyPlayers.Remove(name);
                }
            }
            ReadyText.text = CharacterSelect.ReadyPlayers.Contains(name) ? "X to unready" : "X to ready";         
        }
        if (CharacterSelect.ReadyPlayers.Count == CharacterSelect.Connectedplayers.Count)
        {
            if (Input.GetButtonDown($"{allowedinput}Start"))
            {
                //int scene = GameManager.SceneList[Random.Range(0, GameManager.SceneList.Count)];
                string scene = "Level 1";
                //GameManager.SceneList.Remove(scene);
                SceneManager.LoadScene(scene);
            }
        }

    }
    void CharacterPick()
    {

        RaycastHit2D LeftCheck = Physics2D.Raycast(transform.position, Vector2.left, 1, 1 << 9);
        RaycastHit2D RightCheck = Physics2D.Raycast(transform.position, Vector2.right, 1, 1 << 9);
        if (LeftCheck)
        {
            if (Input.GetButtonDown($"{allowedinput}Pickup"))
            {
                GameManager.characters[PlayerNR] = LeftCheck.collider.GetComponent<character>().Character;
                GetComponent<SpriteRenderer>().sprite = LeftCheck.collider.GetComponent<SpriteRenderer>().sprite;

            }
        }
        else if (RightCheck)
        {
            if (Input.GetButtonDown($"{allowedinput}Pickup"))
            {
                GameManager.characters[PlayerNR] = RightCheck.collider.GetComponent<character>().Character;
                GetComponent<SpriteRenderer>().sprite = RightCheck.collider.GetComponent<SpriteRenderer>().sprite;
            }
        }
    }
    void movement()
    {
        if (rb.velocity.x < 6)
        {
            //Right
            if (Input.GetAxis($"{allowedinput}Hor") > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;

                RaycastHit2D WalkCheck = Physics2D.BoxCast(transform.position, new Vector2(0.25f, 1), 0, Vector2.right, 0.3f, 1 << 8 | 1 << 21);
                if (!WalkCheck)
                {
                    transform.Translate(Vector2.right * Input.GetAxis($"{allowedinput}Hor") * Time.deltaTime * MovementSpeed);
                }
            }
            //Left
            if (Input.GetAxis($"{allowedinput}Hor") < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;

                RaycastHit2D WalkCheck = Physics2D.BoxCast(transform.position, new Vector2(0.25f, 1), 0, Vector2.left, 0.3f, 1 << 8 | 1 << 21);
                if (!WalkCheck)
                {
                    transform.Translate(Vector2.right * Input.GetAxis($"{allowedinput}Hor") * Time.deltaTime * MovementSpeed);
                }
            }
        }   
        if (Input.GetButtonDown($"{allowedinput}Jump"))
        {
            Bottom = transform.Find("Bottom").position;
            RaycastHit2D GroundedCheck;                                                                      // Floor and box layer
            GroundedCheck = Physics2D.BoxCast(Bottom, new Vector2(HitboxWidth, 0.25f), 0, Vector2.down, 0.1f, 1 << 8 | 1 << 19);
            if (GroundedCheck)
            {
                rb.AddForce(Vector2.up * JumpHeight);

            }
        }
    }
}
