using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float maxSpeed = 4.0f;
    public float maxHealth = 100.0f;
    public Transform player;

    private NavMeshAgent _agent;
    private Animator _animator;
    private float dividedSpeed = 0.0f;
    private bool isDead = false;
    private bool isDestroy = false;
    public float attackRange = 0.0f;
    private float _currentHealth = 0.0f;
    private float deathClipLength=1.0f;

    private void Awake()
    {

        _animator = gameObject.GetComponent<Animator>();
        if(_animator == null)
        {
            Debug.LogError("animator compoinaent does exists");
            return;
        }
        //NavMeshAgent.Warp();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();
        if (_agent != null)
        {
            if (isDead)
            {
                _agent.isStopped = true;

            }
            else
            {
                player = GameObject.FindWithTag("Player").transform;

                if (player == null)
                {
                    return;
                }

                if (Vector3.SqrMagnitude(_agent.transform.position - player.transform.position) < attackRange * attackRange)
                {
                    _agent.isStopped = true;
                    _agent.transform.LookAt(player);

                }
                else
                {
                    _agent.isStopped = false;
                    _agent.SetDestination(player.position);
                }
            }
        }
    }

    private void UpdateAnimation()
    {
        _animator.SetFloat("EnemySpeed", _agent.velocity.magnitude * dividedSpeed);
        _animator.SetBool("Isdead", isDead);
    }

    public void Init()
    {
        _currentHealth = maxHealth;

        _agent = gameObject.AddComponent<NavMeshAgent>();

        if (_agent != null)
        {
            _agent.speed = maxSpeed;
        }
        dividedSpeed = 1 / maxSpeed;

        AnimationClip[] animations = _animator.runtimeAnimatorController.animationClips;
        if (animations == null || animations.Length <= 0)
        {
            Debug.Log("animations Error");
            return;
        }

        for (int i = 0; i < animations.Length; ++i)
        {
            if (animations[i].name == "Death From Right")
            {
                deathClipLength = animations[i].length;
                break;
            }
        }
    }

    public float GetHealth()
    {
        return _currentHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if(_currentHealth <= 0.0f)
        {
            StartCoroutine("Kill");
        }
    }

    public IEnumerator Kill()
    {
        isDead = true;

        yield return new WaitForSeconds(deathClipLength);
        ResetAndRecycle();
    }

   private void ResetAndRecycle()
   {
        isDead = false;
        _currentHealth = maxHealth;
        transform.rotation = Quaternion.identity;
        Destroy(_agent);
        isDestroy = true;
        if (isDestroy)
        {
            print("Ai was destroyed");
            LevelManger.Instance.deathcount++;
        }
        ServiceLocator.Get<ObjectPoolManager>().RecycleObject(gameObject);
        isDestroy = false;
    }
}
