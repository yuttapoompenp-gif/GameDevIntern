using UnityEngine;

[DefaultExecutionOrder(400)]
public class BossController : MonoBehaviour
{
    [SerializeField] public Transform boss;
    [SerializeField] Transform player;
    public static float speed = 3f;
    public static float currentSpeed;
    [SerializeField] bool foundPlayer;



    private void Start()
    {
        currentSpeed = speed;
    }

    private void Update()
    {
        if (boss != null && player != null)
        {
            MoveToPlayer();

        }
    }

    private void MoveToPlayer()
    {
        if (player != null && boss != null)
        {
            if(Vector3.Distance(boss.position, player.position) <= 10f)
            {
                
                Vector3 target = new Vector3(player.position.x, boss.position.y, 0f);

                boss.position = Vector3.MoveTowards(boss.position, target, currentSpeed * Time.deltaTime);

                if(BossAnim.bossRot == true)
                {
                    FacePlayer();
                }

                if (!foundPlayer && gameObject.activeSelf)
                {
                    foundPlayer = true;
                    if (Audio.instance != null && Audio.instance.musicSound.isPlaying) Audio.instance.musicSound.Stop();
                    if(Audio.instance != null && !Audio.instance.battleSound.isPlaying) Audio.instance.battleSound.Play();
                }


            }
            else
            {
                if (foundPlayer) 
                {
                    foundPlayer = false;
                    if (Audio.instance != null && Audio.instance.battleSound.isPlaying) Audio.instance.battleSound.Stop();
                    if (Audio.instance != null && !Audio.instance.musicSound.isPlaying) Audio.instance.musicSound.Play();

                }

            }
        }


    }

    private void FacePlayer()
    {
        Vector3 scale = boss.localScale;
        if(player.position.x > boss.position.x)
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        if(player.position.x < boss.position.x)
        {
            scale.x = Mathf.Abs(scale.x);
        }

        boss.localScale = scale;
    }

    

    
}   