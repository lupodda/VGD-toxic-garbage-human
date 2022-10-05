using System;
using UnityEngine; // Includere questa libreria, ci serve

[Serializable]
public struct SerializableVector3 // Questa struct rappresenta un Vector3 che però è serializzabile
{
    // Coordinate vettore
    private float x;
    private float y;
    private float z;

    public SerializableVector3(Vector3 vector)
    {
        this.x = vector.x;
        this.y = vector.y;
        this.z = vector.z;
    }

    public Vector3 getVector3()
    {
        return new Vector3(x, y, z);
    }
}


[Serializable] // Per rendere serializzabile una struct/classe, va messo prima di essa
public struct GameData
{
    private string sceneName; // Qui vogliamo salvare il nome della scena corrente

    public GameData(string sceneName)
    {
        this.sceneName = sceneName;
    }

    public string getSceneName()
    {
        return sceneName;
    }


}