using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform balloonPosition;
    [SerializeField] private Transform specialCharPos;
    [SerializeField] private BalloonSelector selector;
    [SerializeField] private OnCollisionNewBalloonSpawner spawner;
    [SerializeField] private SpriteRenderer indicator;
    [SerializeField] private Transform balloonsParent;
    [SerializeField] private GameObject charPanel;
    [SerializeField] private GameObject specialCharacter1;
    [SerializeField] private GameObject specialCharacter2;

    [SerializeField] private GameObject fairy;
    private GameObject currentFairy;
    public Transform fairyPos;
    public float fairyTime = 4f;

    private SpriteRenderer sr;

    private float moveDirInp;
    private readonly float waitTimer = 1f;
    private float waitTime;

    private bool canSpawnBalloon = true;
    
    [HideInInspector] public bool shouldStartSpecialAttack1 = false;
    [HideInInspector] public bool shouldStartSpecialAttack2 = false;
    [HideInInspector] public bool bringFairy = false;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(charPanel.activeInHierarchy)
        {
            spawner.enabled = false;
        }
        else
        {
            spawner.enabled = true;
        }
        if (!canSpawnBalloon)
            waitTime += Time.deltaTime;
        if (waitTime > waitTimer)
        {
            canSpawnBalloon = true;
            waitTime = 0;
        }
        moveDirInp = Input.GetAxisRaw("Horizontal");

        if(moveDirInp != 0)
        {
            transform.Translate(moveDirInp * moveSpeed * Time.deltaTime * transform.right);
            balloonPosition.localPosition = new Vector3(moveDirInp * .5f, 0, 0);
            sr.flipX = balloonPosition.localPosition.x < 0;
        }

        if(transform.position.x <= -1.5f)
        {
            Vector3 temp = transform.position;
            temp.x = -1.5f;
            transform.position = temp;
        }else if (transform.position.x >= 1.5f)
        {
            Vector3 temp = transform.position;
            temp.x = 1.5f;
            transform.position = temp;

        }

        if (Input.GetMouseButtonDown(0) && BalloonSelector.selectedBalloon != null && canSpawnBalloon && !EventSystem.current.IsPointerOverGameObject())
        {
            Instantiate(BalloonSelector.selectedBalloon,balloonPosition.position, BalloonSelector.selectedBalloon.transform.rotation, balloonsParent);
            canSpawnBalloon = false;
            selector.SelectBalloon();
        }

        indicator.color = BalloonSelector.selectedBalloon.GetComponent<BalloonBehavior>().balloonColor;
        if(shouldStartSpecialAttack1)
        {
            shouldStartSpecialAttack1 = false;
            StartSpecialAttack1();
        }
        if(shouldStartSpecialAttack2)
        {
            shouldStartSpecialAttack2 = false;
            StartSpecialAttack2();
        }

        // Starting fairy powerUp here
        if (bringFairy)
        {
            bringFairy = false;
            FairyPowerup();
        }
    }

    public void ChangeCharacter(Image img)
    {
        transform.GetComponent<SpriteRenderer>().sprite = img.sprite;
    }

    public void StartSpecialAttack1()
    {
        indicator.gameObject.SetActive(false);
        Instantiate(specialCharacter1, specialCharPos.position, Quaternion.identity, transform);
    }
    public void StartSpecialAttack2()
    {
        indicator.gameObject.SetActive(false);
        Instantiate(specialCharacter2, specialCharPos.position, Quaternion.identity, transform);
        Invoke("ShowIndicator", 3.5f);
    }
    public void ShowIndicator()
    {
        indicator.gameObject.SetActive(true);
    }

    // Fairy Attack Managed Here
    void FairyPowerup()
    {
        indicator.gameObject.SetActive(false);

        currentFairy = Instantiate(fairy, fairyPos.position, Quaternion.identity,fairyPos);

        Invoke("AfterFairyPowerUp", fairyTime);
    }
    void AfterFairyPowerUp()
    {
        ShowIndicator();
        Destroy(currentFairy);
    }

}
