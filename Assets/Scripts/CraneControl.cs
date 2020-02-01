using UnityEngine;
using UnityEngine.InputSystem;

public class CraneControl : MonoBehaviour
{
    [SerializeField] private InputAction _rotate;
    [SerializeField] private float _acceleration;
    [SerializeField] private Transform _mainPart;
    [SerializeField] private float _friction;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _fastEnoughForSparks;
    [SerializeField] private float _brakeBoost;
    [SerializeField] private ParticleSystem _sparks;
    public Rigidbody _arm;
    [Space]
    [SerializeField] private AudioClip _brakeSound;
    [SerializeField] private AudioSource _audioSource;

    void OnEnable()
    {
        _rotate.Enable();
    }

    void OnDisable()
    {
        _rotate.Disable();
    }

    private void Start()
    {
        _arm.maxAngularVelocity = _maxSpeed;
        _arm.angularDrag = _friction;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateRotation();
    }

    private void CalculateRotation()
    {
        float input = _rotate.ReadValue<float>();
        float accel = _acceleration * input;
        float speed = _arm.angularVelocity.y;
        // actively steering against rotation
        if (Mathf.Abs(input) > .5f && input < 0 != speed < 0)
        {
            accel *= _brakeBoost;
            if (Mathf.Abs(speed) > _fastEnoughForSparks)
            {
                _sparks.Play();
                if (!_audioSource.isPlaying)
                    _audioSource.PlayOneShot(_brakeSound);
            }
            else
            {
                _sparks.Stop();
            }
        }
        _arm.angularVelocity += new Vector3(0, Time.deltaTime * accel, 0);
    }

}
