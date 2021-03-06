﻿using UnityEngine;
using System.Collections;

public class Skill_Fireball : MonoBehaviour {
    ParticleSystem particles;
    public float speed;
    public Vector3 target;
    public float skillDamage;
	// Use this for initialization
	void Start () {
        particles = this.GetComponent<ParticleSystem>();
        speed += Random.Range(-50, 50) / 25f;
        if(skillDamage==0)target += new Vector3(Random.Range(-50, 50) / 20f, 0, Random.Range(-50, 50) / 20f);
        Vector3 pos = target;
        pos.y += 25;
        pos.x += 10;
        pos.z += 10;
        this.transform.position = pos;
        particles.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (target.magnitude < 0.0001f) return;
        Vector3 dir = target - transform.position;

        if (dir.magnitude < 1)
        {
            particles.Stop();
            foreach (GameObject character in GameManager.characters[GameManager.other])
            {
                if ((character.transform.position - target).magnitude < 10)
                {
                    //initiate some kind of damage on the character
                    character.GetComponent<CombatController>().TakeDamage(new Damage(0, skillDamage));

                }
            }
            Destroy(this.gameObject) ;
        }
        Vector3 result = transform.position+dir.normalized*speed*Time.deltaTime;
        this.transform.position = result;
        this.transform.forward = dir.normalized;
        if (dir.magnitude < 1) particles.Stop();
	}
}
