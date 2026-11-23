using System;
using UnityEngine;

public class PlayerAnim1 : MonoBehaviour
{
    private Animator playerAnimator;

    int currentAnim_Hash;

    private static readonly int WalkF = Animator.StringToHash("Walk_F");
    private static readonly int WalkB = Animator.StringToHash("Walk_B");
    private static readonly int WalkL = Animator.StringToHash("Walk_L");
    private static readonly int WalkR = Animator.StringToHash("Walk_R");
    private static readonly int WalkFR = Animator.StringToHash("Walk_F_R");
    private static readonly int WalkFL = Animator.StringToHash("Walk_F_L");
    private static readonly int WalkBR = Animator.StringToHash("Walk_B_R");
    private static readonly int WalkBL = Animator.StringToHash("Walk_B_L");
    private static readonly int RunF = Animator.StringToHash("Run_F");
    private static readonly int RunFL = Animator.StringToHash("Run_F_L");
    private static readonly int RunFR = Animator.StringToHash("Run_F_R");
    private static readonly int RunB = Animator.StringToHash("Run_B");
    private static readonly int RunBL = Animator.StringToHash("Run_B_L");
    private static readonly int RunBR = Animator.StringToHash("Run_B_R");
    private static readonly int RunL = Animator.StringToHash("Run_L");
    private static readonly int RunR = Animator.StringToHash("Run_R");
    private static readonly int Atk_Spr = Animator.StringToHash("Attack_Sprint_Anim");

    private bool run;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }
 
    private void Update()
    {
        if (playerAnimator == null) return;
        MoveAnim();

        if (Input.GetKeyDown(KeyCode.Mouse0)) PlayAnim(Atk_Spr);
    }

    private void PlayAnimCrossFade(int Anim_Hash)
    {
        if(Anim_Hash == currentAnim_Hash) return;
        playerAnimator.CrossFade(Anim_Hash, 0.2f);
        currentAnim_Hash = Anim_Hash;
    }
    private void PlayAnim(int Anim_Hash)
    {
        playerAnimator.Play(Anim_Hash);
        currentAnim_Hash = Anim_Hash;
    }
    private void MoveAnim()
    {
        float h = PlayerController1.h; // A = -1, D = +1
        float v = PlayerController1.v;   // S = -1, W = +1

        run = Input.GetKey(KeyCode.LeftShift) ? true : false;
        



        bool f = v > 0 && h == 0;
        bool b = v < 0 && h == 0;
        bool r = h > 0 && v == 0; 
        bool l = h < 0 && v == 0;
        bool fr = v > 0 && h > 0;
        bool fl = v > 0 && h < 0;
        bool br = v < 0 && h > 0;
        bool bl = v < 0 && h < 0;

        bool rf = f == true && run == true; 
        bool rfl = fl == true && run == true; 
        bool rfr = fr == true && run == true; 
        bool rb = b == true && run == true; 
        bool rbl = bl == true && run == true; 
        bool rbr = br == true && run == true; 
        bool rl = l == true && run == true; 
        bool rr = r == true && run == true; 

        playerAnimator.SetBool(WalkF, f);
        playerAnimator.SetBool(WalkB, b);
        playerAnimator.SetBool(WalkR, r);
        playerAnimator.SetBool(WalkL, l);
        playerAnimator.SetBool(WalkFR, fr);
        playerAnimator.SetBool(WalkFL, fl);
        playerAnimator.SetBool(WalkBR, br);
        playerAnimator.SetBool(WalkBL, bl);
        playerAnimator.SetBool(RunF, rf);
        playerAnimator.SetBool(RunFL, rfl);
        playerAnimator.SetBool(RunFR, rfr);
        playerAnimator.SetBool(RunB, rb);
        playerAnimator.SetBool(RunBL, rbl);
        playerAnimator.SetBool(RunBR, rbr);
        playerAnimator.SetBool(RunL, rl);
        playerAnimator.SetBool(RunR, rr);


    }
}