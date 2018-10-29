﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightEquipment : Equipment {



    public void Start()
    {
        type = EquipmentType.FlashLight;
    }

    public void TurnOnTorch()
    {
        SpriteRenderer torch = GetComponent<SpriteRenderer>();
        torch.color = new Color(torch.color.r, torch.color.g, torch.color.b, 0.5f);
    }

    public void TurnOffTorch()
    {
        SpriteRenderer torch = GetComponent<SpriteRenderer>();
        torch.color = new Color(torch.color.r, torch.color.g, torch.color.b, 0f);
    }
}