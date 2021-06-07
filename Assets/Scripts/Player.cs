using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStates playerStates;
    [Range(1f, 10f)] public float walkingSpeed;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private CircleCollider2D circle;
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _audioSource_in;
    [SerializeField] private AudioSource _audioSource_out;

    private float _horizontalDirection;
    private Vector2 CurrentSpeed;

    public enum PlayerStates
    {
        Idle, Walking, Stealth
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStates = PlayerStates.Idle;
    }
    private void FixedUpdate()
    {
        _horizontalDirection = Input.GetAxis("Horizontal"); //A-D    
        StateMachine();
        FlipCharacter();
    }
    private void Update()
    {
        if ((playerStates == PlayerStates.Idle || playerStates == PlayerStates.Walking) && Input.GetKeyDown(KeyCode.Space))
        {
            playerStates = PlayerStates.Stealth;
            animator.SetBool("Hide", true);
            _audioSource_in.Play();
        }
        else if (playerStates == PlayerStates.Stealth && Input.GetKeyDown(KeyCode.Space))
        {
            playerStates = PlayerStates.Idle;
            animator.SetBool("Hide", false);
            _audioSource_out.Play();
        }

        if (!_audioSource.isPlaying && Mathf.Abs(_horizontalDirection) > 0.5f)
        {
            _audioSource.Play();
        }
    }
    private void StateMachine()
    {
        switch (playerStates)
        {
            case PlayerStates.Idle:

                circle.enabled = true;
                box.enabled = true;
                rb.gravityScale = 5;

                if (_horizontalDirection != 0)
                {
                    playerStates = PlayerStates.Walking;
                }

                break;

            case PlayerStates.Walking:

                Movement(_horizontalDirection * walkingSpeed, rb.velocity.y);

                animator.SetFloat("Speed", Mathf.Abs(CurrentSpeed.x));

                if (CurrentSpeed.sqrMagnitude == 0)
                {
                    playerStates = PlayerStates.Idle;
                }

                break;

            case PlayerStates.Stealth:
                Movement(0, 0);

                circle.enabled = false;
                box.enabled = false;
                rb.gravityScale = 0;

                break;
        }
    }
    private void Movement(float x, float y)
    {
        CurrentSpeed = new Vector2(x, y);
        rb.velocity = CurrentSpeed;
    }
    private void FlipCharacter()
    {
        if (_horizontalDirection < 0)
        {
            sr.flipX = true;
        }
        else if (_horizontalDirection > 0)
        {
            sr.flipX = false;
        }
        else
        {
            //son directionda kalsın diye
        }
    }
}