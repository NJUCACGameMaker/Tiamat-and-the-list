using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightEquipment : Equipment {

    public Sprite sprite;

    public void Start()
    {
        type = EquipmentType.FlashLight;
    }

    public void TurnOnTorch()
    {
        SpriteRenderer torch = GetComponent<SpriteRenderer>();
        torch.color = new Color(torch.color.r, torch.color.g, torch.color.b, 0.3f);
        this.gameObject.AddComponent<SpriteMask>();
        SpriteMask mask = transform.GetComponent<SpriteMask>();
        mask.sprite = sprite;
        mask.alphaCutoff = 0.228f;
        mask.spriteSortPoint = SpriteSortPoint.Center;
    }

    public void TurnOffTorch()
    {
        SpriteRenderer torch = GetComponent<SpriteRenderer>();
        torch.color = new Color(torch.color.r, torch.color.g, torch.color.b, 0f);
        SpriteMask mask = transform.GetComponent<SpriteMask>();
        Destroy(mask);
    }
}
