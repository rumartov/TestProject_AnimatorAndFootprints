using Services;
using Services.Input;
using UnityEngine;

namespace Logic.Hero
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 6.0f;
        [SerializeField] private float jumpHeight = 1.0f;
        [SerializeField] private float gravity = 20.0f;
        [SerializeField] private HeroAnimator animator;
        [SerializeField] private CharacterController characterController;

        private IInputService _inputService;
        private Camera _mainCamera;
        private Vector3 _moveDirection = Vector3.zero;

        public void Construct()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            Debug.Log(characterController.isGrounded);
            if (characterController.isGrounded)
            {
                _moveDirection = _mainCamera.transform.TransformDirection(_inputService.Axis);
                _moveDirection.y = 0;
                _moveDirection.Normalize();
                _moveDirection *= moveSpeed;
                transform.forward = _moveDirection;
                
                if (_inputService.IsJumpButtonUp())
                {
                    _moveDirection.y = jumpHeight;
                    animator.PlayJump();
                }
            }

            _moveDirection.y += gravity * Time.deltaTime;

            characterController.Move( _moveDirection * Time.deltaTime);
        }

    }
}