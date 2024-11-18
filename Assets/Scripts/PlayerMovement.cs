using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [SerializeField] float mineRange = 1f;

    Vector2 movement;

    // Update is called once per frame

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            moveSpeed *= 1.5f;
        else if(Input.GetKeyUp(KeyCode.LeftShift))
            moveSpeed /= 1.5f;


        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attacked");
            //TryMineSomething();
        }
        movement.Normalize();

        if (animator != null)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

    }

    public void TryMineSomething()//Animation Event
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, mineRange);
        foreach (var hitCollider in hitColliders)
        {
            IMineable mineable = hitCollider.GetComponent<IMineable>();
            if (mineable != null)
            {
                mineable.Mine();
                return;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, mineRange);
    }
}
