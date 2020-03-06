using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingArea : MonoBehaviour
{
    public  CookingPositions cookingPositions;


    [System.Serializable]
    public struct CookingPositions
    {
        [SerializeField]
        private Transform ovenPos;
        [SerializeField]
        private Transform stovePos;
        [SerializeField]
        private Transform cuttingPos;
        [SerializeField]
        private Transform prepPos;

        public CookingPositions (Transform ovenPos = null,Transform stovePos = null,Transform cuttingPos = null,Transform prepPos = null) {
            this.ovenPos = ovenPos;
            this.stovePos = stovePos;
            this.cuttingPos = cuttingPos;
            this.prepPos = prepPos;
        }

        public Vector3 OvenPos { get => ovenPos.position; }
        public Vector3 StovePos { get => stovePos.position; }
        public Vector3 CuttingPos { get => cuttingPos.position; }
        public Vector3 PrepPos { get => prepPos.position; }
    }
}
