using System;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using Vuforia.Scripts.Models;
using Image = UnityEngine.UI.Image;

namespace script {
    public class MenuUpdate : MonoBehaviour {
        
        private VuMarkBehaviour _vuMark;
        private Vector3 _scale;
        private VuMarkTarget _vuMarkTarget;
        private VuMarkManager _mVuMarkManager;
        public double Countdown = 60;
        
        private VuMarkDataSource _source;
 
        // Use this for initialization
        private void Start(){

            _vuMark = GameObject.Find("VuMark").GetComponent<VuMarkBehaviour>();
            _vuMarkTarget = GetVuMarkTarget();
            _source = new VuMarkDataSource(_vuMarkTarget);
            _scale = transform.localScale;
    
           
            name = _vuMarkTarget.InstanceId.StringValue;
        }

        private VuMarkTarget GetVuMarkTarget(){
            _mVuMarkManager = TrackerManager.Instance.GetStateManager().GetVuMarkManager();
            foreach (var bhvr in _mVuMarkManager.GetActiveBehaviours())
            {
                return bhvr.VuMarkTarget;
            }

            return null;
        }

        private void Update(){
           UpdateUi();
            if (_vuMark == null) return;
            if (VuMarkIsTracked() && _vuMark.VuMarkTarget.InstanceId.StringValue==_vuMarkTarget.InstanceId.StringValue){

                this.transform.localScale = new Vector3(0.01f, 0.01f, 00.01f);
                Countdown = 60;
                PositionUi();
            } else{
                Countdown -= Time.deltaTime;
                if (!(Countdown < 0)) return;
                DeleteFrame();
            }
        }

        private void HideFrame(){
            this.transform.localScale = new Vector3(0,0,0);
        }

        private void ShowFrame(){
            this.transform.localScale = _scale;
        }

        private void DeleteFrame(){
            Destroy (transform.gameObject.GetComponentInParent<Canvas>().gameObject);
            GameObject.Find("EventSystem").GetComponent<MenuManager>().Remove(_vuMarkTarget.InstanceId.StringValue); 
        }

        public void PositionUi() {
            transform.position = new Vector3(_vuMark.transform.position.x, _vuMark.transform.position.y, _vuMark.transform.position.z);;
            var dir = Camera.main.transform.forward;
            dir.y = 0.0f;
            transform.rotation = Quaternion.LookRotation(dir);
        }

        private bool VuMarkIsTracked() {
            switch (_vuMark.CurrentStatus){
                case TrackableBehaviour.Status.NO_POSE:
                    return false;
                case TrackableBehaviour.Status.LIMITED:
                    return false;
                case TrackableBehaviour.Status.DETECTED:
                    return true;
                case TrackableBehaviour.Status.TRACKED:
                    return true;
                case TrackableBehaviour.Status.EXTENDED_TRACKED:
                    return true;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void UpdateUi(){
            GetComponentsInChildren<Image>()[2].GetComponentsInChildren<Text>()[0].text = Countdown.ToString();
        }
        
    }
}

// code for altering canvas relative position
// RectTransform rectt = (RectTransform) transform;
// Vector3 v3 = new Vector3(vuMark.transform.position.x + (transform.localScale.x * rectt.rect.width / 2), vuMark.transform.position.y, vuMark.transform.position.z);