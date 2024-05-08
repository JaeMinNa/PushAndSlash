using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public void StartSFX(string name)
    {
        GameManager.I.SoundManager.StartSFX(name);
    }
}
