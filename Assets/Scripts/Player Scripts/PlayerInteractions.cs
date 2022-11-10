using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float rayDisance = 7f;
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private PlayerUI playerUI;
    private Gun gunAllowance;
    public enum GunLevel { ONCLICK, HOLDDOWN }
    GunLevel level = GunLevel.ONCLICK;

    public float damage = 10f;
    public float range = 50f;


    // Start is called before the first frame update
    void Start()
    {
        playerUI = GetComponent<PlayerUI>();
        gunAllowance = GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        LookingAround();
        LetsShoot();

    }

    void LookingAround()
    {
        bool isMouseoverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDisance, Color.green);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, rayDisance, mask) && !isMouseoverUI())
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.DefaultInteract();
                }
            }
            else if (isMouseoverUI() == true)
            {
                return;
            }


        }
    }

    public void LetsShoot()
    {
        if (BuyScript.isPaused == false)
        {
            if (level == GunLevel.ONCLICK)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    gunAllowance.Shoot();
                }
            }
            else if (level == GunLevel.HOLDDOWN)
            {
                if (Input.GetButton("Fire1"))
                {
                    gunAllowance.Shoot();
                }
            }
            else if (BuyScript.isPaused == true)
            {
                return;
            }
        }
    }

    public void AutomaticFire()
    {
        level = GunLevel.HOLDDOWN;
    }
}
