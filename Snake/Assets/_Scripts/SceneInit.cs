using UnityEngine;

public class SceneInit : MonoBehaviour
{
    [SerializeField]
    Box florPref;

    [SerializeField]
    GameObject florAnchor;

    [SerializeField]
    SnakeTail snakePref;

    SceneOperator _sceneManager;
    Vector3 _snakeStartPos;
    private void Start()
    {
        _sceneManager = new SceneOperator();

        AppleTransform appleTransform = GetComponent<AppleTransform>();

        _snakeStartPos = new Vector3(4, 0.75f, 1);
        SnakeTail snake = Instantiate(snakePref, _snakeStartPos, Quaternion.identity);
        snake.RestartEvent += _sceneManager.ReloadScene;

        for (int i = 0; i < 10; i++)
        {
            for (int y = 0; y < 10; y++)
            {
                Box box = Instantiate(florPref, new Vector3(0 + i, 0, 0 + y), Quaternion.identity, florAnchor.transform);
                ChangeBoxMat(box, i, y);
                appleTransform.SetBoxInGround(box, i, y);
            }
        }
    }
    void ChangeBoxMat(Box box, int i, int y)
    {
        Renderer boxRend = box.gameObject.GetComponent<Renderer>();
        if (i % 2 == 0)
        {
            if (y % 2 != 0) boxRend.material.SetColor("_Color", Color.green);
        }
        else
        {
            if (y % 2 == 0) boxRend.material.SetColor("_Color", Color.green);
        }
    }
}
