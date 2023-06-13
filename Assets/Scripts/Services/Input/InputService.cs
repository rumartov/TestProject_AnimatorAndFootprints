using UnityEngine;

namespace Services.Input
{
  public abstract class InputService : IInputService
  {
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";
    private const string Fire = "Fire";
    private const string Jump = "Jump";

    public abstract Vector2 Axis { get; }

    public bool IsAttackButton()
    {
      return SimpleInput.GetButton(Fire);
    }
    
    public bool IsJumpButtonUp()
    {
      return SimpleInput.GetButtonUp(Jump);
    }

    protected static Vector2 SimpleInputAxis()
    {
      return new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
  }
}