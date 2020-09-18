using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{
    public Sprite[] Sprites;
    [Space(15), Header("Has to be higher than lowest active time")]
    public float UnclickableTime;
    [System.Serializable]
    public class StringEvent : UnityEvent<float> { }
    public StringEvent[] OnButtonPress;
    bool activated;
    SpriteRenderer spriterenderer;
    internal bool adding;
    [HideInInspector]
    public float SlowDuration;
    [HideInInspector]
    public float SpeedUpDuration;
    private void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        spriterenderer.sprite = activated ? Sprites[1] : Sprites[0];
        if (!activated)
        {
            RaycastHit2D ButtonCheck = Physics2D.BoxCast(transform.position
                , new Vector2(spriterenderer.bounds.size.x, spriterenderer.bounds.size.y)
                , 0, Vector2.up
                , 0.01f
                , ~1 << 8);
            if (ButtonCheck && ButtonCheck.collider.GetComponent<Rigidbody2D>() != null
                    && !ButtonCheck.transform.GetComponent<PlayerBullet>())
            {
                    Rigidbody2D player = ButtonCheck.collider.GetComponent<Rigidbody2D>();
                    if (player.velocity.y <= 0)
                    {
                        ActivateButton();
                    }
            }
        }
    }
    void ActivateButton()
    {
        for (int i = 0; i < OnButtonPress.Length; i++)
        {
            OnButtonPress[i].Invoke(0);
        }
        GetComponent<BoxCollider2D>().enabled = false;
        activated = true;
        Invoke("DeactivateButton", UnclickableTime);
    }
    void DeactivateButton()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        activated = false;
    }
    public void StartAdding()
    {
        adding = true;
    }
    public void StopAdding()
    {
        adding = false;
    }
    public void SlowTime(float Duration)
    {
        Time.timeScale = Mathf.Clamp(Time.timeScale / 2, 0.5f, 2);
        Invoke("SlowTimeExpire", Duration);
    }
    public void SpeedUpTime(float Duration)
    {
        Time.timeScale = Mathf.Clamp(Time.timeScale * 2, 0.5f, 2);
        Invoke("SpeedUpExpire", Duration);
    }
    void SlowTimeExpire()
    {
        if (Time.timeScale < 1)
        {
            Time.timeScale *= 2;
        }
    }
    void SpeedUpExpire()
    {
        if (Time.timeScale > 1)
        {
            Time.timeScale = Time.timeScale / 2;
        }
    }
}
