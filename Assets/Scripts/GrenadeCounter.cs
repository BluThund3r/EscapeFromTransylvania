using UnityEngine;
using UnityEngine.UI;

public class GrenadeCounter : MonoBehaviour
{
    [SerializeField] private Text grenadeCountText;

    [SerializeField] private Image grenadeIcon;

    public Color UnfocusColor;
    public Color FocusColor;
    public Vector3 FocusScale = new Vector3(1f, 1f, 1f);
    public Vector3 UnfocusScale = new Vector3(0.75f, 0.75f, 0.75f);
    private float focusedX;
    private float unfocusedX;

    private void Start() {
        focusedX = transform.position.x;
        unfocusedX = focusedX - (1f - UnfocusScale.x) * focusedX;
    }

    public void RefreshGrenadeCounter(int grenadesLeft, int maxGrenades) {
        grenadeCountText.text = $"{grenadesLeft}/{maxGrenades}";
    }

    public void SetActive(bool state) {
        gameObject.SetActive(state);
    }

    public void Unfocus() {
        transform.localScale = UnfocusScale;
        transform.position = new Vector3(unfocusedX, transform.position.y, transform.position.z);
        grenadeCountText.color = UnfocusColor;
        grenadeIcon.color = UnfocusColor;
    }

    public void Focus() {
        transform.localScale = FocusScale;
        transform.position = new Vector3(focusedX, transform.position.y, transform.position.z);
        grenadeCountText.color = FocusColor;
        grenadeIcon.color = FocusColor;
    }
}