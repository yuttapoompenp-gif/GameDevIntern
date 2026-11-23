using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus instance { get; private set; }

    [SerializeField] private Slider playerHealthBar;
    [SerializeField] public Slider protectBar;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        protectBar.value = protectBar.maxValue;
        playerHealthBar.value = playerHealthBar.maxValue;
    }

    public void dealDamageToPlayer(float damage_value)
    {
        playerHealthBar.value -= damage_value;
    }
    public void dealDamageToPlayerProtect(float damage_value)
    {
        protectBar.value -= damage_value;
    }

    public void healthPlayer(float health_value)
    {
        playerHealthBar.value += health_value;
    }
    public void healthPlayerProtect(float health_value)
    {
        protectBar.value += health_value;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            dealDamageToPlayerProtect(10);
        }
    }
}
