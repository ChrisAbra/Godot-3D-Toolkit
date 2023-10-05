using System;

namespace Godot3dToolkit;

public interface IRayHitable
{
    public struct Hit {
        public Node target;
        public Vector3 position;
        public Vector3 normal;
        public Vector3 source;
    }


    public void TakeHit(Hit hit);
}
