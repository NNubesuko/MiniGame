using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain : EnemyImpl {

    [SerializeField] private GameAdminMain gameAdmin;

    [Header("ステータス")]
    [SerializeField] private int hp;
    [SerializeField] private float moveSpeed;

    [Header("クールタイム")]
    [SerializeField] private float longDistanceAttackCoolTime;

    private Transform playerTransform;
    private WeaponsManager weaponsManager;

    private void Start() {
        Init(
            hp,
            moveSpeed,
            longDistanceAttackCoolTime
        );

        playerTransform = gameAdmin.PlayerObject.transform;

        weaponsManager = gameAdmin.WeaponsManager;
        AxeList = weaponsManager.AxeList;
        HammerList = weaponsManager.HammerList;
        SwordList = weaponsManager.SwordList;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            int weaponType = Random.Range(0, weaponsManager.WeaponTypeNumber);
            Debug.Log(weaponType);

            switch (weaponType) {
                case (int)WeaponType.Axe:
                    Debug.Log("Axe");
                    AxeAttack();
                    break;
                case (int)WeaponType.Hammer:
                    Debug.Log("Hammer");
                    // HammerAttack();
                    break;
                case (int)WeaponType.Sword:
                    Debug.Log("Sword");
                    // SwordAttack();
                    break;
            }
        }
    }

    private void AxeAttack() {
        if (AxeList.Count == 0) return;

        GameObject[] weaponObjects = new GameObject[2];
        ActivateWeaponScript(AxeList, weaponObjects);
        RemoveWeaponFromList(AxeList, weaponObjects);
    }

    private void HammerAttack() {
        if (HammerList.Count == 0) return;

        GameObject[] weaponObjects = new GameObject[1];
        ActivateWeaponScript(HammerList, weaponObjects);
        RemoveWeaponFromList(HammerList, weaponObjects);
    }

    private void SwordAttack() {
        if (SwordList.Count == 0) return;

        GameObject[] weaponObjects = new GameObject[2];
        ActivateWeaponScript(SwordList, weaponObjects);
        RemoveWeaponFromList(SwordList, weaponObjects);
    }

    private void ActivateWeaponScript(List<GameObject> list, GameObject[] arr) {
        for (int i = 0; i < arr.Length; i++) {
            arr[i] = list[i];
            arr[i].GetComponent<WeaponMain>().enabled = true;
        }
    }

    private List<GameObject> RemoveWeaponFromList(List<GameObject> list, GameObject[] arr) {
        for (int i = 0; i < arr.Length; i++) {
            list.Remove(arr[i]);
        }

        return list;
    }

}
