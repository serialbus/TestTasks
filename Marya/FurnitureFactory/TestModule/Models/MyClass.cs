using System;

namespace TestModule.Models
{
    public class MyClass
    {
        public MyClass()
        {
            Uid = Guid.NewGuid();
        }

        public Guid Uid { get; private set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            //return base.Equals(obj);

            if (obj == null)
                return false;

            if (!(obj is MyClass))
                return base.Equals(obj);

            return ((MyClass)obj).Uid == this.Uid;
        }
    }
}
