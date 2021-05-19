using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAiming : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Rig weaponAim;
    private float aimDuration = 0.2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1)) {
            weaponAim.weight += Time.deltaTime / aimDuration;
        } else {
            weaponAim.weight -= Time.deltaTime / aimDuration;
        }
        
    }
}
