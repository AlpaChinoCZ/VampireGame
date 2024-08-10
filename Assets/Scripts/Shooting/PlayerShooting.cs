using System;
using System.Collections;
using UnityEngine;
using UnityEngine.WSA;

namespace VG
{
    public class PlayerFire : BasicFire
    {
        [SerializeField] private float fireRate = 2f;
        [SerializeField] private Transform fireStartPosition;
        
        private Vector3 fireTargetPosition = new Vector3(0f,0f, 0f);
        private WaitForSeconds rapidFireWait;

        public void Awake()
        {
            rapidFireWait = new WaitForSeconds( 1f / fireRate);
        }

        public IEnumerator RapidFireCoroutine()
        {
            while (true)
            {
                Launch(fireStartPosition.position, fireTargetPosition);
                yield return rapidFireWait;
            }
        }

        public void UpdateFireTarget(Vector3 fireTargetPosition)
        {
            this.fireTargetPosition = fireTargetPosition;
        }
    }
}
