using UnityEngine;

public class SnakeMove : MonoBehaviour
{
    [SerializeField]
    float _speed = 5;
    float _timeBetweenChangeDir;
    float _timeStart;

    Vector2 _receiveDir = Vector2.up;
    Vector3 _endHeadPos;
    Vector2 _currentDir;

    Controls _control;

    private void Awake()
    {
        _endHeadPos = SetTarget();
        _control = new Controls();
    }
    private void Start()
    {
        _timeBetweenChangeDir = 1 / _speed;
        _timeStart = Time.time;
    }
    private void OnEnable() => _control.Enable();
    private void OnDisable() => _control.Disable();

    private void FixedUpdate()
    {
        if (_control.SnakeMap.Move.ReadValue<Vector2>() != Vector2.zero) _receiveDir = _control.SnakeMap.Move.ReadValue<Vector2>();
        Move();
    }
    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _endHeadPos, Time.fixedDeltaTime * _speed);
        if (_timeStart + _timeBetweenChangeDir <= Time.time)
        {
            ChangeTarget();
            _timeStart = Time.time;
        }
    }
    void ChangeTarget()
    {
        if (_currentDir + _receiveDir != Vector2.zero) _currentDir = _receiveDir;
        _endHeadPos = SetTarget();
    }
    Vector3 SetTarget() => new Vector3(transform.position.x + _currentDir.x, 0.75f, transform.position.z + _currentDir.y);
}
