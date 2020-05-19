using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreaperBehaviour : MonoBehaviour
{
    public enum State { Idle, Patrol, Chase, Explode, Dead };
    public State state;

    private NavMeshAgent agent;
    private Animator anim;
    [SerializeField] SoundPlayer sound;

    [Header("Creeper properties")]
    public int life = 5;

    [Header("Target Detection")]
    public float radius;
    public float idleRadius;
    public float chaseRadius;
    public LayerMask targetMask;
    public bool targetDetected = false;
    private Transform targetTransform;

    [Header("Patrol path")]
    public bool stopAtEachNode = true;
    public float timeStopped = 1.0f;
    private float timeCounter = 0.0f;
    public Transform[] pathNodes;
    private int currentNode = 0;
    private bool nearNode = false;

    [Header("Explosion properties")]
    public float explodeDistance;
    public float explosionRadius;
    public float explosionForce;
    public ParticleSystem explosionPS;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        nearNode = true;
        SetIdle();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Explode:
                Explode();
                break;
            case State.Dead:
                Dead();
                break;
            default:
                break;
        }
    }
    private void FixedUpdate()
    {
        targetDetected = false;
        Collider[] cols = Physics.OverlapSphere(this.transform.position, radius, targetMask);
        if (cols.Length != 0)
        {
            targetDetected = true;
            targetTransform = cols[0].transform;
        }
    }

    void Idle()
    {
        if (targetDetected)
        {
            SetChase();
            return;
        }

        if (timeCounter >= timeStopped)
        {
            if (nearNode) GoNearNode();
            else GoNextNode();
            SetPatrol();
        }
        else timeCounter += Time.deltaTime;
    }
    void Patrol()
    {
        if (targetDetected)
        {
            SetChase();
            return;
        }

        if (agent.remainingDistance <= 0.1f)
        {
            if (stopAtEachNode) SetIdle();
            else GoNextNode();
        }
    }
    void Chase()
    {
        if (!targetDetected)
        {
            nearNode = true;
            SetIdle();
            return;
        }
        agent.SetDestination(targetTransform.position);

        Debug.LogError("agent distance: " + agent.remainingDistance);
        Debug.LogError("distance: " + Vector3.Distance(transform.position, targetTransform.position));
        if (Vector3.Distance(transform.position, targetTransform.position) <= explodeDistance)
        {
            SetExplode();
        }


    }
    void Explode() { }
    void Dead() { }

    void SetIdle()
    {
        agent.isStopped = true;
        anim.SetBool("walking", false);
        radius = idleRadius;
        timeCounter = 0;

        int random = Random.Range(0, 4);
        sound.Play(0, 1);

        state = State.Idle;
    }
    void SetPatrol()
    {
        agent.isStopped = false;
        agent.stoppingDistance = 0;
        radius = idleRadius;
        anim.SetBool("walking", true);

        state = State.Patrol;
    }
    void SetChase()
    {
        agent.isStopped = false;
        agent.SetDestination(targetTransform.position);
        agent.stoppingDistance = 2.0f;
        anim.SetBool("walking", true);
        radius = chaseRadius;

        state = State.Chase;
    }
    void SetExplode()
    {
        sound.Play(0, 1);
        agent.isStopped = true;
        anim.SetTrigger("muerto");

        state = State.Explode;
    }
    void SetDead()
    {
        sound.Play(0, 1);
        Destroy(this.gameObject);
        state = State.Dead;
    }

    void GoNextNode()
    {
        currentNode++;
        if (currentNode >= pathNodes.Length) currentNode = 0;

        agent.SetDestination(pathNodes[currentNode].position);
    }
    void GoNearNode()
    {
        nearNode = false;
        float minDistance = Mathf.Infinity;
        for (int i = 0; i < pathNodes.Length; i++)
        {
            if (Vector3.Distance(transform.position, pathNodes[i].position) < minDistance)
            {
                minDistance = Vector3.Distance(transform.position, pathNodes[i].position);
                currentNode = i;
            }
        }
        agent.SetDestination(pathNodes[currentNode].position);
    }

    public void Explosion()
    {
        Collider[] cols = Physics.OverlapSphere(this.transform.position, explosionRadius);
        foreach (Collider c in cols)
        {
            if (c.attachedRigidbody != null)
            {
                c.attachedRigidbody.AddExplosionForce(explosionForce, this.transform.position, explosionRadius, 1, ForceMode.Impulse);
            }
        }
        int random = Random.Range(4, 8);
        sound.Play(0, 1);
        explosionPS.Play();
        explosionPS.transform.parent = null;

        SetDead();
    }
    private void OnDrawGizmos()
    {
        Color color = Color.blue;
        if (targetDetected) color = Color.red;
        color.a = 0.1f;
        Gizmos.color = color;
        Gizmos.DrawSphere(this.transform.position, radius);
    }

    public void Damage(int hit)
    {
        Debug.Log("Creeper damage");
        if (state == State.Dead) return;
        life -= hit;
        if (life <= 0) SetDead();
    }

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Shot")
        {
            Damage(5);
        }
    }
}
