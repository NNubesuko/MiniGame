using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    [SerializeField] private GameObject[] axe;
    [SerializeField] private GameObject[] hammer;
    [SerializeField] private GameObject[] sword;
    [SerializeField] private GameObject generateWeaponsRangeStartPoint;
    [SerializeField] private GameObject generateWeaponsRangeEndPoint;

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
            new Vector2(generateWeaponsRangeEnd.x, generateWeaponsRangeEnd.z)
        );

        while (true) {
            (Vector3 position, bool exist) = generateWeaponsRange.RandomPosition();
            if (!exist) break;

            Instantiate(
                sword[Random.Range(0, sword.Length)],
                position + Vector3.up,
                Quaternion.Euler(90f, 0f, 0f)
            );
        }
    }

    private void Update() {
    }

}
