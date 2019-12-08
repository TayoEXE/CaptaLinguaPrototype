//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EasterEgg : MonoBehaviour
//{
//    public Transform headset;

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
        
//    }
//}

namespace VRTK.Examples
{
    using UnityEngine;

    public class EasterEgg : MonoBehaviour
    {
        public VRTK_InteractableObject linkedObject;

        protected Transform banana;
        public Transform headset;
        public Transform centerEyeAnchor;
        protected bool flying;
        public AudioSource unknown;
        private int timer = 0;

        //private const int TIMER_INTERVALS = 0;
        private const int SLOW_MAGNITUDE = 8;

        protected virtual void OnEnable()
        {
            flying = false;
            linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed += InteractableObjectUsed;
                linkedObject.InteractableObjectUnused += InteractableObjectUnused;
            }
        }

        protected virtual void OnDisable()
        {
            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
                linkedObject.InteractableObjectUnused -= InteractableObjectUnused;
            }
        }

        protected virtual void Update()
        {
            if (flying)
            {
                Debug.Log("Flying!!!");
                //headset.position = new Vector3(this.transform.position.x, this.transform.position.y - 1, this.transform.position.z);

                Vector3 newPos = new Vector3((this.transform.position.x - centerEyeAnchor.position.x) / SLOW_MAGNITUDE, (this.transform.position.y - centerEyeAnchor.position.y) / SLOW_MAGNITUDE, (this.transform.position.z - centerEyeAnchor.position.z) / SLOW_MAGNITUDE);
                headset.transform.position += newPos;
            }

            //++timer;

            //if (timer >= TIMER_INTERVALS)
            //{
            //    timer = 0;
            //}
        }

        protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
        {
            flying = true;
            unknown.Play();
        }

        protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
        {
            flying = false;
            unknown.Pause();
        }
    }
}