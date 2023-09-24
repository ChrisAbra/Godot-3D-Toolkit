using System.Linq;

namespace Godot3dToolkit;
[Tool]
[GlobalClass]
[Icon("res://src/Damage/assets/HurtBox3D.svg")]
public partial class HurtBox3D : Area3D, IDamageCausing
{
    protected class TrackedHitbox
    {
        public IHitbox hitbox;
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


    private System.Collections.Generic.Dictionary<IHitbox, TrackedHitbox> trackedHitboxes = new();
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
    }

    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
        BodyEntered += OnAreaEntered;
        AreaExited += OnAreaExit;
        BodyExited += OnAreaExit;
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
                trackedHitbox.hitbox.HandleDamage(Damage);
                if (!Repeat)
                {
                    trackedHitboxes.Remove(trackedHitbox.hitbox);
                }
                trackedHitbox.cooldown = HitCooldown;
            }
        }
    }

    public void OnAreaEntered(Node3D enteringArea)
    {
        GD.Print("Area Entered");

        if (enteringArea is IHitbox hitbox)
        {
            GD.Print(enteringArea);
            trackedHitboxes.Add(hitbox, new TrackedHitbox
            {
                hitbox = hitbox,
                cooldown = HitDelay
            });
        }
    }

    public void OnAreaExit(Node3D exitingArea)
    {
        if (exitingArea is IHitbox hitbox)
        {
            trackedHitboxes.Remove(hitbox);
        }
    }
}