using UnityEngine;

public class MyTile : MonoBehaviour {

    [SerializeField]
    protected GameObject cross_view;
    [SerializeField]
    protected GameObject zero_view;
    [SerializeField]
    protected GameObject t_view;
    [SerializeField]
    protected GameObject Line1;
    [SerializeField]
    protected GameObject Line2;
    [SerializeField]
    protected GameObject Line3;
    [SerializeField]
    protected GameObject Line4;


    private Map map;

    private void OnMouseUp() {
       // Debug.Log(transform.position);

        if (CheckTile()) return; 

        if (map.Curr_player % 2 == 0) {
            cross_view.SetActive(true);
        } else {
            zero_view.SetActive(true);
        }

        
        map.ChangePlayer();
    }

    private bool CheckTile() {
        return cross_view.activeSelf || zero_view.activeSelf || t_view.activeSelf;
    }

    public void InitMap(Map map) {
        this.map = map;
    }

    public int GetTile() {
        if (cross_view.activeSelf) return 1;
        if (zero_view.activeSelf) return 2;
        if (t_view.activeSelf) return 3;
        return 0;
    }

    public bool IsEqual(int num) {
        return (GetTile() > 0 && (GetTile() == num || GetTile() == 3 || num == 3));
    }

    public void SetT() {
        t_view.SetActive(true);
    }

    public void SetLine(int sign) {
        switch (sign) {
            case 1: Line1.SetActive(true); break;
            case 2: Line2.SetActive(true); break;
            case 3: Line3.SetActive(true); break;
            case 4: Line4.SetActive(true); break;
        }
    }

    public void ResetTile() {
        cross_view.SetActive(false);
        zero_view.SetActive(false);
        t_view.SetActive(false);
        Line1.SetActive(false);
        Line2.SetActive(false);
        Line3.SetActive(false);
        Line4.SetActive(false);
    }
}
