using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHpBar : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private float timeHiden = 2f;
    [SerializeField] private int style = 0;
    private Color32 high;
    private Color32 low;

    private float count;

    const int style_normal = 0;
    const int style_boss = 1;

    public Slider HpBar { get => hpBar; set => hpBar = value; }

    private void Awake()
    {
        Canvas canvas = GetComponentInChildren<Canvas>();

        if (canvas != null && style == style_normal)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.worldCamera = Camera.main;
        }
        if (HpBar != null && style == style_normal)
        {
            Vector3 offset = new Vector3(0f, 0.7f, 0f);
            HpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
        }

        high = new Color32(104, 219, 142, 255);
        low = new Color32(217, 69, 44, 255);
        count = timeHiden;
    }
    public void configMaxHP(int Hp)
    {
        if (HpBar == null)
        {
            HpBar = GetComponentInChildren<Slider>();

            Vector3 offset = new Vector3(0f, 0.7f, 0f);
            HpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);

        }
        HpBar.maxValue = Hp;
        setHp(Hp);
        if (style == style_normal)
            HpBar.gameObject.SetActive(false);

    }

    public void setHp(int Hp)
    {
        HpBar.value = Hp;
        HpBar.fillRect.GetComponent<Image>().color = Color.Lerp(low, high, HpBar.normalizedValue);
        if (style == style_normal)
            HpBar.gameObject.SetActive(true);
        count = timeHiden;

    }

    private void Update()
    {
        if (style == style_boss) return;
        if (count >= 0 && HpBar != null)
        {
            Vector3 offset = new Vector3(0f, 0.7f, 0f);
            HpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);

        }
        count -= Time.deltaTime;
        if (count < 0)
        {
            HpBar.gameObject.SetActive(false);
        }

    }
}
