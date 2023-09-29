using System;

namespace Godot3dToolkit;

public interface IDamageable : IInteractor<Damage>
{

    public bool TakeDamage(Damage damage, IDamaging source = null){
        return Interact(damage, source);
    }

}
