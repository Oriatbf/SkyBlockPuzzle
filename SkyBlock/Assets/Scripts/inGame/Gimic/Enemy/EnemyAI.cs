using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // 플레이어를 추적할 대상
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // 플레이어의 태그를 설정하세요.
    }

    void Update()
    {
        if (player != null)
        {
            // 플레이어를 추적하도록 설정
            navMeshAgent.SetDestination(player.position);
        }
    }
}