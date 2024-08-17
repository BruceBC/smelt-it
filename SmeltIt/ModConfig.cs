using SmeltIt.Decorators;

/// <summary>
/// A list of options that can be configured by the mod.
/// </summary>
/// <remarks>Each option corresponds to a MachineOutputRule.</remarks>
public sealed class ModConfig
{
    [ConfigOption(
        fieldId: "Default_Bouqet",
        name: "Instant Bouqet",
        tooltip: "Instantly smelt a bouqet in the furnace."
    )]
    public bool InstantBouquet { get; set; } = true;

    [ConfigOption(
        fieldId: "Default_CopperOre",
        name: "Instant Copper Ore",
        tooltip: "Instantly smelt copper ore in the furnace."
    )]
    public bool InstantCopperOre { get; set; } = true;

    [ConfigOption(
        fieldId: "Default_Quartz",
        name: "Instant Quartz",
        tooltip: "Instantly smelt quartz in the furnace."
    )]
    public bool InstantQuartz { get; set; } = true;

    [ConfigOption(
        fieldId: "Default_FireQuartz",
        name: "Instant Fire Quartz",
        tooltip: "Instantly smelt fire quartz in the furnace."
    )]
    public bool InstantFireQuartz { get; set; } = true;

    [ConfigOption(
        fieldId: "Default_IronOre",
        name: "Instant Iron Ore",
        tooltip: "Instantly smelt iron ore in the furnace."
    )]
    public bool InstantIronOre { get; set; } = true;

    [ConfigOption(
        fieldId: "Default_GoldOre",
        name: "Instant Gold Ore",
        tooltip: "Instantly smelt gold ore in the furnace."
    )]
    public bool InstantGoldOre { get; set; } = true;

    [ConfigOption(
        fieldId: "Default_IridiumOre",
        name: "Instant Iridium Ore",
        tooltip: "Instantly smelt iridium ore in the furnace."
    )]
    public bool InstantIridiumOre { get; set; } = true;

    [ConfigOption(
        fieldId: "Default_RadioactiveOre",
        name: "Instant Radioactive Ore",
        tooltip: "Instantly smelt radioactive ore in the furnace."
    )]
    public bool InstantRadioactiveOre { get; set; } = true;

    [ConfigOption(
        fieldId: "Default_Trash",
        name: "Instant Trash",
        tooltip: "Instantly recycle trash in the recyling machine."
    )]
    public bool InstantTrash { get; set; } = true;

    [ConfigOption(
        fieldId: "Default_Driftwood",
        name: "Instant Driftwood",
        tooltip: "Instantly recycle driftwood in the recyling machine."
    )]
    public bool InstantDriftwood { get; set; } = true;

    [ConfigOption(
        fieldId: "Default_BrokenGlasses",
        name: "Instant Broken Glasses",
        tooltip: "Instantly recycle broken glasses in the recyling machine."
    )]
    public bool InstantBrokenGlasses { get; set; } = true;

    [ConfigOption(
        fieldId: "Default_BrokenCd",
        name: "Instant Broken CD",
        tooltip: "Instantly recycle broken cd in the recyling machine."
    )]
    public bool InstantBrokenCd { get; set; } = true;

    [ConfigOption(
        fieldId: "Default_SoggyNewspaper",
        name: "Instant Soggy Newspaper",
        tooltip: "Instantly recycle soggy newspaper in the recyling machine."
    )]
    public bool InstantSoggyNewspaper { get; set; } = true;

    [ConfigOption(
        fieldId: "Default",
        name: "Instant Coal",
        tooltip: "Instantly turns 10 pieces of wood into one piece of coal."
    )]
    public bool InstantCoal { get; set; } = true;
}
