using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

[CreateAssetMenu]
public class ShotTypeEnemySO :  ScriptableObject
{
   [SerializeField]
   public GameObject bullet;

   [SerializeField]
   public float bulletSpeed;

   [SerializeField]
   public float lifeTime;

   [SerializeField]
   public float timeBetweenShots;


}
