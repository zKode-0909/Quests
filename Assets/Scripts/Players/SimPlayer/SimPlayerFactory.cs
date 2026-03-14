using UnityEngine;

public class SimPlayerFactory
{
    SimPlayer simPlayerPrefab;
    PlayerRegistry registry;
    PlayerRegistration registration;


    public SimPlayerFactory(SimPlayer prefab,PlayerRegistration registration,PlayerRegistry registry) { 
        simPlayerPrefab = prefab;
        this.registration = registration;
        this.registry = registry;
    }

    public SimPlayer CreateSimPlayer(Level1CharacterTemplate template) {

        var motor = new SimPlayerMotor();
        var player = Object.Instantiate(simPlayerPrefab);
       // var runTimeWeapon = weaponFactory.CreateWeapon(template.WeaponSettings);
        var animator = player.GetComponent<Animator>();
        var health = new EntityHealth(template.MaxHealth);
        var playerState = new PlayerState(player, animator);
        
        
        player.gameObject.SetActive(false);
        player.Initialize(animator, health, RuntimeIDGenerator.GetNext(),registration,playerState,motor, registry);
        template.VisualVariants.ApplyVariant(player);
        return player;   
    }

    
}
