using System;
using System.Collections;
using System.Collections.Generic;
using Logic.Hero;
using Services;
using Services.Input;
using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    [SerializeField] private HeroAnimator animator;
    private IInputService _inputService;
    
    public void Construct() => _inputService = AllServices.Container.Single<IInputService>();
    private void Update()
    {
        if (_inputService.IsAttackButton())
            animator.PlayAttack();
        else
            animator.StopAttack();
    }
}
