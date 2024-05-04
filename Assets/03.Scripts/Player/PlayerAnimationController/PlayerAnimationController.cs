using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class PlayerAnimationController : MonoBehaviour
{
    private Character _character;
    private Animator _animator;

    private void Start()
    {
        _character = GetComponent<Character>();
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        if (_character.IsGrounded())
        {
            _animator.SetBool("Ground", true);
            _animator.SetBool("Jump", false);

            if (_character.GetSpeed() > 0) _animator.SetBool("Run", true);
            else _animator.SetBool("Run", false);
        }
        else
        {
            _animator.SetBool("Ground", false);
            _animator.SetBool("Jump", true);
        }
    }
}
