using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour
{
    #region Variables
    [HideInInspector] public NavMeshAgent npcAgent;
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] NPC_STATE currentState;
    //Animator npcAnim;
    public float walkingSpeed;

    public enum NPC_STATE
    {
        Idle,
        Walking
    }
    [Header("Idle State")]
    public float idleTime;
    [Tooltip("X is min value, Y is max value")]
    public Vector2 minMaxIdleTime;
    float elapsedIdleTime;
    #endregion

    private void Awake()
    {
        npcAgent = GetComponent<NavMeshAgent>();
        //npcAnim = GetComponent<Animator>();
    }
    private void Start()
    {
        idleTime = Random.Range(minMaxIdleTime.x, minMaxIdleTime.y);
    }
    private void Update()
    {
        switch (currentState) //Overkill, pero es para acostumbrarme - Catto
        {
            case NPC_STATE.Idle: //Estado de idle
                idleTime = Random.Range(minMaxIdleTime.x, minMaxIdleTime.y);
                elapsedIdleTime += Time.deltaTime;
                if (elapsedIdleTime >= idleTime)
                {
                    elapsedIdleTime = 0;
                    ChangeState(NPC_STATE.Walking);
                }
                break;

            case NPC_STATE.Walking:
                if (npcAgent.remainingDistance <= npcAgent.stoppingDistance)
                {
                    currentState = NPC_STATE.Idle;
                }
                break;
        }
    }
    void ChangeState(NPC_STATE newState)
    {
        currentState = newState;
        if (currentState == NPC_STATE.Walking)
        {
            npcAgent.SetDestination(patrolPoints[Random.Range(0, patrolPoints.Length)].position);
            npcAgent.speed = walkingSpeed;
        }
    }
}
