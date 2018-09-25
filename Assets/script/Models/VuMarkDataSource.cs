using System;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Vuforia.Scripts.Models {
    public class VuMarkDataSource {

        private VuMarkTarget _vuMark;
        private int data = 0;

        private string _baseUrl;
        private string _clientId;
        private string _clientSecret; // to be moved
        private string _token;

        public VuMarkDataSource(VuMarkTarget target){
            // initiate class
            _vuMark = target;
        }

        private void RefreshToken(){
            // refresh token from API after certain amount of time
        }

        public string GetData(){
            // get json file from api
            return "";
        }

        public int getData() {
            data++;
            return data;
        }


    }
}