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

    // fairy Spawing Managing
    [HideInInspector] public bool bringFairy = false;
    [SerializeField] private GameObject fairy;
    private GameObject currentFairy;
    public Transform fairyPos;
    public float fairyTime = 4f;

    // Astaunaut Management here
    [HideInInspector] public bool bringAstonaut = false;
    public GameObject[] astonauts;
    public Transform astonautSpawnPos;
    private GameObject currentAstonaut;
    public float astoTime = 4f;

    // Unicorn spawing managed here
    [HideInInspector] public bool bringUnicorn = false;
    public GameObject unicorn;
    public Transform unicornSpawnPos;

    // This is checked before moving the player
    public bool blockMovement = false;

    private SpriteRenderer sr;

    private float moveDirInp;
    private readonly float waitTimer = 1f;
    private float waitTime;

    private bool canSpawnBalloon = true;

    [HideInInspector] public bool shouldStartSpecialAttack1 = false;
    [HideInInspector] public bool shouldStartSpecialAttack2 = false;

    public BalloonSelector balloonSelector;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (charPanel.activeInHierarchy)
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

        indicator.color = BalloonSelector.selectedBalloon.GetComponent<BalloonBehavior>().balloonColor;
        if (shouldStartSpecialAttack1)
        {
            shouldStartSpecialAttack1 = false;
            StartSpecialAttack1();
        }
        if (shouldStartSpecialAttack2)
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

        if (bringAstonaut)
        {
            bringAstonaut = false;
            AstonautPowerUp();
        }

        if (bringUnicorn)
        {
            bringUnicorn = false;
            UnicornPowerUp();
        }
    }
    
    // Spawn A Balloon
    public void SpawnABalloon()
    {
        if (BalloonSelector.selectedBalloon != null && canSpawnBalloon && !EventSystem.current.IsPointerOverGameObject())
        {
            Instantiate(BalloonSelector.selectedBalloon, balloonPosition.position, BalloonSelector.selectedBalloon.transform.rotation, balloonsParent);
            canSpawnBalloon = false;
            selector.SelectBalloon();
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

        currentFairy = Instantiate(fairy, fairyPos.position, Quaternion.identity, fairyPos);

        Invoke("AfterFairyPowerUp", fairyTime);
    }
    void AfterFairyPowerUp()
    {
        ShowIndicator();
        Destroy(currentFairy);
    }

    // Starting Astonaut Here
    void AstonautPowerUp()
    {
        blockMovement = true;
        int index = Random.Range(0, astonauts.Length);
        currentAstonaut = Instantiate(astonauts[index], astonautSpawnPos.position, Quaternion.identity, astonautSpawnPos);

        Invoke("AfterAstonauts", astoTime);
    }

    void AfterAstonauts()
    {
        blockMovement = false;
        Destroy(currentAstonaut);
    }

    void UnicornPowerUp()
    {
        Instantiate(unicorn, unicornSpawnPos.position, Quaternion.identity, unicornSpawnPos);
    }

}
