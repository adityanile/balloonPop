using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private Vector3 fp;
    private Vector3 lp;
    private float dragDistance;

    public float speed = 10f;
    [SerializeField] private Transform balloonPosition;
    private SpriteRenderer sr;

    private PlayerController playerController;

    void Start()
    {
        dragDistance = Screen.height * 15 / 100;
        sr = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!playerController.blockMovement)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0); // get the touch

                if (touch.phase == TouchPhase.Began) //check for the first touch
                {
                    fp = touch.position;
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    lp = touch.position;

                    Vector3 pos = Camera.main.ScreenToWorldPoint(lp);

                    transform.position = new Vector3(pos.x, transform.position.y, transform.position.z);
                    balloonPosition.localPosition = new Vector3(pos.x * .5f, 0, 0);
                    sr.flipX = balloonPosition.localPosition.x < 0;

                    // Restictions from falling down
                    if (transform.position.x <= -1.5f)
                    {
                        Vector3 temp = transform.position;
                        temp.x = -1.5f;
                        transform.position = temp;
                    }
                    else if (transform.position.x >= 1.5f)
                    {
                        Vector3 temp = transform.position;
                        temp.x = 1.5f;
                        transform.position = temp;
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    // Spawn a balloon when finger is removed
                    playerController.SpawnABalloon();
                }
            }
        }
    }
}
