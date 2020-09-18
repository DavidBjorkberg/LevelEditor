using UnityEngine;

public class RobotDude : Character
{
    public int AbilityCharges;
    public int OnKillRecharge;
    [Header("Carstats")]
    public Racecar Racecar;
    public float CarSpeed;
    public int MovementSpeedGrowth;
    public int ExplodeTimer;
    public float ExplosionRadius;

    protected override void Ability()
    {
        if (Input.GetButtonDown(allowedinput + "Ability") && AbilityCharges > 0)
        {
            hasControl = false;
Vector3 Bottom = transform.Find("Bottom").position + GetComponent<Character>().direction;
            Racecar racecar = Instantiate(Racecar, Bottom, Quaternion.identity);
            racecar.allowedinput = allowedinput;
            racecar.player = this;
            AbilityCharges--;
        }
    }
    public void AbilityRecharge()
    {
        AbilityCharges += OnKillRecharge;
    }
}
