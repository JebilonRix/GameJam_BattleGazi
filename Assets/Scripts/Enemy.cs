using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public Player pl;

    [Range(1f, 10f)] public float speed;
    [Range(1f, 10f)] public float run;

    [SerializeField] private EnemyStates enemyStates = EnemyStates.Patrol;
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private LayerMask BorderLayer;
    [SerializeField] private AudioSource _audioSource;
    //[SerializeField] private AudioSource _audioSource1;
    //[SerializeField] private AudioSource _audioSource2;
    //[SerializeField] private AudioSource _audioSource3;
    //[SerializeField] private AudioSource _audioSource4;
    //[SerializeField] private AudioSource _audioSource5;

    private bool movingRight = true;
    private Transform target;
    private RaycastHit2D playerInfo;
    private RaycastHit2D borderInfo;

    private float Uzak = 5f;
    private float Yakin = 0.5f;
    private bool _detected_Fx = false;

    public enum EnemyStates
    {
        Patrol, Chase, Capture
    }
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //Vector3 test = transform.position;
    }
    private void Update()
    {
        StateMachineEnemy();
    }
    void StateMachineEnemy()
    {
        playerInfo = Physics2D.Raycast(transform.position, transform.right.normalized, Uzak, PlayerLayer);

        switch (enemyStates)
        {
            case EnemyStates.Patrol:

                transform.Translate(Vector3.right * (speed * Time.deltaTime));

                if (movingRight == true)
                {
                    borderInfo = Physics2D.Raycast(transform.position, transform.right.normalized, Yakin, BorderLayer);
                }
                else
                {
                    borderInfo = Physics2D.Raycast(transform.position, transform.right.normalized, Yakin, BorderLayer);
                }

                if (borderInfo)
                {
                    if (movingRight == true)
                    {
                        transform.eulerAngles = new Vector3(0, -180, 0);
                        movingRight = false;
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        movingRight = true;
                    }
                }
                if (playerInfo.collider != null)
                {
                    if (pl.playerStates == Player.PlayerStates.Stealth)
                    {
                        enemyStates = EnemyStates.Patrol;
                    }
                    else
                    {
                        enemyStates = EnemyStates.Chase;

                        if (_detected_Fx == false)
                        {
                            _audioSource.Play();

                            FindObjectOfType<SesEfendisi>().PlayRandomAudio(7, 9);

                            _detected_Fx = true;
                        }
                    }
                }

                break;

            case EnemyStates.Chase:

                transform.position = Vector3.MoveTowards(transform.position, target.position, (run + speed) * Time.deltaTime);

                if (playerInfo.collider == null)
                {
                    enemyStates = EnemyStates.Patrol;
                    _detected_Fx = false;

                    FindObjectOfType<SesEfendisi>().PlayRandomAudio(1, 6);

                }

                break;

            case EnemyStates.Capture:

               FindObjectOfType<SesEfendisi>().PlayRandomAudio(10, 11);
              
                SceneManager.LoadScene("02GameOver");

                break;
        }
    }
    //karakter ve düşman çarpıştığında
    private void OnCollisionEnter2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            if (pl.playerStates == Player.PlayerStates.Stealth)
            {
                enemyStates = EnemyStates.Patrol;
            }
            else
            {
                enemyStates = EnemyStates.Capture;
            }
        }
    }
}