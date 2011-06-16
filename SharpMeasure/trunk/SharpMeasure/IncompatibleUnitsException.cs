using System;

namespace SharpMeasure
{
    [Serializable]
    public class IncompatibleUnitsException : Exception
    {
        public IncompatibleUnitsException(Type type1, Type type2)
            : this(MakeMessage(type1, type2)) { }

        public IncompatibleUnitsException() { }
        public IncompatibleUnitsException(string message) : base(message) { }
        public IncompatibleUnitsException(string message, Exception inner) : base(message, inner) { }
        protected IncompatibleUnitsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        private static string MakeMessage(Type type1, Type type2)
        {
            return string.Format("Units {0} and {1} represent different quantities", type1.FullName, type2.FullName);
        }
    }
}
