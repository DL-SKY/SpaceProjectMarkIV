using System;
using UnityEngine;

namespace SpaceProject.Interfaces.Pool
{
    public interface IPoolObject
    {
        Action<GameObject> OnReturnObject { get; set; }

        bool IsUsing { get; }
        GameObject PoolObject{ get; }

        void ShowObject();
        void ReturnObject();
    }
}
