﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System.Linq;
using Rnd = UnityEngine.Random;

//Coded by blananas2
public class FakeFestivePianoKeys : ImpostorMod 
{
    [SerializeField]
    private TextMesh display;
    void Start()
    {
        flickerObjs.Add(display.gameObject);
        string set = "mB\"%x*v^w>";
        List<string> symbols = new List<string>();
        PKretry:
        for (int i = 0; i < 3; i++) {
            symbols.Add(set.PickRandom().ToString());
        }
        if (symbols.HasDuplicates()) {
            Log("the display has identical symbols");
        } else {
            symbols.Clear();
            goto PKretry;
        }
        display.text = symbols.Join();
    }
}
