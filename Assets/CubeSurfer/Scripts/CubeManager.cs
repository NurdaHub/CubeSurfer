using System.Collections;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private CubesContainer _cubesContainer;
    private float _defaultYPosition = 0.7f;

    public void Init(CubesContainer cubesContainer, Vector3 newPosition)
    {
        transform.position = new Vector3(newPosition.x, _defaultYPosition, newPosition.z);
        _cubesContainer = cubesContainer;
    }

    public IEnumerator AddDetector()
    {
        yield return null;
        gameObject.AddComponent<CollisionDetector>();
    }

    public void OnCorrectCube(GameObject correctCube)
    {
        Debug.Log("correct");
        var cubeManager = correctCube.GetComponent<CubeManager>();
        cubeManager.Init(_cubesContainer, transform.position);
        _cubesContainer.AddCube(cubeManager);
        StartCoroutine(cubeManager.AddDetector());
    }

    public void OnWrongCube()
    {
        Debug.Log("wrong");
        _cubesContainer.DeleteCubeFromContainer(this);
    }
}
