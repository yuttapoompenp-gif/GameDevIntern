using Unity.VisualScripting;
using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    GameObject sword;
    [SerializeField] Transform tip_sword;
    [SerializeField] Transform base_sword;
    void Awake()
    {
        sword = gameObject;
    }

    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Matrix4x4 oldMatrix = Gizmos.matrix;

        Gizmos.color = Color.green;
        if (tip_sword != null && base_sword != null)
        {
            Gizmos.DrawLine(tip_sword.position, base_sword.position);
            Gizmos.color = Color.red;

            Vector3 directionBaseToTip = (tip_sword.position - base_sword.position).normalized;

            float angle = Vector3.Angle(tip_sword.forward, directionBaseToTip);
            Vector3 rotationAxis = Vector3.Cross(tip_sword.forward, directionBaseToTip);
            Quaternion rotationDelta = Quaternion.AngleAxis(angle, rotationAxis);
            Vector3 midPos_Sword = (base_sword.position + tip_sword.position)/2;



            Gizmos.DrawWireCube(midPos_Sword, new Vector3(.2f,.2f,.5f));

            Gizmos.matrix = gameObject.transform.localToWorldMatrix;

        }
        Gizmos.matrix = oldMatrix;

    }
}
