namespace FastColoredTextBoxNS
{
    using System.ComponentModel;
    using System.Windows.Forms;

    internal class FCTBTypeDescriptor : CustomTypeDescriptor
    {
        private object instance;
        private ICustomTypeDescriptor parent;

        public FCTBTypeDescriptor(ICustomTypeDescriptor parent, object instance) : base(parent)
        {
            this.parent = parent;
            this.instance = instance;
        }

        public override string GetComponentName()
        {
            Control instance = this.instance as Control;
            return ((instance == null) ? null : instance.Name);
        }

        public override EventDescriptorCollection GetEvents()
        {
            EventDescriptorCollection events = base.GetEvents();
            EventDescriptor[] descriptorArray = new EventDescriptor[events.Count];
            for (int i = 0; i < events.Count; i++)
            {
                if (events[i].Name == "TextChanged")
                {
                    descriptorArray[i] = new FooTextChangedDescriptor(events[i]);
                }
                else
                {
                    descriptorArray[i] = events[i];
                }
            }
            return new EventDescriptorCollection(descriptorArray);
        }
    }
}