namespace SmeltIt.Decorators
{
    /// <summary>
    /// A decorator for Generic Mod Config Menu's UI.
    /// </summary>
    internal sealed class ConfigOption : Attribute
    {
        public string FieldId { get; set; }
        public string Name { get; set; }
        public string Tooltip { get; set; }

        /// <summary>
        /// A ConfigOption constructor.
        /// </summary>
        /// <param name="fieldId">This should be the same as the MachineOutputRule id.</param>
        /// <param name="name">The name of the config option that will appear in the UI.</param>
        /// <param name="tooltip">The tooltip of the config option that will appear in the UI.</param>
        public ConfigOption(string fieldId, string name, string tooltip)
        {
            this.FieldId = fieldId;
            this.Name = name;
            this.Tooltip = tooltip;
        }
    }
}
