using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] public AudioSource musicSound;
    [SerializeField] public AudioSource battleSound;

    public static Audio instance {get; set;}

    public void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
            musicSound.Play();   
    }

    public void Update()
    {

    }
}
