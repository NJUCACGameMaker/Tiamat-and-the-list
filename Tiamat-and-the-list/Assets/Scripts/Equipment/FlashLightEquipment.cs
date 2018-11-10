using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightEquipment : Equipment {

    public GameObject torchMaskPrefab;

    public void Start()
    {
        type = EquipmentType.FlashLight;
    }

    public void TurnOnTorch()
    {
        SpriteRenderer torch = GetComponent<SpriteRenderer>();
        torch.color = new Color(torch.color.r, torch.color.g, torch.color.b, 0.3f);
        GameObject torchMask = Instantiate(torchMaskPrefab) as GameObject;
        torchMask.transform.position = transform.position;
        torchMask.transform.parent = transform;

    }

    public void TurnOffTorch()
    {
        SpriteRenderer torch = GetComponent<SpriteRenderer>();
        torch.color = new Color(torch.color.r, torch.color.g, torch.color.b, 0f);
        while (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
