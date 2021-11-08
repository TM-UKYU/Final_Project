using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathShot : MonoBehaviour
{
    //’e‚ÌƒIƒuƒWƒFƒNƒg
    public BreathBall breathBall;
    //ŠI‚ÌƒNƒ‰ƒX
    public CrabScript crabScript;

    public void BreathFiring()
    {
        Debug.Log("’e”­Ë");

        //’e‚Ì¶¬
        BreathBall obj = (BreathBall)Instantiate(breathBall, transform);
        //’e‚ğ”­Ë‚µ‚½–‚ğCrabScriot‚É‚à“`‚¦‚é
        crabScript.SetIsBreath(true);
    }
}
