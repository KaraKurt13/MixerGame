using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingBackround : MonoBehaviour
{
    [SerializeField] private float speedX;
    [SerializeField] private float speedY;
    private RawImage backgroundImage;

    void Start()
    {
        backgroundImage = GetComponent<RawImage>();
    }

    
    void Update()
    {
        backgroundImage.uvRect = new Rect(backgroundImage.uvRect.x + speedX*Time.deltaTime, backgroundImage.uvRect.y + speedY*Time.deltaTime,1,1);
    }
}
