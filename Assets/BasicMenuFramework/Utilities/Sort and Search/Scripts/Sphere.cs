using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasicMenuFramework.Utilities
{
    /// <summary>
    /// Sets values for the object
    /// </summary>
    public class Sphere : MonoBehaviour
    {
        public Color color;
        public MeshRenderer rend;
        public float size;
    
        // Start is called before the first frame update
        void Start()
        {
            rend = GetComponent<MeshRenderer>();
            color = Random.ColorHSV(0f,1f,1,1);
            rend.material.color = color;
            size = transform.localScale.x;
        }

        
    }
}