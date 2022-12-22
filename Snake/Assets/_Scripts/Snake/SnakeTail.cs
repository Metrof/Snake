using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour, IAppleEater
{
    public delegate void CollisionObstacle();
    public event CollisionObstacle RestartEvent;

    [SerializeField]
    GameObject _tailPref;

    Transform _head;
    float tailMagnitude = 1f;

    List<Transform> _limbs = new List<Transform>();
    List<Vector3> _positions = new List<Vector3>();
    private void Awake()
    {
        _head = transform;
        _positions.Add(_head.position);
    }
    private void FixedUpdate()
    {
        float distance = (_head.position - _positions[0]).magnitude;

        if (distance > tailMagnitude)
        {
            Vector3 direction = (_head.position - _positions[0]).normalized;

            _positions.Insert(0, _positions[0] + direction * tailMagnitude);
            _positions.RemoveAt(_positions.Count - 1);

            distance -= tailMagnitude;
        }

        for (int i = 0; i < _limbs.Count; i++)
        {
            _limbs[i].position = Vector3.Lerp(_positions[i + 1], _positions[i], distance / tailMagnitude);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle") Restart();
    }
    void AddTail()
    {
        Vector3 tailPos = (_head.position - _positions[0]).normalized;
        GameObject tailLimb = Instantiate(_tailPref, _positions[_positions.Count - 1] - tailPos, Quaternion.identity, _head);
        _positions.Add(tailLimb.transform.position);
        _limbs.Add(tailLimb.transform);

        if(_limbs.Count == 1)
        {
            Collider limbColl = tailLimb.GetComponent<Collider>();
            limbColl.enabled = false;
        }

        if (_limbs.Count >= 99) Restart();
    }
    public void EatApple() => AddTail();
    void Restart() => RestartEvent?.Invoke();
}
