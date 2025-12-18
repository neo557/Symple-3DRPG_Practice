using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatus : MonoBehaviour
{
    public int maxHp = 100;
    public int Hp;
    public Slider hpBar;
    public Image hpFillImage;

    void Start()
    {
        Hp = maxHp;
        hpBar.maxValue = maxHp;
        hpBar.value = Hp;
    }
    public void TakeDamage(int damage)
    {
        Hp -= damage;
        Hp = Mathf.Max(Hp, 0);

        Debug.Log("Player Hp:" + Hp);

        hpBar.value = Hp;



        StartCoroutine(hpFlash());
        StartCoroutine(HpShake());

        if (Hp <= 0)
        {
            Debug.Log("Player Dead");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator hpFlash()
    {
        Color original = hpFillImage.color;
        hpFillImage.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        hpFillImage.color = original;
    }

    IEnumerator HpShake()
    {
        RectTransform rt = hpBar.GetComponent<RectTransform>();
        Vector2 originalPos = rt.anchoredPosition;

        float duration = 0.15f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-5f, 5f);
            rt.anchoredPosition = originalPos + new Vector2(x, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rt.anchoredPosition = originalPos;
    }



    // Update is called once per frame
    void Update()
    {

    }
}
