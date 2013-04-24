using System;

namespace WMS_client.Processes
    {
    /// <summary>����������</summary>
    /// <typeparam name="T">��� �����</typeparam>
    public struct PlaningData<T>
        {
        public T Plan { get; set; }
        public T Fact { get; set; }
        public bool IsEqual { get { return Plan.Equals(Fact); } }

        public PlaningData(T p)
            : this()
            {
            Plan = p;
            Fact = (T)Activator.CreateInstance(typeof(T));
            }

        public PlaningData(T p, T f) : this()
            {
            Plan = p;
            Fact = f;
            }
        }
    }