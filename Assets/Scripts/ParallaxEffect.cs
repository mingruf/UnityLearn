using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxEffect : MonoBehaviour
{
    public new Camera camera;
    public Transform followTarget;
    // Start is called before the first frame update
    Vector2 startingPosition;
    Vector2 camMoveSinceStart => (Vector2) camera.transform.position - startingPosition;
    float distanceFromTarget => transform.position.z - followTarget.position.z;
    float clippingPlane => (camera.transform.position.z + (distanceFromTarget > 0 ? camera.farClipPlane : camera.nearClipPlane));
    float parallalFactor => Mathf.Abs(distanceFromTarget) / clippingPlane;
    float startingZ;
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
        
    }
    
    
    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallalFactor;

        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
