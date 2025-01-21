using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class plane : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private GameObject _prefabCube;
    [SerializeField] private float _spawnTime = 0.04f;
    [SerializeField] private int _countCubes = 20;
    
    [SerializeField] private float _timeToChangeColor = 0.5f;
    [SerializeField] private float _durationChange = 0.2f;

    private CubeColor[,] _cubes;

    private void Start()
    {
        _cubes = new CubeColor[_countCubes, _countCubes];
        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        for (int x = 19; x>= 0; x--)
        {
            for (int z = 0; z < 20; z++)
            {
                var cellWorldPos = _grid.GetCellCenterWorld(new Vector3Int(z-11, 0, x-11));
                var cube = Instantiate(_prefabCube);
                cube.transform.position = cellWorldPos + new Vector3(0, 0.15f);
                _cubes[z, x] = cube.GetComponent<CubeColor>();
                yield return new WaitForSeconds(_spawnTime);
            }
        }
    }

    public void ChangeColor()
    {
        StartCoroutine(ChangeColorCoroutine());
    }

    private IEnumerator ChangeColorCoroutine()
    {
        var newColor = Random.ColorHSV();

        for (int x = 19; x>= 0; x--)
        {
            for (int z = 0; z < 20; z++)
            {
                _cubes[z, x].ChangeColor(newColor, _timeToChangeColor);
                yield return new WaitForSeconds(_durationChange);
            }
        }
    }

    

}
