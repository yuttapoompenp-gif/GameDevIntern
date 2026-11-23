using UnityEngine;

[DefaultExecutionOrder(-500)]
[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance { get; private set; }

    [Header("Main Component")]
    [SerializeField] Transform player;
    [HideInInspector] public CharacterController controller;
    [SerializeField] Transform leftFoot;
    [SerializeField] Transform rightFoot;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float gravity = -9.81f;

    [Header("Handle Value")]
    [Range(0, 10)][SerializeField] float heightColliderSize = 2f;
    [Range(0, 10)][SerializeField] float radiusColliderSize = 0.5f;
    [Range(0, 10)][SerializeField] float radiusColliderGroundSize = 0.1f;
    [SerializeField] Vector3 centerColider = new Vector3(0, 1, 0);

    [Range(0, 10)][SerializeField] float speed;
    [Range(0, 10)][SerializeField] public float runSpeed = 5f;
    [Range(0, 10)][SerializeField] public float walkSpeed = 2f;
    [Range(0, 10)][SerializeField] float wireSphereSize = 0.5f;
    [SerializeField] Vector3 wireSphereHeight = new Vector3(0, 0, 0);

    [Header("Dash Settings")]
    [SerializeField] private float dashForce = 5f;          // ความเร็วในการพุ่ง
    [SerializeField] private float dashDuration = 0.2f;      // ระยะเวลาที่พุ่ง
    private float dashTimeLeft;
    private Vector3 dashVelocity;

    // เพิ่มตัวแปรเก็บความเร็วในแนวตั้ง (แรงโน้มถ่วง)
    private float verticalVelocity;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        if (player != null)
        {
            controller = player.GetComponent<CharacterController>();
        }

        WalkSpeed();
    }

    private void Update()
    {
        if (controller == null) return;

        SetColliderSize();
        if (!PlayerAnim.instance.checkParry)
        {
            Move();
        }
    }

    private float ApplyGravity()
    {
        if (isGrounded())
        {
            // แรงกดลงพื้นเล็กน้อยเมื่ออยู่บนพื้น เพื่อให้ CheckSphere / CharacterController เสถียร
            if (verticalVelocity < 0)
            {
                verticalVelocity = -2f;
            }
        }
        else
        {
            // สะสมความเร็วการตกตามกาลเวลา (v = v0 + g*t)
            verticalVelocity += gravity * Time.deltaTime;
        }

        return verticalVelocity;
    }

    private Vector3 MovePos()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 1. คำนวณทิศทางการเดินปกติ
        Vector3 moveInput = (controller.transform.forward * v) + (controller.transform.right * h);
        Vector3 moveDirection = moveInput * speed;

        // 2. ตรวจสอบการกดปุ่ม Dash (LeftControl)
        if (Input.GetKeyDown(KeyCode.LeftControl) && dashTimeLeft <= 0)
        {
            Vector3 dashDir = Vector3.zero;

            // ถ้าผู้เล่นกดปุ่มทิศทางอยู่ ให้แดชไปทางนั้น ถ้าไม่ได้กด ให้แดชไปข้างหน้าตัวละคร
            if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
            {
                dashDir = moveInput.normalized;
            }
            else
            {
                dashDir = controller.transform.forward;
            }

            dashVelocity = dashDir * dashForce;
            dashTimeLeft = dashDuration;
            Debug.Log("Dash Triggered!");
        }

        // 3. จัดการระยะเวลาและความเร็วของการ Dash
        if (dashTimeLeft > 0)
        {
            dashTimeLeft -= Time.deltaTime;
        }
        else
        {
            // ค่อยๆ ลดแรงแดชกลับเป็น 0 อย่างนุ่มนวล
            dashVelocity = Vector3.Lerp(dashVelocity, Vector3.zero, 15f * Time.deltaTime);
        }

        // 4. รวมความเร็วเดิน + แรงแดช
        Vector3 finalMove = moveDirection + dashVelocity;

        // 5. ใส่แรงโน้มถ่วงเข้าไปที่แกน Y
        finalMove.y = ApplyGravity();

        return finalMove * Time.deltaTime;
    }

    public void RunSpeed()
    {
        speed = runSpeed;
        Status.instance.DecreasePlayerStamina();
    }

    public void WalkSpeed()
    {
        speed = walkSpeed;
        Status.instance.IncreasePlayerStamina();
    }

    private void Move()
    {
        // เรียก Move() ตลอดเวลา (รวมถึงตอนอยู่กลางอากาศเพื่อให้ตกลงมาได้)
        controller.Move(MovePos());
    }

    private void SetColliderSize()
    {
        controller.height = heightColliderSize;
        controller.radius = radiusColliderSize;
        controller.center = centerColider;
    }

    private void OnDrawGizmosSelected()
    {
        if (controller == null) return;

        Gizmos.color = isGrounded() ? Color.green : Color.red;
        Gizmos.DrawWireSphere(controller.transform.position + wireSphereHeight, wireSphereSize);
    }

    public bool isGrounded()
    {
        bool grounded = Physics.CheckSphere(controller.transform.position + wireSphereHeight, wireSphereSize, groundLayer);
        return grounded;
    }
}