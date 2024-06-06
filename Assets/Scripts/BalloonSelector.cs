using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSelector : MonoBehaviour
{
    public GameObject[] balloons;
    [SerializeField] private SpriteRenderer indicator;

    public static GameObject selectedBalloon;

    private void Start()
    {
        selectedBalloon = balloons[Random.Range(0, balloons.Length)];
        indicator.sprite = selectedBalloon.GetComponent<SpriteRenderer>().sprite;
        indicator.color = selectedBalloon.GetComponent<SpriteRenderer>().color;
    }

    public void SelectBalloon()
    {
        selectedBalloon = balloons[Random.Range(0, 1160)%balloons.Length];
        for(int i = 0; i < balloons.Length; i++)
        {
            selectedBalloon = balloons[Random.Range(0, balloons.Length)];
        }
        ShowSelectedIndicator();
    }
    public void ShowSelectedIndicator()
    {
        indicator.sprite = selectedBalloon.GetComponent<SpriteRenderer>().sprite;
        indicator.color = selectedBalloon.GetComponent<SpriteRenderer>().color;
    }
}
