using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private LayerMask jumpableGround;

    public float Health = 100;

    private bool walking = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (math.abs(dirX) > 0.1f && !walking)
        {
            anim.SetTrigger("MoveTrigger");
            walking = true;
        } else if (math.abs(dirX) < 0.1f && walking)
        {
            anim.SetTrigger("IdleTrigger");
            walking = false;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetTrigger("JumpTrigger");
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    public void DoDamage(float damage)
    {
        Health -= damage;

        if (Health < 0)
        {
            Health = 0;
        }
    }

    public void Kill()
    {
        Health = 0;
        ScoreManager.instance.RemoveLife();
        GameManager.instance.RespawnIfPossible();
        Destroy(gameObject);
    }
}
