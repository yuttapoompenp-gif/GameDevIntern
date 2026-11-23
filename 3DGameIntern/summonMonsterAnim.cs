using UnityEngine;

public class summonMonsterAnim : MonoBehaviour
{
    Animator animator;
    string currentAnim;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimCrossFade(string newAnim)
    {
        if (currentAnim == newAnim || animator == null) return;
        animator.CrossFade(newAnim, 0.2f);
        currentAnim = newAnim;
    }
}
