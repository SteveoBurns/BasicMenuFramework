using BasicMenuFramework;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BasicMenuFramework.Utilities
{
    /// <summary>
    /// Handles the operations for generating objects, sorting and searching.
    /// </summary>
    public class SphereGenerator : MonoBehaviour
    {
        public List<Sphere> objects = new List<Sphere>();
        public GameObject prefab;

        public float searchMin = 0;
        public float searchMax = 0;


        /// <summary>
        /// Spawns a sphere with a random scale and position and adds it into the list objects.
        /// </summary>
        public void SpawnPrefab()
        {
            float randomFloat = Random.Range(1f, 8f);
            Sphere newObject = Instantiate(prefab).AddComponent<Sphere>();
            newObject.transform.localScale = new Vector3(randomFloat, randomFloat, randomFloat);
            newObject.transform.position = transform.position + new Vector3(Random.Range(0,20), Random.Range(-10,20), Random.Range(-20,20));
            objects.Add(newObject);
        
        }

        /// <summary>
        /// Using the SphereSizeComparer sorts the objects list smallest to largest.
        /// </summary>
        public void GetSmallestSphere()
        {
            objects[0].rend.material.color = Color.blue;
            SphereSizeComparer comparer = new SphereSizeComparer();
            objects.Sort(comparer);
            objects[0].rend.material.color = Color.white;
        }

        /// <summary>
        /// Lines up the objects in objects List in the scene.
        /// </summary>
        public void OrganiseSpheres()
        {
            for(int i = 0; i < objects.Count; i++)
            {
                objects[i].transform.position = new Vector3(objects[i].size * 5, 0, 0);
            }
        }

        /// <summary>
        /// Function for the UI Slider. Sets the searchMin variable at runtime.
        /// </summary>
        /// <param name="_size">Passed in float from the Slider</param>
        public void MinSlider(float _size)
        {
            searchMin = _size;
        }
        /// <summary>
        /// Function for the UI Slider. Sets the searchMax variable at runtime.
        /// </summary>
        /// <param name="_size">Passed in float from the Slider</param>
        public void MaxSlider(float _size)
        {
            searchMax = _size;
        }

    
        /// <summary>
        ///Loops through the objects List and moves the objects up the y axis that fit in between the set values of searchMin and searchMax.
        /// </summary>
        public void Search()
        {
            foreach(Sphere _sphere in objects)
            {
                if(_sphere.size > searchMin & _sphere.size < searchMax)
                {
                    _sphere.transform.position = new Vector3(_sphere.transform.position.x,_sphere.transform.position.y + 10,_sphere.transform.position.z);
                }
            }
        
        }

    
    }
}