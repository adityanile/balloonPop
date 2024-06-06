using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnCollisionNewBalloonSpawner : MonoBehaviour
{
    public Image charMask;
    [SerializeField] private Image charImage;
    [SerializeField] private Image charUI_Image;
    [SerializeField] private Transform balloonsParent;
    [SerializeField] private GameObject newCharPanel;
    [SerializeField] private GameObject[] balloons;
    [SerializeField] private Sprite[] characters;

    [HideInInspector] public bool shouldSpawnBalloon = false;
    [HideInInspector] public int balloonNumber;
    private int charIndex = 1;

    void Update()
    {
        if (shouldSpawnBalloon)
        {
            SpawnNewBalloon();
        }
    }


    public void SpawnNewBalloon()
    {
        shouldSpawnBalloon = false;
        
        if(balloonNumber < balloons.Length)
        {
            GameObject balloon =  Instantiate(balloons[balloonNumber], transform.position, Quaternion.identity);
            balloon.transform.parent = balloonsParent;
            balloon.transform.GetChild(0).gameObject.SetActive(true);
        }
        
    }

    public void StartNewCharacterPanel()
    {
        newCharPanel.SetActive(true);
        SetCharacterImage((charIndex++) % characters.Length);
    }

    public void SetCharacterImage(int index)
    {
        charImage.sprite = characters[index];
        charUI_Image.sprite = characters[index];
    }
}
