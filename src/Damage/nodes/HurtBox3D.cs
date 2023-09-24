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


    readonly System.Collections.Generic.Dictionary<IHitbox, TrackedHitbox> trackedHitboxes = new();
    bool isTrackingHitboxes = false;

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

        if (!isTrackingHitboxes) return;

        foreach (var trackedHitbox in trackedHitboxes.Values)
        {
            trackedHitbox.cooldown -= delta;
            if (trackedHitbox.cooldown <= Mathf.Epsilon)
            {
                trackedHitbox.hitbox.HandleDamage(Damage);
                trackedHitbox.cooldown = HitCooldown;
                if (!Repeat)
                {
                    removeHitbox(trackedHitbox.hitbox);

                }
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
            isTrackingHitboxes = true;
        }
    }

    public void OnAreaExit(Node3D exitingArea)
    {
        if (exitingArea is IHitbox hitbox)
        {
            removeHitbox(hitbox);
        }
    }

    private void removeHitbox(IHitbox hitbox)
    {
        trackedHitboxes.Remove(hitbox);
        isTrackingHitboxes = trackedHitboxes.Count() > 0;
    }
}