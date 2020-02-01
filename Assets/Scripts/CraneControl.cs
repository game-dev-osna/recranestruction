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
    [Space]
    [SerializeField] private AudioClip _brakeSound;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private InputAction _ropeLengthInput;
    [SerializeField] private float _ropeSpeed;
    [SerializeField] private float _minLength = 0;
    [SerializeField] private float _maxLength = 0.35f;
    [SerializeField] private RopeControllerRealisticNoSpring _ropeController;

    private float speed;

    void OnEnable()
    {
        _rotate.Enable();
        _ropeLengthInput.Enable();
    }

    void OnDisable()
    {
        _rotate.Disable();
        _ropeLengthInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateRotation();
        CalculcateRopeLength();
    }

    private void CalculateRotation()
    {
        float accel = _acceleration * _rotate.ReadValue<float>();
        float input = _rotate.ReadValue<float>();
        float accel = _acceleration * input;
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
        float wouldBeSpeed = (speed + Time.deltaTime * accel) * _friction;
        speed = Mathf.Clamp(wouldBeSpeed, -_maxSpeed, _maxSpeed);
        // float wouldBeSpeed = (speed + Time.deltaTime * accel) - Mathf.Sign(speed) * _friction * Time.deltaTime;
        // speed = speed < 0 ? Mathf.Clamp(wouldBeSpeed, -_maxSpeed, 0) : Mathf.Clamp(wouldBeSpeed, 0, _maxSpeed);
        _mainPart.localEulerAngles += Time.deltaTime * new Vector3(0, speed, 0);
    }

    private void CalculcateRopeLength()
    {
        var lengthChange = _ropeLengthInput.ReadValue<float>();
        Debug.Log(lengthChange);
        var newLength = (lengthChange * _ropeSpeed * Time.deltaTime) + _ropeController.ropeSectionLength;
        _ropeController.ropeSectionLength = Mathf.Clamp(newLength, _minLength, _maxLength);
    }
}
