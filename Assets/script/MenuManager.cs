using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Vuforia;

namespace script{
    public class MenuManager : MonoBehaviour{
        
        private Dictionary<string, GameObject> menus;

        private void Start(){
            menus = new Dictionary<string, GameObject>();
        }

        public void Remove(string key){
            menus.Remove(key);
        }
        
        public void Add(){
            var vuMark = GameObject.Find("VuMark").GetComponent<VuMarkBehaviour>().VuMarkTarget.InstanceId.StringValue;
            
            foreach (var canvas in menus) if (canvas.Key == vuMark)return;  
           
            var prefab = AssetDatabase.LoadAssetAtPath("Assets/script/Models/VuMarkUiComponent.prefab", typeof(GameObject));
            menus.Add(vuMark,Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject);
        }

    }
}