using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[ExecuteInEditMode]
public class BlendShapeTranslator : MonoBehaviour
 {
 
     public bool Apply; //"run" or "generate" for example
     public string file_path;

     public string Local_Mouth;
     string Ext_Mouth = "Fcl_MTH_O";
 
     void Update()
     {
         if (Apply)
            readTextFile(file_path);
         Apply = false;
     }
 
    void readTextFile(string file_path)
    {
        
        string text = File.ReadAllText(file_path);
        print(Local_Mouth + " " + Ext_Mouth);
        text = text.Replace(Ext_Mouth, Local_Mouth);
        File.WriteAllText(file_path, text);
        print("Success");
        //AssetDatabase.Refresh();
    }

 }