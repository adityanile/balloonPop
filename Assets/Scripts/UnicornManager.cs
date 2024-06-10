using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornManager : MonoBehaviour
{
    [SerializeField]
    private BalloonBehavior[] targets;

    [SerializeField]
    private List<int> killableTargets;
    private PlayerController player;

    public float selfDestructionTime = 4f;
    public float killeAfterTime = 1.5f;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        // Blocking Player movement so that player does not spawn more balloons
        player.blockMovement = true;

        int count = GameObject.FindObjectsOfType<BalloonBehavior>().Length;

        targets = new BalloonBehavior[count];
        targets = GameObject.FindObjectsOfType<BalloonBehavior>();

        // Get Ramdomly how many targets to kill and then kill them
        killableTargets = GetTargetsIndex(HowManyToKill());

        StartCoroutine(KillSelectedTargets());
    }

    IEnumerator KillSelectedTargets()
    {
        yield return new WaitForSeconds(killeAfterTime);

        if (targets.Length > 0)
        {
            foreach (var target in killableTargets)
            {
                if (targets[target])
                {
                    Destroy(targets[target].gameObject);
                }
            }
        }
        StartCoroutine(DestroyYourself());
    }

    IEnumerator DestroyYourself()
    {
        yield return new WaitForSeconds(selfDestructionTime);
            
        player.blockMovement = false;
        Destroy(gameObject.transform.parent.gameObject);
    }

    List<int> GetTargetsIndex(int n)
    {
        List<int> indexes = new List<int>();

        int index = Random.Range(0, targets.Length);
        indexes.Add(index);

        for (int i = 0; i < n - 1; i++)
        {
            index = Random.Range(0, targets.Length);

            while (indexes.IndexOf(index) != -1)
            {
                index = Random.Range(0, targets.Length);
            }
            indexes.Add(index);
        }
        return indexes;
    }

    int HowManyToKill()
    {
        return (Random.Range(0, targets.Length));
    }
}
