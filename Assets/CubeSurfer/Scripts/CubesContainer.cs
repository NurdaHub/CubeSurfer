using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesContainer : MonoBehaviour
{
    [SerializeField] private Transform otherCubesParent;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Game game;
    [SerializeField] private Vector3 positionToUp = new Vector3(0, 0.41f, 0);
    [SerializeField] private float waitTime = 0.4f;
    
    private List<CubeManager> _cubeManagers = new List<CubeManager>();
    private List<GameObject> _oldCubes = new List<GameObject>();

    public void AddCube(CubeManager cubeManager)
    {
        playerTransform.position += positionToUp;
        var cubeTransform = cubeManager.transform;
        cubeTransform.parent = transform;
        _cubeManagers.Add(cubeManager);
    }

    public void DeleteCubeFromContainer(CubeManager cubeManager)
    {
        if (_cubeManagers.Count > 0)
        {
            _cubeManagers.RemoveAt(_cubeManagers.Count - 1);
            cubeManager.transform.parent = otherCubesParent;
            _oldCubes.Add(cubeManager.gameObject);
            StartCoroutine(ContainerDown());
        }
        else
        {
            game.Lose();
        }
    }

    private IEnumerator ContainerDown()
    {
        yield return new WaitForSeconds(waitTime);
        playerTransform.position -= positionToUp;
    }

    public void ClearAllCubes()
    {
        foreach (var cube in _oldCubes)
            Destroy(cube);

        foreach (var cubeManager in _cubeManagers)
            Destroy(cubeManager.gameObject);
        
        _oldCubes.Clear();
        _cubeManagers.Clear();
    }
}
