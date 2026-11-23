 using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    private GameObject player;
    private CharacterController characterController;
    private float speed = 1.5f;
    public static float h = 0;
    public static float v = 0;


    void Start()
    {
        player = gameObject;
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Vector3 movePos = transform.forward * v * speed * Time.deltaTime + transform.right * h * speed * Time.deltaTime + transform.up * 0;
        if (player != null && characterController != null)
        {
            characterController.Move(movePos);
        }
    }

    
}
