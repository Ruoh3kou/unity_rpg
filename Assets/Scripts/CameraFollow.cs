using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private BoxCollider2D bg_collider;
    private BoxCollider2D ca_collider;
    // 地图边界
    public float bg_Minx;
    public float bg_Maxx;
    public float bg_Miny;
    public float bg_Maxy;
    // 摄像机边界
    public float ca_Minx;
    public float ca_Maxx;
    public float ca_Miny;
    public float ca_Maxy;

    private float ca_RadiusX;//x轴半径
    private float ca_RadiusY;//y轴半径

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bg_collider = GameObject.FindGameObjectWithTag("bg").GetComponent<BoxCollider2D>();
        ca_collider = this.GetComponent<BoxCollider2D>();

        bg_Minx = bg_collider.bounds.min.x;
        bg_Maxx = bg_collider.bounds.max.x;
        bg_Miny = bg_collider.bounds.min.y;
        bg_Maxy = bg_collider.bounds.max.y;

        ca_Maxx = ca_collider.bounds.max.x;
        ca_Maxy = ca_collider.bounds.max.y;
        ca_Minx = ca_collider.bounds.min.x;
        ca_Miny = ca_collider.bounds.min.y;

        ca_RadiusX = (Mathf.Abs(ca_Maxx) + Mathf.Abs(ca_Minx)) / 2;
        ca_RadiusY = (Mathf.Abs(ca_Maxy) + Mathf.Abs(ca_Miny)) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 player_Pos = player.transform.position;

        // 在边界内移动
        if (((player_Pos.x - ca_RadiusX) > bg_Minx) && ((player_Pos.x - ca_RadiusX) < bg_Maxx) && ((player_Pos.y + ca_RadiusY) > bg_Miny) && (player_Pos.y - ca_RadiusY) < bg_Maxy)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(player_Pos.x,player_Pos.y,-2), 1.0f);
        }


        //在左边移动
        if ((player_Pos.x - ca_RadiusX) <= bg_Minx)
        {
            // 左上角
            if ((player_Pos.y + ca_RadiusY) >= bg_Maxy)
                this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(bg_Minx + ca_RadiusX, bg_Maxy - ca_RadiusY, -2), 1.0f);
            // 左下角
            else if ((player_Pos.y - ca_RadiusY) <= bg_Miny)
                this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(bg_Minx + ca_RadiusX, bg_Miny + ca_RadiusY, -2), 1.0f);
            // 左边
            else this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(bg_Minx + ca_RadiusX, player_Pos.y, -2), 1.0f);
        }

        //在右边移动
        if ((player_Pos.x + ca_RadiusX) >= bg_Maxx)
        {
            // 右上角
            if ((player_Pos.y + ca_RadiusY) >= bg_Maxy)
                this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(bg_Maxx - ca_RadiusX, bg_Maxy - ca_RadiusY, -2), 1.0f);
            // 右下角
            else if ((player_Pos.y - ca_RadiusY) <= bg_Miny)
                this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(bg_Maxx - ca_RadiusX, bg_Miny + ca_RadiusY, -2), 1.0f);
            // 右边
            else
                this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(bg_Maxx - ca_RadiusX, player_Pos.y, -2), 1.0f);
        }

        // 在上边移动
        if ((player_Pos.y + ca_RadiusY) >= bg_Maxy)
        {
            // 右上角
            if ((player_Pos.x + ca_RadiusX) >= bg_Maxx)
                this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(bg_Maxx - ca_RadiusX, bg_Maxy - ca_RadiusY, -2), 1.0f);
            // 左上角
            else if ((player_Pos.x - ca_RadiusX) <= bg_Minx)
                this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(bg_Minx + ca_RadiusX, bg_Maxy - ca_RadiusY, -2), 1.0f);
            // 上边
            else
                this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(player_Pos.x, bg_Maxy - ca_RadiusY, -2), 1.0f);
        }
        // 在下边移动
        if ((player_Pos.y - ca_RadiusY) <= bg_Miny)
        {
            // 右下角
            if ((player_Pos.x + ca_RadiusX) >= bg_Maxx)
                this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(bg_Maxx - ca_RadiusX, bg_Miny + ca_RadiusY, -2), 1.0f);
            // 左下角
            else if ((player_Pos.x - ca_RadiusX) <= bg_Minx)
                this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(bg_Minx + ca_RadiusX, bg_Miny + ca_RadiusY, -2), 1.0f);
            // 下边
            else
                this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(player_Pos.x, bg_Miny + ca_RadiusY, -2), 1.0f);
        }
    }
}
