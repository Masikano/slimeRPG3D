using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public GameObject enemyPrefab;
    private List<GameObject> _enemyList = new List<GameObject>();
    [SerializeField]
    private int _enemyQuantity;
    private int _maxActiveEnemyQuantity;
    private Vector3 _enemyRespawnPosition;
    private int _activeEnemyQuantity;

    private int _enemyMaxHealthDelta;
    //private int _bounty;

    [SerializeField]
    private List<Transform> _targetList;

    [SerializeField]
    private Transform _startPlayerTransform;
    [SerializeField]
    private Transform _endPlayerTransform;
    [SerializeField]
    private Transform _enemyRespawnTransform;
    [SerializeField]
    private int _fragCounter;
    private bool _playerMoveForward;
    private bool _playerMoveBackward;
    
    private Transform _playerTransform;
    private int _playerSpeed;
    //private bool _anyEnemyEnable;

    private void Awake()
    {
        instance = this;
        RestartButton.instance.gameObject.SetActive(false);
    }


    private void Start()
    {
        //_enemyQuantity = 1;
        _playerSpeed = 5;
        _playerTransform = PlayerController.instance.transform;
        _playerMoveForward = false;
        _playerMoveBackward = false;
        
        _enemyMaxHealthDelta = 10;
        _maxActiveEnemyQuantity = 2;
        _activeEnemyQuantity = 1;
        _enemyRespawnPosition = _enemyRespawnTransform.position;
        _fragCounter = 0;
        for(int i = 0; i < _maxActiveEnemyQuantity; i++)
        {
            Debug.Log("enemyCreation " + i);
            GameObject enemy = Instantiate(enemyPrefab, new Vector3(_enemyRespawnPosition.x + i, _enemyRespawnPosition.y,_enemyRespawnPosition.z - i*2), Quaternion.identity);
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            enemyComponent.StartPosition = enemy.transform.position;
            _enemyList.Add(enemy);
            enemyComponent.SetTargetPosition(_targetList[i].position);
            //enemyComponent.Bounty = _bounty;
            enemy.SetActive(false);
        }

    }

    private void Update()
    {
        if (_playerMoveForward)
        {
            _playerTransform.position = Vector3.MoveTowards(_playerTransform.position, _endPlayerTransform.position, _playerSpeed * Time.deltaTime);
            if (_playerTransform.position == _endPlayerTransform.position)
            {
                _playerMoveForward = false;
                _playerMoveBackward = true;
            }
        }
        else if (_playerMoveBackward)
        {
            _playerTransform.position = Vector3.MoveTowards(_playerTransform.position, _startPlayerTransform.position, _playerSpeed * 2 * Time.deltaTime);
            if (_playerTransform.position == _startPlayerTransform.position)
            {

                _playerMoveBackward = false;
                NextLevel();
            }
        }
        else
        {
            //if(_playerMoveForward == false && _playerMoveBackward == false)
            if (_fragCounter >= _enemyQuantity)
            {
                StartCoroutine(WaitForEnd(1));
                
            }
            else
            {
                if (_fragCounter >= _enemyQuantity / 2)
                {
                    _activeEnemyQuantity = 2;
                }

                bool _anyEnemyEnable = false;
                foreach (var enemy in _enemyList)
                {
                    if (enemy.active == true) _anyEnemyEnable = true;
                }
                if (_anyEnemyEnable == false)
                {
                    RespawnEnemy();
                }
            }   

            
        }
    }
    public void AddFrag() { _fragCounter++; }

    private void RespawnEnemy()
    {
        for(int i = 0; i < _activeEnemyQuantity; i++)
        {
            _enemyList[i].GetComponent<Enemy>().Reset();
            //_activeEnemyQuantity++;
        }
    }
    private void EndLevel()
    {
        foreach (var enemy in _enemyList)
        {
            enemy.SetActive(false);
            enemy.GetComponent<Enemy>().MaxHealth += _enemyMaxHealthDelta;
            enemy.GetComponent<Enemy>().Bounty += 2;
            enemy.GetComponent<Enemy>().DamageValue++;
        }
        
        _activeEnemyQuantity = 0;
        _playerMoveForward = true;
    }
   
    private void NextLevel()
    {

        
        _enemyMaxHealthDelta *= 2;
        _activeEnemyQuantity = 1;
        _enemyQuantity += 4;
        
        RespawnEnemy();
        PlayerController.instance.ResetPlayer();
        
        //_fragCounter = 0;


    }
    public void RetryLevel()
    {
        foreach(var enemy in _enemyList)
        {
            enemy.GetComponent<Enemy>().ExitCollition = true;
        }
        
        RespawnEnemy();
        PlayerController.instance.ResetPlayer();
        RestartButton.instance.gameObject.SetActive(false);
    }
    
    private IEnumerator WaitForEnd(float time)
    {
        _fragCounter = 0;
        yield return new WaitForSeconds(time);
        EndLevel();
    }



}
