using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
 
public class ConstraintRotation : MonoBehaviour 
{ 
    // Start is called before the first frame update 
 
    private float rotationx; 
    private float rotationy; 

    public float distance;
 
    public Transform mainCamera; 
    void Start() 
    { 
         
    } 
 
    // Update is called once per frame 
    void Update() 
    { 
        transform.position = mainCamera.transform.position + mainCamera.transform.forward
         * distance;

        rotationx = Mathf.Clamp(mainCamera.transform.rotation.x, -20f, 20f);
        rotationy = Mathf.Clamp(mainCamera.transform.rotation.y, -20f, 20f); 
 
        transform.rotation = Quaternion.Euler(rotationx, rotationy, mainCamera.transform.rotation.z); 
    } 
} 