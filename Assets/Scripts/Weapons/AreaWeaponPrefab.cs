using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaWeaponPrefab : MonoBehaviour
{

    public AreaWeapon weapon;
    private Vector3 targetSize;
    private float timer;
    private float damage;
    void Start()
    {
        weapon = GameObject.Find("AreaWeapon").GetComponent<AreaWeapon>();
        //Destroy(gameObject, weapon.duration);
        targetSize = Vector3.one * weapon.stats[weapon.weaponLevel].range;
        transform.localScale = Vector3.zero;
        timer = weapon.stats[weapon.weaponLevel].duration;
        damage = weapon.stats[weapon.weaponLevel].damage;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, Time.deltaTime * 5);

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            targetSize = Vector3.zero;
            if(transform.localScale.x == 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            enemy.TakeDamage(weapon.stats[weapon.weaponLevel].damage);
        }
    }
}
