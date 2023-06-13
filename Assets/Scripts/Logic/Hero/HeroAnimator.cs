using System;
using Logic.Animation;
using UnityEngine;

namespace Logic.Hero
{
  public class HeroAnimator : MonoBehaviour, IAnimationStateReader
  {
    [SerializeField] private CharacterController characterController;
    [SerializeField] public Animator animator;

    private string _shootingLayer = "Shooting";
    
    private static readonly int WalkingSpeedHash = Animator.StringToHash("WalkingSpeed");
    private static readonly int AttackHash = Animator.StringToHash("Shoot");
    private static readonly int JumpHash = Animator.StringToHash("Jump");

    private readonly int _attackStateHash = Animator.StringToHash("Attack");
    private readonly int _walkingStateHash = Animator.StringToHash("Walking");
    private readonly int _jumpStateHash = Animator.StringToHash("Jump");

    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited;

    public AnimatorState State { get; private set; }

    public bool IsJumping() => State == AnimatorState.Jump;

    public bool IsAttacking => State == AnimatorState.Attack;

    private void Update()
    {
      animator.SetFloat(
        WalkingSpeedHash, characterController.velocity.magnitude, 0.1f, Time.deltaTime); 
    }

    public void PlayAttack()
    { 
      animator.SetLayerWeight(animator.GetLayerIndex(_shootingLayer), 1);
    }

    public void StopAttack()
    {
      animator.SetLayerWeight(animator.GetLayerIndex(_shootingLayer), 0);
    }

    public void PlayJump()
    {
      animator.SetTrigger(JumpHash);
    }

    public void EnteredState(int stateHash)
    {
      State = StateFor(stateHash);
      StateEntered?.Invoke(State);
    }

    public void ExitedState(int stateHash)
    {
      StateExited?.Invoke(StateFor(stateHash));
    }

    private AnimatorState StateFor(int stateHash)
    {
      AnimatorState state;
      if (stateHash == _attackStateHash)
      {
        state = AnimatorState.Attack;
      }
      else if (stateHash == _walkingStateHash)
      {
        state = AnimatorState.Walking;
      }
      else if (stateHash == _jumpStateHash)
      {
        state = AnimatorState.Jump;
      }
      else
      {
        state = AnimatorState.Unknown;
      }

      return state;
    }
  }
}