using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customizer : MonoBehaviour
{
    public Button color;
    public Button Outfit; 

    public GameObject CurrMilena;
    public GameObject SpawnTransform;
    public GameObject OldMilena;
    public GameObject[] Milenas;
    public Texture2D[] ClothColors;
    public int ColorIterator=0;

    public Material ClothMaterial;

    [System.Serializable]
    public class ClothesClass{
         public List<GameObject> Clothes = new List<GameObject>();
    }

   public List<ClothesClass> AllClothes = new List<ClothesClass>();
    
    public int[] i;
    // Start is called before the first frame update
    void Start()
    {
        int itemCount=0;
        foreach (var item in AllClothes)
        {
            item.Clothes[i[0]].SetActive(true);
            i[itemCount]++;
            itemCount++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor()
    {

    }

    public void ChangeClothes(int cloth)
    {
        if (i[cloth] > 0)
        {
            AllClothes[cloth].Clothes[i[cloth]-1].SetActive(false);
            print(AllClothes[cloth].Clothes[i[cloth]-1].name+" fas");
        }
            
        if (i[cloth] ==  AllClothes[cloth].Clothes.Count)
            i[cloth]=0;

        
        AllClothes[cloth].Clothes[i[cloth]].SetActive(true);
        i[cloth]++; 
    }

    public void ChangeColors()
    {
        if (ColorIterator == ClothColors.Length)
            ColorIterator=0;

        ClothMaterial.SetTexture("_MainTex",ClothColors[ColorIterator]);

        
        ColorIterator++;
    }

}
