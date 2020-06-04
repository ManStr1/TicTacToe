using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour {
    
    [SerializeField]
    protected MyTile tile_prefab;
    [SerializeField]
    protected GameObject HiddenSquare;
    [SerializeField]
    protected Canvas canvas;
    [SerializeField]
    protected Text text;

    private MyTile[,] tiles;
    private Vector2[] road;
    private int curr_player = 0;
    private int size = 4;

    public int Curr_player { get => curr_player; }

    private void Start() {
        tiles = new MyTile[size, size];

        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                MyTile tile = Instantiate<MyTile>(tile_prefab, new Vector3(i, j, 0), Quaternion.identity, transform);
                tiles[i, j] = tile;
                tile.InitMap(this);
            }
        }

        System.Random rnd = new System.Random();
        tiles[rnd.Next(0, 3), rnd.Next(0,3)].SetT();

    }

    public void ChangePlayer() {
        if (End()) {
            if (curr_player % 2 == 0) text.text = "1-st player wins!"; else text.text = "2-nd player wins!";
            GenerateWinRoad();
            HiddenSquare.transform.position = new Vector3(0, 0, -1f);
            canvas.planeDistance = 1;
        } else if (curr_player == size * size - 2) Debug.Log("NOBODY WIN!");

        curr_player++;
    }


    private bool End() {
        for (var x = 0; x < size; x++) {
            if (AllFieldsTheSame(x, 0, 0, 1))
                return true;
        }

        // Check rows
        for (var y = 0; y < size; y++)
            if (AllFieldsTheSame(0, y,  1, 0))
                return true;

        // Check diagonals
        if (AllFieldsTheSame(0, 0, 1, 1))
            return true;

        if (AllFieldsTheSame(size - 1, 0, -1, 1))
            return true;
        return false;
    }

    public bool AllFieldsTheSame(int startX, int startY, int dx, int dy) {
        int firstField = -1;
        int count = 0;
        road = new Vector2[4];
        road[0] = new Vector2(startY, startX);

        for (var i = 0; i < size; i++) {
            int y = startY + dy * i;
            int x = startX + dx * i;

            if (tiles[y, x].GetTile() != 0 && firstField < 0) {
                firstField = tiles[y, x].GetTile();
            } else if (firstField > 0 && !tiles[y, x].IsEqual(firstField)) {
                return false;
            } else if (firstField > 0) {
                if (firstField == 3) {
                    firstField = tiles[y, x].GetTile();
                }
                count++;
                road[count] = new Vector2(y, x);
            }
        }
        
        return (count == 3);
    }

    private void GenerateWinRoad() {
        int sign = 0;

        if (road[0].x == road[0].y && road[1].x == road[1].y) {
            sign = 2; // /
        } else if (road[0].x == road[size - 1].y && road[0].y == road[size - 1].x) {
            sign = 1; // \
        } else if (road[0].x > road[1].x || road[0].x < road[1].x) {
            sign = 3; // -
        } else if (road[0].y > road[1].y || road[0].y < road[1].y) {
            sign = 4; // |
        }

        for (int i = 0; i < size; i ++) {
            tiles[(int) road[i].x, (int) road[i].y].SetLine(sign);
        }
    }

    public void RestartGame() {
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                tiles[i, j].ResetTile();
            }
        }

        System.Random rnd = new System.Random();
        tiles[rnd.Next(0, 3), rnd.Next(0, 3)].SetT();
        HiddenSquare.transform.position = new Vector3(0, 0, 0.5f);
        canvas.planeDistance = 0;
        curr_player = 0;
    }
}

