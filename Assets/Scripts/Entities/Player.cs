using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace VG
{
    public class Player : Actor
    {
        private static Player instance;

        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<Player>();
                    if (instance == null)
                    {
                        instance = new GameObject().AddComponent<Player>();
                    }
                }
                return instance;
            }
        }

        public UnityEvent onShoot;

        public override void Awake()
        {
            base.Awake();
    
            if (instance != null) Destroy(this);
            DontDestroyOnLoad(this);
        }

    }
}