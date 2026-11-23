using UnityEngine;
using System.Collections.Generic;

[DefaultExecutionOrder(-800)]
[ExecuteAlways]
public class WeaponSet : MonoBehaviour
{
    public static WeaponSet instance { get; private set; } 

    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh bakeMesh;
    [SerializeField] public Transform baseSword, tipSword, bladePos;
    [SerializeField] private int baseIndex, tipIndex;
    [SerializeField] private float boxThickness = 0.25f;
    [SerializeField] private LayerMask enemyLayer;
    Vector3 bladeDir;

    public static HashSet<Collider> hitHistory = new HashSet<Collider>();
    private List<Vector3> cachedVertices = new List<Vector3>();
    private Collider[] hitResult = new Collider[10];
    //public static bool isAttacking = false;

    public static bool isAttacking  { get; set;}
    public static Collider hit;


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

        BakeMeshStorage();
    }

    public static void OnAttack()
    {
        
        hitHistory.Clear();
        isAttacking = true;
        Debug.Log("Attack");
    }

    public static void OnEndAttack()
    {
        hitHistory.Clear();
        isAttacking = false;
        hit = null;
        Debug.Log("EndAttack");

    }


    void Update()
    {

        UpdateSwordTransforms();

        if (isAttacking && Application.isPlaying)
        {
            DetectHit();
        }

        if(isAttacking == false)
        {
            hitHistory.Clear();
        }

    }


    private void BakeMeshStorage()
    {
        if(bakeMesh == null)
        {
            bakeMesh = new Mesh();
        }
    }

    private void UpdateSwordTransforms()
    {
        if (skinnedMeshRenderer == null || baseSword == null || tipSword == null ||  bladePos == null) return;

        skinnedMeshRenderer.BakeMesh(bakeMesh);

        bakeMesh.GetVertices(cachedVertices);
        if (cachedVertices.Count == 0) return;

        baseIndex = Mathf.Clamp(baseIndex, 0, cachedVertices.Count - 1);
        tipIndex = Mathf.Clamp(tipIndex, 0, cachedVertices.Count - 1);

        baseSword.position = skinnedMeshRenderer.transform.TransformPoint(cachedVertices[baseIndex]);
        tipSword.position = skinnedMeshRenderer.transform.TransformPoint(cachedVertices[tipIndex]);
        float bladeLenght = Vector3.Distance(baseSword.position, tipSword.position);
        bladeDir = tipSword.position - baseSword.position;
        bladeDir.Normalize();


        bladePos.position = Vector3.Lerp(baseSword.position, tipSword.position, 0.5f);
        bladePos.rotation = Quaternion.LookRotation(bladeDir, Vector3.up);
        bladePos.localScale = new Vector3 (boxThickness * 0.2f, boxThickness * 0.2f, bladeLenght);



    }

    private void DetectHit()
    {
        int count = Physics.OverlapBoxNonAlloc(bladePos.position, bladePos.localScale / 2, hitResult, bladePos.rotation, enemyLayer);

        for(int i = 0; i < count; i++)
        {
            hit = hitResult[i];

            if (!hitHistory.Contains(hit))
            {
                hitHistory.Add(hit);

                Vector3 hitPoint = hit.ClosestPoint(bladePos.position);

                OnHitTarget(hit, hitPoint);
            }
        }


    }

    private void OnHitTarget(Collider target, Vector3 point)
    {
        Debug.Log($"Hit {target.name} at {point}");
    }
    private void OnDrawGizmos()
    {

        Matrix4x4 oldMatrix = Gizmos.matrix;

        Gizmos.color = isAttacking ? Color.red : Color.green;
        Gizmos.DrawSphere(baseSword.position, 0.03f);
        Gizmos.DrawWireSphere(tipSword.position, 0.05f);
        Gizmos.DrawLine(baseSword.position, tipSword.position);

        Gizmos.matrix = bladePos.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

        Gizmos.matrix = oldMatrix;
    }

}
