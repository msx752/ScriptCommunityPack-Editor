using System;
using System.ComponentModel;

namespace FastColoredTextBoxNS
{
    internal class FCTBDescriptionProvider : TypeDescriptionProvider
    {
        public FCTBDescriptionProvider(Type type) : base(GetDefaultTypeProvider(type))
        {
        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            return new FCTBTypeDescriptor(base.GetTypeDescriptor(objectType, instance), instance);
        }

        private static TypeDescriptionProvider GetDefaultTypeProvider(Type type)
        {
            return TypeDescriptor.GetProvider(type);
        }
    }
}