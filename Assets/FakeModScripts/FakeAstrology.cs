﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class FakeAstrology : ImpostorMod 
{
    public override string ModAbbreviation { get { return "As"; } }
    public MeshRenderer[] displays;
    public Texture[] elements, planets, zodiacs;
    public TextMesh[] buttonLabels;
    int change;
    void Start()
    {
        displays[0].material.mainTexture = elements.PickRandom();
        displays[1].material.mainTexture = planets.PickRandom();
        displays[2].material.mainTexture = zodiacs.PickRandom();
        if (Ut.RandBool())
        {
            switch (Rnd.Range(0, 3))
            {
                case 0:
                    change = Rnd.Range(1, 3);
                    displays[change].material.mainTexture = elements.PickRandom();
                    AddFlicker(displays[change]);
                    LogQuirk("there is a duplicate element");
                    break;
                case 1:
                    change = Rnd.Range(0, 1) * 2;
                    displays[change].material.mainTexture = planets.PickRandom();
                    AddFlicker(displays[change]);
                    LogQuirk("there is a duplicate planet");
                    break;
                case 2:
                    change = Rnd.Range(0, 1);
                    displays[change].material.mainTexture = zodiacs.PickRandom();
                    AddFlicker(displays[change]);
                    LogQuirk("there is a duplicate zodiac");
                    break;
            }
        }
        else
        {
            if (Ut.RandBool())
            {
                buttonLabels[0].text = "good\nomen";
                AddFlicker(buttonLabels[0]);
                LogQuirk("the poor omen button says \"good omen\"");
            }
            else
            {
                buttonLabels[2].text = "poor\nomen";
                AddFlicker(buttonLabels[2]);
                LogQuirk("the good omen button says \"poor omen\"");
            }
        }
    }
}
