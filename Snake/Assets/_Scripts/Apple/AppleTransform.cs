using UnityEngine;

public class AppleTransform : MonoBehaviour
{
    [SerializeField]
    Apple _applePref;

    Apple _apple;

    Box[,] _ground = new Box[10, 10];
    private void Start()
    {
        _apple = Instantiate(_applePref, new Vector3(4, 0.75f, 8), Quaternion.identity);
        _apple.Teleportation += MoveApple;
    }

    void MoveApple()
    {
        int randX;
        int randZ;
        do
        {
            randX = Random.Range(0, 9);
            randZ = Random.Range(0, 9);
        } while (_ground[randX, randZ].OverGround);

        _apple.transform.position = new Vector3(randX, 0.75f, randZ);
    }

    public void SetBoxInGround(Box box, int x, int z) => _ground[x, z] = box;
    ~AppleTransform() => _apple.Teleportation -= MoveApple;
}
