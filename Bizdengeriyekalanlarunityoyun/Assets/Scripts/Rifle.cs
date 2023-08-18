using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
  [Header("Rifle Things")]
  public Camera cam;
  public float giveDamageOf=10f;
  public float shootingRange=100f;
  public float fireCharge=15f;
  public float nextTimeToShot=0f;
  public PlayerScript player;
  public Transform hand;
  public Animator animator;

[Header ("Riffle Ammunition and Shooting")]
private int maixumumAmmunition=500;
public int mag=10;
private int presentAmmunition;
public float reloadingTime=1;
private bool setReloading=false;

  [Header("Rifle Effects")]
  public ParticleSystem muzzleSpark;
  public GameObject WoodedEffect;
  public GameObject goreEffect;
  private void Awake()
  {
    transform.SetParent(hand);
    presentAmmunition=maixumumAmmunition;
  }
   
private void Update() {
  if(setReloading)
  return;

  if(presentAmmunition<=0)
  {
    StartCoroutine(Reload());
    return;

  }

    if(Input.GetButton("Fire1")&& Time.time>=nextTimeToShot)
    {
        nextTimeToShot = Time.time + 1f / fireCharge;
        Shoot();
        animator.SetBool("Fire", true);
        animator.SetBool("Idle",false);
        nextTimeToShot=Time.time + 1f / fireCharge;
        Shoot();
    }
else if(Input.GetButton("Fire1")&& Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
{
  animator.SetBool("Idle", false);
  animator.SetBool("FireWalk",true);    

    
}
else if(Input.GetButton("Fire2")&& Input.GetButton("Fire1"))
{
  animator.SetBool("Idle", false);
  animator.SetBool("FireWalk",true);
  animator.SetBool("IdleAim",true);
  animator.SetBool("Walk",true);
  animator.SetBool("Reloading",false);


}
else
{
  animator.SetBool("Fire", false);
  animator.SetBool("Idle",true);
  animator.SetBool("FireWalk",false);
  
}
}


private void Shoot()
{
  if(mag==0)
  {
    return;
  }
  presentAmmunition--;
  if(presentAmmunition==0)
  {
    mag--;
  }
  
  muzzleSpark.Play();

  RaycastHit hitInfo;
  if(Physics.Raycast(cam.transform.position,cam.transform.forward,out hitInfo,shootingRange))
  {
    Debug.Log(hitInfo.transform.name);
   ObjectToHit objectohit = hitInfo.transform.GetComponent<ObjectToHit>();
   Zombie1 zombie1=hitInfo.transform.GetComponent<Zombie1>();
 Zombie2 zombie2=hitInfo.transform.GetComponent<Zombie2>();
    if(objectohit != null)
    {

        objectohit.ObjectHitDamage(giveDamageOf);
        GameObject WoodGo=Instantiate(WoodedEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        Destroy(WoodGo, 1f);
    }
    else if(zombie1!=null)
    {
      zombie1.zombieHitDamage(giveDamageOf);
      GameObject goreEffectGo=Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
      Destroy(goreEffectGo,1f);

    }
else if(zombie2!=null)
    {
      zombie2.zombieHitDamage(giveDamageOf);
      GameObject goreEffectGo=Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
      Destroy(goreEffectGo,1f);

    }
  }
}

IEnumerator Reload()
{
  player.playerSpeed=0f;
  player.playerSprint=0f;
  setReloading=true;
  animator.SetBool("Reloading",true);
  Debug.Log("Reloading");
  animator.SetBool("Reloading",false);
  yield return new WaitForSeconds(reloadingTime);
  presentAmmunition=maixumumAmmunition;
  player.playerSpeed=1.9f;
  player.playerSprint=3;
  setReloading=false;
}


}

