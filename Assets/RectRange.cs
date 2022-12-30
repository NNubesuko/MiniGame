using System.Collections.Generic;
using UnityEngine;

public class RectRange {

    public float startX;
    public float startY;
    public float endX;
    public float endY;
    public Vector2 start => new Vector2(startX, startY);
    public Vector2 end => new Vector2(endX, endY);

    private List<Vector3> list = new List<Vector3>();

    public RectRange(
        float startX,
        float startY,
        float endX,
        float endY
    ) {
        this.startX = startX;
        this.startY = startY;
        this.endX = endX;
        this.endY = endY;

        InitList(startX, startY, endX, endY);
    }

    public RectRange(Vector2 start, Vector2 end) : this(start.x, start.y, end.x, end.y) {
    }

    public override string ToString() {
        return $"({startX}, {startY}) ~ ({endX}, {endY})";
    }

    private void InitList(float startX, float startY, float endX, float endY) {
        for (float x = startX; x <= endX; x+=1) {
            for (float z = startY; z <= endY; z+=1) {
                list.Add(new Vector3(x, 0f, z));
            }
        }
    }

    public (Vector3 position, bool exist) RandomPosition() {
        if (list.Count == 0) return (default, false);

        Vector3 position = list[Random.Range(0, list.Count)];
        list.Remove(position);

        return (position, true);
    }

}
