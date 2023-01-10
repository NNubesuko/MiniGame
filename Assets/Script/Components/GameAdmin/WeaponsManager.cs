using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {
    Axe = 0,
    Hammer = 1,
    Sword = 2
}

public class WeaponsManager : MonoBehaviour {

    [Header("各武器のゲームオブジェクト")]
    [SerializeField] private GameObject[] axe;
    [SerializeField] private GameObject[] hammer;
    [SerializeField] private GameObject[] sword;

    [Header("各武器の生成数")]
    [SerializeField] private float numberAxe;
    [SerializeField] private float numberHammer;
    [SerializeField] private float numberSword;

    [Header("生成する位置")]
    [SerializeField] private GameObject generateWeaponsRangeStartPoint;
    [SerializeField] private GameObject generateWeaponsRangeEndPoint;
    [SerializeField] private float height;

    public int WeaponTypeNumber => System.Enum.GetValues(typeof(WeaponType)).Length;

    public List<WeaponMain> AxeList { get; private set; } = new List<WeaponMain>();
    public List<WeaponMain> HammerList { get; private set; } = new List<WeaponMain>();
    public List<WeaponMain> SwordList { get; private set; } = new List<WeaponMain>();

    private RectRange generateWeaponsRange;
    private Vector3[] weaponsPositionArray;

    private List<Vector3> weaponsPositionList = new List<Vector3>();

    private void Awake() {
        // 範囲の開始地点と終了地点から座標を取得
        Vector3 generateWeaponsRangeStart = generateWeaponsRangeStartPoint.transform.position;
        Vector3 generateWeaponsRangeEnd = generateWeaponsRangeEndPoint.transform.position;

        // 四角形の範囲クラスを初期化
        generateWeaponsRange = new RectRange(
            new Vector2(generateWeaponsRangeStart.x, generateWeaponsRangeStart.z),
            new Vector2(generateWeaponsRangeEnd.x, generateWeaponsRangeEnd.z),
            height
        );

        AxeList = GenerateWeapons(numberAxe, axe);
        HammerList = GenerateWeapons(numberHammer, hammer);
        SwordList = GenerateWeapons(numberSword, sword);
    }

    private List<WeaponMain> GenerateWeapons(float number, GameObject[] gameObjects) {
        List<WeaponMain> list = new List<WeaponMain>();

        for (int i = 0; i < number; i++) {
            // 返せる位置がある場合は、位置とtrueの2つの値が返却される
            // 返せる位置が無い場合は、デフォルト値とfalseの2つの値が返却される
            (Vector3 position, bool exist) = generateWeaponsRange.RandomPosition();
            if (!exist) break;

            // 武器のオブジェクトが格納されている配列から、ランダムに武器を選択する
            GameObject gameObject = gameObjects[Random.Range(0, gameObjects.Length)];
            GameObject generatedWeaponObject =
                Instantiate(gameObject, position + Vector3.up, Quaternion.Euler(90f, 0f, 0f));

            list.Add(generatedWeaponObject.GetComponent<WeaponMain>());
        }

        return list;
    }

    public void ActivateWeapon(WeaponType weaponType, int weaponNumber) {
        switch (weaponType) {
            case WeaponType.Axe:
                ActivateAxe(weaponNumber);
                break;
            case WeaponType.Hammer:
                ActivateHammer(weaponNumber);
                break;
            case WeaponType.Sword:
                ActivateSword(weaponNumber);
                break;
        }
    }

    private void ActivateAxe(int weaponNumber) {
        if (AxeList.Count == 0) return;

        ActivateWeapon(AxeList, weaponNumber);
    }

    private void ActivateHammer(int weaponNumber) {
        if (HammerList.Count == 0) return;

        ActivateWeapon(HammerList, weaponNumber);
    }

    private void ActivateSword(int weaponNumber) {
        if (SwordList.Count == 0) return;

        ActivateWeapon(SwordList, weaponNumber);
    }

    private void ActivateWeapon(List<WeaponMain> list, int weaponNumber) {
        WeaponMain[] weapons = new WeaponMain[weaponNumber];

        for (int i = 0; i < weapons.Length; i++) {
            weapons[i] = list[i];
            weapons[i].enabled = true;
        }

        RemoveActiveWeaponFromList(list, weapons);
    }

    private void RemoveActiveWeaponFromList(List<WeaponMain> list, WeaponMain[] arr) {
        for (int i = 0; i < arr.Length; i++) {
            list.Remove(arr[i]);
        }
    }

}
