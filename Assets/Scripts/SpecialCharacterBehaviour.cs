using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCharacterBehaviour : MonoBehaviour
{
    private PlayerController player;
    private GameObject targetParent;
    private GameObject[] targets;
    [SerializeField] private GameObject laser;
    private List<GameObject> targetsList;

    private void OnEnable()
    {
        player = FindObjectOfType<PlayerController>();
        player.enabled = false;
        targetsList = new List<GameObject>();
    }
    void Start()
    {
        StartCoroutine(DestroyEachTarget());
    }
    
    private IEnumerator DestroyEachTarget()
    {
        yield return new WaitForSecondsRealtime(2.5f);

        targetParent = GameObject.Find("SpawnedBalloons");
        targets = new GameObject[targetParent.transform.childCount];
        if(targetParent != null)
        {
            for(int i = 0; i < targetParent.transform.childCount; i++)
            {
                targets[i] = targetParent.transform.GetChild(i).gameObject;
            }

        }

        Debug.Log("targets are " +  targets.Length);
        foreach(GameObject target in targets)
        {
            if (target != null)
            {
                target.GetComponent<BalloonBehavior>().enabled = false;
                GameObject tempLaser = Instantiate(laser);
                tempLaser.GetComponent<DartBehavior>().target = target.transform;
                tempLaser.GetComponent<DartBehavior>().shouldMove = true;
                Vector3 offset = target.transform.position - laser.transform.position;
                tempLaser.transform.SetPositionAndRotation(player.transform.GetChild(1).transform.position, Quaternion.LookRotation(transform.forward, offset));
            }
            else
                continue;
            yield return new WaitForSecondsRealtime(1f);
        }
        yield return new WaitForSecondsRealtime(1f);
        player.enabled = true;
        player.ShowIndicator();
        Destroy(gameObject);
    }
}
