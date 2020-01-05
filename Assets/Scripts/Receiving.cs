/*
    Created by Carl Emil Carlsen.
    Copyright 2016-2018 Sixth Sensor.
    All rights reserved.
    http://sixthsensor.dk
*/

using UnityEngine;

//namespace OscSimpl.Examples
//{
    public class Receiving : MonoBehaviour
    {
        [SerializeField] OscIn _oscIn;

        const string address = "/boatcontrol";

        [Range(-1,1)]
        public float steering;
        public float rowingRate;
        [Range(0,1)]
        public float disPerRow;
        public bool isBoosting;


        void Start()
        {
            // Ensure that we have a OscIn component and start receiving on port 7000.
            if (!_oscIn) _oscIn = gameObject.AddComponent<OscIn>();
            _oscIn.Open(12000);
        }


        void OnEnable()
        {
            //For messages with multiple arguments, route the message using the Map method.
            _oscIn.Map(address, OnTest);
        }


        void OnDisable()
        {
            // If you want to stop receiving messages you have to "unmap".
            _oscIn.Unmap(OnTest);
        }



        void OnTest(OscMessage message)
        {
            message.TryGet(0, out steering);
            message.TryGet(1, out rowingRate);
            message.TryGet(2, out disPerRow);
            message.TryGet(3, out isBoosting);

            //Debug.Log("steering "+ steering.ToString());
            //Debug.Log("rowingRate " + rowingRate.ToString());
            //Debug.Log("disPerRow " + disPerRow.ToString());
            //Debug.Log("isBoosting " + isBoosting.ToString());

            // Always recycle incoming messages when used.
            OscPool.Recycle(message);
        }
    }
//}