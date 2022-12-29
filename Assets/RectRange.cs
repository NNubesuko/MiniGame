using UnityEngine;

public class RectRange {

    public float startX;
    public float startY;
    public float endX;
    public float endY;
    public Vector2 start => new Vector2(startX, startY);
    public Vector2 end => new Vector2(endX, endY);

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
    }

    public RectRange(Vector2 start, Vector2 end) {
        this.startX = start.x;
        this.startY = start.y;
        this.endX = end.x;
        this.endY = end.y;
    }

    public override string ToString() {
        return $"({startX}, {startY}) ~ ({endX}, {endY})";
    }

}
