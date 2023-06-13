using Services;
using Services.Input;
using UnityEngine;

public class FootprintSpawner : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GameObject footprintPrefab;
    [SerializeField] private float spawnTime = 1;
    [SerializeField] private Transform spawnPoint;
    private IInputService _inputService;
    private float _timer;
    public void Construct() => _inputService = AllServices.Container.Single<IInputService>();
    
    private void Update()
    {
        _timer -= Time.deltaTime;
        
        if (IsMoving() && CanSpawn())
        {
            Instantiate(footprintPrefab, spawnPoint.position, Quaternion.Euler(90, 0, 0));
        }
    }

    private bool IsMoving()
    {
        return _inputService.Axis.sqrMagnitude > Constants.Epsilon;
    }

    private bool CanSpawn()
    {
        if (_timer <= 0 && characterController.isGrounded)
        {
            _timer = spawnTime;
            return true;
        }
        
        return false;
    }
}