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


    readonly System.Collections.Generic.Dictionary<ulong, TrackedHitbox> trackedHitboxes = new();
    bool isTrackingHitboxes = false;

    IEnumerable<KeyValuePair<ulong, TrackedHitbox>> cachedTrackedHitboxes;

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

        foreach (var trackedHitbox in cachedTrackedHitboxes)
        {
            trackedHitbox.Value.cooldown -= delta;
            if (trackedHitbox.Value.cooldown <= Mathf.Epsilon)
            {
                if (trackedHitbox.Value.hitbox is null)
                {

                }
                trackedHitbox.Value?.hitbox?.HandleDamage(Damage);
                trackedHitbox.Value.cooldown = HitCooldown;
                if (!Repeat)
                {
                    removeHitbox(trackedHitbox.Key);

                }
            }
        }
    }

    public void OnAreaEntered(Node3D enteringArea)
    {
        GD.Print("Area Entered");

        if (enteringArea is IHitbox hitbox)
        {
            addHitbox(enteringArea.GetInstanceId(), hitbox);
        }
    }

    public void OnAreaExit(Node3D exitingArea)
    {
        if (exitingArea is IHitbox)
        {
            removeHitbox(exitingArea.GetInstanceId());
        }
    }

    private void addHitbox(ulong hitboxInstanceId, IHitbox hitbox)
    {
        trackedHitboxes.Add(
            hitboxInstanceId,
            new TrackedHitbox
            {
                hitbox = hitbox,
                cooldown = HitDelay
            });
        isTrackingHitboxes = true;
        cachedTrackedHitboxes = trackedHitboxes.AsEnumerable();

    }

    private void removeHitbox(ulong hitboxInstanceId)
    {
        trackedHitboxes.Remove(hitboxInstanceId);
        cachedTrackedHitboxes = trackedHitboxes.AsEnumerable();
        isTrackingHitboxes = trackedHitboxes.Count() > 0;

    }
}