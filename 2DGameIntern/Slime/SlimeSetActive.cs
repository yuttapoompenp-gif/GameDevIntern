using System.Collections;
using UnityEngine;

public class SlimeSetActive : MonoBehaviour
{
    [SerializeField] public GameObject slime;
    SlimeStatus slimestatus;
    [SerializeField ]Animator slimeAnimator;
    public bool isDead = false;
    private void Awake()
    {
        slimestatus = GetComponent<SlimeStatus>();

    }

    private void Update()
    {
        if (slime.activeSelf)
        {
            StartCoroutine(Slime_SetActive());
        }
    }

    private IEnumerator Slime_SetActive()
    {
        if (slime.CompareTag("BigSlime") && isDead == false)
        {
            if (slimestatus.bigSmileHealth <= 0)
            {
                isDead = true;
                slimeAnimator.SetTrigger("Dead");
                yield return new WaitForSeconds(1.47f);
                slime.SetActive(false);

            }
        }
        else if (slime.CompareTag("MediumSlime") && isDead == false)
        {
            if (slimestatus.mediumSmileHealth <= 0)
            {
                isDead = true;
                slimeAnimator.SetTrigger("Dead");
                yield return new WaitForSeconds(1.47f);
                slime.SetActive(false);

            }
        }
        else if (slime.CompareTag("SmallSlime") && isDead == false)
        {
            if (slimestatus.smallSmileHealth <= 0)
            {
                isDead = true;
                slimeAnimator.SetTrigger("Dead");
                yield return new WaitForSeconds(1.47f);
                slime.SetActive(false);

            }
        }
    }
}
