using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;


public struct PlayerDataInGame : INetworkSerializable, System.IEquatable<PlayerDataInGame>
{
    public ulong _id;
    public Vector3 _velocity;
    //This string will have all the forces to be added in that frame separated by #
    public FixedString512Bytes _forceToBeAdded;

    public PlayerDataInGame(ulong id )
    {
        this._id = id;
        _velocity = Vector3.zero;
        _forceToBeAdded = "";
    }

    public void NetworkSerialize<T>( BufferSerializer<T> serializer ) where T : IReaderWriter
    {
        
        if(serializer.IsReader)
        {
            var reader = serializer.GetFastBufferReader();
            reader.ReadValueSafe( out _id );
            reader.ReadValueSafe( out _velocity );
            reader.ReadValueSafe( out _forceToBeAdded );
        }
        else
        {
            var writer = serializer.GetFastBufferWriter();
            writer.WriteValueSafe( _id );
            writer.WriteValueSafe( _velocity );
            writer.WriteValueSafe( _forceToBeAdded );
        }

    }

    public bool Equals( PlayerDataInGame other )
    {
        return _id == other._id
            && _velocity == other._velocity
            ;
    }

    public override string ToString()
    {
        return "ID: " + _id + ". Velocity: " + _velocity.ToString() + ". Force To Be Added: " + _forceToBeAdded ;
    }

    public List<Vector3> GetForceToBeAdded() {
        List<Vector3> result = new List<Vector3>();
        if(_forceToBeAdded != "")
        {
            string[] forces = this._forceToBeAdded.ToString().Split( '#' );
            //The split adds one empty line in the end
            for(int i = 0; i < forces.Length - 1; i++)
            {

                result.Add( GeneralFunctions.StringToVector3( forces[i] ) );
            }
        }
        return result;
    }
}
