using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Duel : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject fireBulletPrefab;
    public GameObject IceBulletPrefab;
    private float throwSpeed = 30f;
    public GameObject Camera;
    public int AttackMode = 0;//0: 일반 공격, 1: 불공격, 2: 얼음공격
    private GameObject newBullet = null;
    private GameObject newBullet2 = null;
    private GameObject newBullet3 = null;
    private GameObject newBullet4 = null;

    public AudioSource TempAudio;
    public AudioClip[] clips;
    public GameObject FadeOut;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //GetComponent<AudioSource>().PlayOneShot();

            if (AttackMode == 1)
            {
                TempAudio.volume = SoundState.musicVolume;
                TempAudio.PlayOneShot(clips[0]);

                newBullet = Instantiate(fireBulletPrefab, transform.position, transform.rotation) as GameObject;
                newBullet2 = Instantiate(fireBulletPrefab, transform.position, transform.rotation) as GameObject;
                newBullet2.name = "Bullet";
                newBullet2.GetComponent<Rigidbody>().velocity = -Camera.transform.forward * throwSpeed;
                Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), newBullet2.GetComponent<Collider>(), true);

                newBullet3 = Instantiate(fireBulletPrefab, transform.position, transform.rotation) as GameObject;
                newBullet3.name = "Bullet";
                newBullet3.GetComponent<Rigidbody>().velocity = Camera.transform.right * throwSpeed;
                Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), newBullet3.GetComponent<Collider>(), true);

                newBullet4 = Instantiate(fireBulletPrefab, transform.position, transform.rotation) as GameObject;
                newBullet4.name = "Bullet";
                newBullet4.GetComponent<Rigidbody>().velocity = -Camera.transform.right * throwSpeed;
                Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), newBullet4.GetComponent<Collider>(), true);
            }
            else if (AttackMode == 2)
            {
                TempAudio.volume = SoundState.musicVolume;
                TempAudio.PlayOneShot(clips[1]);

                newBullet = Instantiate(IceBulletPrefab, transform.position, transform.rotation) as GameObject;
                newBullet2 = Instantiate(IceBulletPrefab, transform.position, transform.rotation) as GameObject;
                newBullet2.name = "Bullet";
                newBullet2.GetComponent<Rigidbody>().velocity = -Camera.transform.forward * throwSpeed;
                Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), newBullet2.GetComponent<Collider>(), true);

                newBullet3 = Instantiate(IceBulletPrefab, transform.position, transform.rotation) as GameObject;
                newBullet3.name = "Bullet";
                newBullet3.GetComponent<Rigidbody>().velocity = Camera.transform.right * throwSpeed;
                Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), newBullet3.GetComponent<Collider>(), true);

                newBullet4 = Instantiate(IceBulletPrefab, transform.position, transform.rotation) as GameObject;
                newBullet4.name = "Bullet";
                newBullet4.GetComponent<Rigidbody>().velocity = -Camera.transform.right * throwSpeed;
                Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), newBullet4.GetComponent<Collider>(), true);

            }
            else
            {
                TempAudio.volume = SoundState.musicVolume;
                TempAudio.PlayOneShot(clips[2]);

                newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            }
            newBullet.name = "Bullet";

            GameObject.Find("MainPlayer").GetComponent<Animator>().SetTrigger("Attack");

            newBullet.GetComponent<Rigidbody>().velocity = Camera.transform.forward * throwSpeed;
            Physics.IgnoreCollision(transform.root.GetComponent<Collider>(), newBullet.GetComponent<Collider>(), true);
        }
    }

    IEnumerator PlayerDie()
    {
        GameObject.Find("SoundState").GetComponent<AudioSource>().Stop();
        TempAudio.volume = SoundState.musicVolume;
        TempAudio.PlayOneShot(clips[3]);
        yield return new WaitForSeconds(1f);
        FadeOut.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("FailEnding");
    }
}
