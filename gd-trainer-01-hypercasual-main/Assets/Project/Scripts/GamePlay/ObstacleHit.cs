using UnityEngine;

public class ObstacleHit : MonoBehaviour
{
    [Tooltip("Ссылка на GameManager")]
    public GameManager gameManager = null;
    [Tooltip("Базовый наносимый урон")]
    public int damage = 1; // ← ЗАДАНИЕ

    private void Update()
    {
        //Двигаем объект в лево с базовой скоростью
        transform.position += (new Vector3(-1, 0, 0) * gameManager.GetSpeed()) * Time.deltaTime;
    }

    //Наносим урон игроку и уничтожаем преграду
    public void MakeHit()
    {
        //Наносим урон игроку в размере базового урона преграды
        gameManager.DealDamage(damage);
        Destroy(gameObject);
    }
}
