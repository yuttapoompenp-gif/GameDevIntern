using UnityEngine;
using System.Collections.Generic;

public class IgnoreCollision : MonoBehaviour
{

    [SerializeField] private string targetLayerName;
    private void Awake()
    {

        Collider2D myCollider = GetComponent<Collider2D>();

        if (myCollider != null && targetLayerName != null)
        {

            int targetLayer = LayerMask.NameToLayer(targetLayerName);

            Collider2D[] allColliders = FindObjectsByType<Collider2D>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (Collider2D col in allColliders)
            {
                if (col.gameObject.layer == targetLayer)
                {

                    Physics2D.IgnoreCollision(myCollider, col, true);
                }
            }

        }
    }


}
