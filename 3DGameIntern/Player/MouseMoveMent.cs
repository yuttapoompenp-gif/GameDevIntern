using UnityEngine;

namespace Controller
{
    [DefaultExecutionOrder(-600)]
    [DisallowMultipleComponent]
    [AddComponentMenu("Controller/MouseMoveMent")]
    public class MouseMoveMent : MonoBehaviour
    {
        public static MouseMoveMent instance { get; set; }

        [Header("Main Component")]
        [SerializeField] private Transform playerToRot;
        [SerializeField] private Transform cameraToRot;

        [Header("Sensitivity Settings")]
        [SerializeField] private float sensitivity = 150f;

        [Header("Camera Pitch Limits (Look Up/Down)")]
        [Range(0, 90)][SerializeField] private float maxXRot = 60f;
        [Range(-90, 0)][SerializeField] private float minXRot = -30f;

        [Header("Orbit Camera Distance & Offset")]
        [SerializeField] private Vector3 targetOffset = new Vector3(0f, 1.5f, 0f); // จุดโฟกัสกล้อง (เช่น ช่วงหัวไหล่/ศีรษะ)
        [Range(1f, 15f)][SerializeField] public float distance = 3f;             // ระยะห่างของกล้องจากตัวละคร
        [SerializeField] private float smoothSpeed = 10f;                           // ความนุ่มนวลในการหมุนของกล้อง
        public float originalDistance;
        
        private float xRot; // มุมก้ม-เงย (Pitch)
        private float yRot; // มุมหันซ้าย-ขวา (Yaw)

        private void Awake()
        {
            if(instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }

            originalDistance = distance;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (cameraToRot == null && Camera.main != null)
            {
                cameraToRot = Camera.main.transform;
            }

            if (playerToRot == null)
            {
                GameObject playerObj = GameObject.FindWithTag("Player");
                if (playerObj != null)
                {
                    playerToRot = playerObj.transform;
                }
                else
                {
                    Debug.LogWarning("ไม่พบ GameObject ที่ใส่ Tag : Player ใน Scene");
                }
            }

            // ยกเลิกการเอา cameraToRot ไปเป็น Child ของ playerToRot เพื่อป้องกันไม่ให้การหมุนของตัวละครมากระทบกล้อง
            if (cameraToRot != null && cameraToRot.parent == playerToRot)
            {
                cameraToRot.SetParent(null);
            }

            // ตั้งค่ามุมเริ่มต้นจากมุมกล้องปัจจุบัน
            if (cameraToRot != null)
            {
                Vector3 currentAngles = cameraToRot.eulerAngles;
                xRot = currentAngles.x;
                if (xRot > 180f) xRot -= 360f;
                yRot = currentAngles.y;
            }
        }

        private void LateUpdate()
        {
            // แนะนำให้ประมวลผลตำแหน่งกล้องใน LateUpdate เพื่อป้องกันกล้องกระตุกตามการเคลื่อนที่ของตัวละคร
            RotateCamera();
            RotatePlayerWithInput();
        }

        private void RotateCamera()
        {
            if (cameraToRot == null || playerToRot == null) return;

            // 1. รับค่าจากเมาส์
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            yRot += mouseX;
            xRot -= mouseY;
            xRot = Mathf.Clamp(xRot, minXRot, maxXRot);

            // 2. คำนวณ Rotation ของกล้อง
            Quaternion targetRotation = Quaternion.Euler(xRot, yRot, 0f);

            // 3. คำนวณ Position ของกล้องแบบ Orbit รอบตัวละคร
            Vector3 targetPosition = playerToRot.position + targetOffset;
            Vector3 cameraPosition = targetPosition - (targetRotation * Vector3.forward * distance);

            // 4. อัปเดตตำแหน่งและการหมุนของกล้องด้วยความนุ่มนวล (Smooth)
            cameraToRot.rotation = Quaternion.Slerp(cameraToRot.rotation, targetRotation, smoothSpeed * Time.deltaTime);
            cameraToRot.position = Vector3.Lerp(cameraToRot.position, cameraPosition, smoothSpeed * Time.deltaTime);
        }


        private void RotatePlayerWithInput()
        {
            if (playerToRot == null) return;

            // เช็กว่ามีการกดปุ่มเคลื่อนที่ (WASD) หรือไม่
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
            {
                // หมุนตัวละครให้หันไปตามทิศทางแนวนอนของกล้องเมื่อมีการกดเดิน
                Quaternion targetPlayerRotation = Quaternion.Euler(0f, yRot, 0f);
                playerToRot.rotation = Quaternion.Slerp(playerToRot.rotation, targetPlayerRotation, smoothSpeed * Time.deltaTime);
            }
        }
    }
}