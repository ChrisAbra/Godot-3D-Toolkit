using System.Linq;

namespace Godot3dToolkit;
[Tool]
[GlobalClass]
[Icon("res://src/Damage/assets/HurtBox3D.svg")]
public partial class HurtBox3D : Area3D, IDamageCausing
{
    protected class TrackedHitbox
    {
        public HitArea3D hitbox;
        public double cooldown;
    }

    [Export]
    public DamageSet Damage { get; set; }

    [Export]
    public bool Repeat = true;

    [Export]
    public double HitCooldown = 1;

    [Export]
    public double HitDelay = 0f;


    private System.Collections.Generic.Dictionary<HitArea3D, TrackedHitbox> trackedHitboxes = new();
    private bool hasHitboxes
    {
        get
        {
            return trackedHitboxes.Count > 0;
        }
    }

    public HurtBox3D()
    {
        Monitorable = false;
        AreaEntered += OnAreaEntered;
    }

    public override void _Process(double delta)
    {
        if (Engine.IsEditorHint()) return;

        if (!hasHitboxes) return;

        foreach (var trackedHitbox in trackedHitboxes.Values)
        {
            trackedHitbox.cooldown -= delta;
            if (trackedHitbox.cooldown <= Mathf.Epsilon)
            {
                trackedHitbox.hitbox.TakeDamage(Damage);
                if (!Repeat)
                {
                    trackedHitboxes.Remove(trackedHitbox.hitbox);
                }
                trackedHitbox.cooldown = HitCooldown;
            }
        }
    }

    public void OnAreaEntered(Area3D enteringArea)
    {
        GD.Print("Area Entered");
        if (enteringArea is HitArea3D hitbox)
        {
            trackedHitboxes.Add(hitbox, new TrackedHitbox
            {
                hitbox = hitbox,
                cooldown = HitDelay
            });
        }
    }

    public void OnAreaExit(Area3D exitingArea)
    {
        if (exitingArea is HitArea3D hitbox)
        {
            trackedHitboxes.Remove(hitbox);
        }
    }
}