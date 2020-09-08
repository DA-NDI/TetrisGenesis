using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTet : MonoBehaviour
{
    [SerializeField] private TetrisBlock[] _tetraminos;
	private bool _stateOn = false;
	private TetrisBlock _tetrisBlock;
    // Start is called before the first frame update
    public ControllerManager _controllerManager;
    void Start()
    {

    }

    void Update()
	{
		if (_stateOn == false && !_controllerManager.MainMenuController.IsOpen)
		{
			SpawnNewTetramino();
			_stateOn = true;
		}
	}

    public void SpawnNewTetramino()
    {
        _tetrisBlock = Instantiate(_tetraminos[UnityEngine.Random.Range(0, _tetraminos.Length)], transform.position, Quaternion.identity);
    }

    internal void NewTetromino()
    {
        throw new NotImplementedException();
    }

    public void IncreaseScore()
	{
		_controllerManager.InGameController.IncreaseScore(10);
	}
}
