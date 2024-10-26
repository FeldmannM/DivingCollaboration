using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralShellBreakDown : Coral
{
    
    //private Renderer renderer1;
    //private Renderer renderer2;
    private Renderer renderer3;
 
    //private Material uniqueMaterial1;
    //private Material uniqueMaterial2;
    private Material uniqueMaterial3;
    //private Material uniqueMaterial4;
    //private Material uniqueMaterial5;
    private Material uniqueMaterial6;
    private Color targetColor;
    private PostProcessGray grayscale;
    private float gameover;
    //GameObject coral1;
    //GameObject coral2;
    GameObject coral3;
    //spawnWaste wasteSpawner;
	
	private int callCount = 0;

    public override void startColor()
    {
        //coral1 = this.gameObject.transform.GetChild(0).gameObject;
        //coral2 = this.gameObject.transform.GetChild(1).gameObject;
        coral3 = this.gameObject.transform.GetChild(2).gameObject;
        //renderer1 = coral1.GetComponent<Renderer>();
        //renderer2 = coral2.GetComponent<Renderer>();
        renderer3 = coral3.GetComponent<Renderer>();
		
		renderer3.sharedMaterials[0].color = new Color(0.65f, 0.55f, 0.5f);
		renderer3.sharedMaterials[1].color = new Color(0.0f, 0.6f, 1.0f);
		
		
        //uniqueMaterial1 = renderer1.materials[0];
        //uniqueMaterial2 = renderer2.materials[0];
        uniqueMaterial3 = renderer3.sharedMaterials[0];
        //uniqueMaterial4 = renderer1.materials[1];
        //uniqueMaterial5 = renderer2.materials[1];
        uniqueMaterial6 = renderer3.sharedMaterials[1];
        targetColor = new Color(1.0f,1.0f,1.0f);
        grayscale = GameObject.Find("Grayscale").GetComponent<PostProcessGray>();
        gameover = grayscale.gameover;
        //wasteSpawner =  GameObject.Find("WasteSpawner").GetComponent<spawnWaste>();
    }

    public override void changeColor(int groupCount)
    {
        callCount++;
        float lerpValue = callCount / 3.0f;

        if (callCount <= 3) {
            //uniqueMaterial1.color = Color.Lerp(uniqueMaterial1.color, targetColor, lerpValue);
            //uniqueMaterial2.color = Color.Lerp(uniqueMaterial2.color, targetColor, lerpValue);
            uniqueMaterial3.color = Color.Lerp(uniqueMaterial3.color, targetColor, lerpValue);
            //uniqueMaterial4.color = Color.Lerp(uniqueMaterial4.color, targetColor, lerpValue);
            //uniqueMaterial5.color = Color.Lerp(uniqueMaterial5.color, targetColor, lerpValue);
            uniqueMaterial6.color = Color.Lerp(uniqueMaterial6.color, targetColor, lerpValue);
			grayscale.aliveCorals -= groupCount;
        }
		/*
        if (callCount == 3) {
            grayscale.aliveCorals -= 1;
        }*/
    }

    
}
