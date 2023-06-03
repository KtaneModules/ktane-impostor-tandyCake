﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public class FakeNeutralization : ImpostorMod
{
    public override string ModAbbreviation { get { return "Neut"; } }
    public TextMesh baseDisp, titrateDisp;
    public TextMesh[] tubeLabels;
    public Transform meterTF;
    public MeshRenderer meter;
    public TextMesh cbText;

    private int Case;
    private static readonly string[] bases = { "NH₃", "LiOH", "NaOH", "KOH" };
    private static readonly string[] acids = { "HF", "HCl", "HBr", "HI", "H₂SO₄", "HNO₃", "H₂CO₃" };
    private static readonly Color32[] colors = { Color.red, Color.green, Color.yellow, Color.blue, new Color32(252, 3, 202, 255), new Color32(207, 3, 252, 255), new Color32(252, 123, 3, 255), };
    private static readonly string[] colorNames = { "Red", "Green", "Yellow", "Blue", "Pink", "Purple", "Orange" };
    private static readonly float[] standardScales = { 1.43f, 3.73f, 6.12f, 8.44f };
    private static readonly float[] offScales = { 0, 2.7f, 5, 7.3f, 9.6f };
    private static readonly string[] offScaleNames = { "0", "7.5", "12.5", "17.5", "22.5" };

    void Start()
    {
        baseDisp.text = bases.PickRandom();
        int chosenColor = Rnd.Range(0,4);
        meterTF.localScale = new Vector3(22.2222f, 50, standardScales.PickRandom());

        Case = Rnd.Range(0, 5);
        
        switch (Case)
        {
            case 0:
                baseDisp.text = acids.PickRandom();
                LogQuirk("the base display has an acid: " + baseDisp.text);
                AddFlicker(baseDisp);
                break;
            case 1:
                chosenColor = 4 + Rnd.Range(0, 3);
                LogQuirk("the acid color is " + colorNames[chosenColor].ToLower());
                AddFlicker(meter);
                break;
            case 2:
                int chosenScale = Rnd.Range(0, 5);
                meterTF.localScale = new Vector3(22.2222f, 50, offScales[chosenScale]);
                LogQuirk("the acid level is {0}mL", offScaleNames[chosenScale]);
                AddFlicker(meter);
                break;
            case 3:
                float[] xPositions = new float[4];
                for (int i = 0; i < 4; i++)
                {
                    xPositions[i] = tubeLabels[3 - i].transform.localPosition.x;
                    tubeLabels[i].text = (5 * (4 - i)).ToString();
                }
                for (int i = 0; i < 4; i++)
                {
                    Vector3 orig = tubeLabels[i].transform.localPosition;
                    tubeLabels[i].transform.localPosition = new Vector3(xPositions[i], orig.y, orig.z);
                }
                AddFlicker(tubeLabels);
                LogQuirk("the measurement labels are reversed");
                break;
            case 4:
                titrateDisp.text = "Castrate";
                LogQuirk("the titrate button says castrate");
                AddFlicker(titrateDisp);
                break;

        }

        meter.material.color = colors[chosenColor];
        cbText.text = colorNames[chosenColor];
    }
    protected override void OnColorblindToggle()
    {
        cbText.gameObject.SetActive(cb);
    }
}
