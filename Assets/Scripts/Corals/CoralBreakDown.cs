using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralBreakDown : Coral
{
    
    //private Renderer renderer1;
    //private Renderer renderer2;
    private Renderer renderer3;
 
    //private Material uniqueMaterial1;
    //private Material uniqueMaterial2;
    private Material uniqueMaterial3;
    private Color targetColor;
    private PostProcessGray grayscale;
    private float gameover;
    //GameObject coral1;
    //GameObject coral2;
    GameObject coral3;
    spawnWaste wasteSpawner;
	
	private int callCount = 0;
	
    // Start is called before the first frame update
    public override void startColor()
    {
        //coral1 = this.gameObject.transform.GetChild(0).gameObject;
        //coral2 = this.gameObject.transform.GetChild(1).gameObject;
        coral3 = this.gameObject.transform.GetChild(2).gameObject;
        //renderer1 = coral1.GetComponent<Renderer>();
        //renderer2 = coral2.GetComponent<Renderer>();
        renderer3 = coral3.GetComponent<Renderer>();
		
		if(this.gameObject.name.Contains("Blue")){
			renderer3.sharedMaterial.color = new Color(0.0f, 0.3f, 1.0f);
		} else if(this.gameObject.name.Contains("Orange")){
			renderer3.sharedMaterial.color = new Color(0.75f, 0.5f, 0.0f);
		} else if(this.gameObject.name.Contains("Purple")){
			renderer3.sharedMaterial.color = new Color(0.5f, 0.1f, 0.5f);
		} else if(this.gameObject.name.Contains("White")){
			renderer3.sharedMaterial.color = new Color(1.0f, 1.0f, 0.7f);
		} else if(this.gameObject.name.Contains("Yellow")){
			renderer3.sharedMaterial.color = new Color(1.0f, 0.85f, 0.0f);
		}
		
		
        //uniqueMaterial1 = renderer1.material;
        //uniqueMaterial2 = renderer2.material;
        uniqueMaterial3 = renderer3.sharedMaterial;
        targetColor = new Color(0.5849f,0.5849f,0.5849f);
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
			grayscale.aliveCorals -= groupCount;
        }
		/*
        if (callCount == 3) {
            grayscale.aliveCorals -= 1;
        }*/
    }
}
