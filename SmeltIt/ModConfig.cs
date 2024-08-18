using SmeltIt.Decorators;

/// <summary>
/// A list of options that can be configured by the mod.
/// </summary>
/// <remarks>Each option corresponds to a MachineOutputRule.</remarks>
public sealed class ModConfig
{
    /*********
    ** Private Constants
    *********/
    private const string furnaceId = "(BC)13";
    private const string preservesJarId = "(BC)15";
    private const string cheesePressId = "(BC)16";
    private const string oilMakerId = "(BC)19";
    private const string recylingMachineId = "(BC)20";
    private const string mayonnaiseMachineId = "(BC)24";
    private const string seedMakerId = "(BC)25";
    private const string charcoalKilnId = "(BC)114";
    private const string fishSmokerId = "(BC)FishSmoker";

    /*********
    ** Furnace
    *********/

    [ConfigOption(
        fieldId: $"{furnaceId}:Default_Bouqet",
        name: "Instant Bouqet",
        tooltip: "Instantly smelt a bouqet in the furnace."
    )]
    public bool InstantBouquet { get; set; } = true;

    [ConfigOption(
        fieldId: $"{furnaceId}:Default_CopperOre",
        name: "Instant Copper Ore",
        tooltip: "Instantly smelt copper ore in the furnace."
    )]
    public bool InstantCopperOre { get; set; } = true;

    [ConfigOption(
        fieldId: $"{furnaceId}:Default_Quartz",
        name: "Instant Quartz",
        tooltip: "Instantly smelt quartz in the furnace."
    )]
    public bool InstantQuartz { get; set; } = true;

    [ConfigOption(
        fieldId: $"{furnaceId}:Default_FireQuartz",
        name: "Instant Fire Quartz",
        tooltip: "Instantly smelt fire quartz in the furnace."
    )]
    public bool InstantFireQuartz { get; set; } = true;

    [ConfigOption(
        fieldId: $"{furnaceId}:Default_IronOre",
        name: "Instant Iron Ore",
        tooltip: "Instantly smelt iron ore in the furnace."
    )]
    public bool InstantIronOre { get; set; } = true;

    [ConfigOption(
        fieldId: $"{furnaceId}:Default_GoldOre",
        name: "Instant Gold Ore",
        tooltip: "Instantly smelt gold ore in the furnace."
    )]
    public bool InstantGoldOre { get; set; } = true;

    [ConfigOption(
        fieldId: $"{furnaceId}:Default_IridiumOre",
        name: "Instant Iridium Ore",
        tooltip: "Instantly smelt iridium ore in the furnace."
    )]
    public bool InstantIridiumOre { get; set; } = true;

    [ConfigOption(
        fieldId: $"{furnaceId}:Default_RadioactiveOre",
        name: "Instant Radioactive Ore",
        tooltip: "Instantly smelt radioactive ore in the furnace."
    )]
    public bool InstantRadioactiveOre { get; set; } = true;

    /*********
    ** Recyling Machine
    *********/

    [ConfigOption(
        fieldId: $"{recylingMachineId}:Default_Trash",
        name: "Instant Trash",
        tooltip: "Instantly recycle trash in the recyling machine."
    )]
    public bool InstantTrash { get; set; } = true;

    [ConfigOption(
        fieldId: $"{recylingMachineId}:Default_Driftwood",
        name: "Instant Driftwood",
        tooltip: "Instantly recycle driftwood in the recyling machine."
    )]
    public bool InstantDriftwood { get; set; } = true;

    [ConfigOption(
        fieldId: $"{recylingMachineId}:Default_BrokenGlasses",
        name: "Instant Broken Glasses",
        tooltip: "Instantly recycle broken glasses in the recyling machine."
    )]
    public bool InstantBrokenGlasses { get; set; } = true;

    [ConfigOption(
        fieldId: $"{recylingMachineId}:Default_BrokenCd",
        name: "Instant Broken CD",
        tooltip: "Instantly recycle broken cd in the recyling machine."
    )]
    public bool InstantBrokenCd { get; set; } = true;

    [ConfigOption(
        fieldId: $"{recylingMachineId}:Default_SoggyNewspaper",
        name: "Instant Soggy Newspaper",
        tooltip: "Instantly recycle soggy newspaper in the recyling machine."
    )]
    public bool InstantSoggyNewspaper { get; set; } = true;

    /*********
    ** Charcoal Kiln
    *********/

    [ConfigOption(
        fieldId: $"{charcoalKilnId}:Default",
        name: "Instant Coal",
        tooltip: "Instantly turns 10 pieces of wood into one piece of coal."
    )]
    public bool InstantCoal { get; set; } = true;

    /*********
    ** Preservers Jar
    *********/

    [ConfigOption(
        fieldId: $"{preservesJarId}:Default_SturgeonRoe",
        name: "Instant Sturgeon Roe",
        tooltip: "Instantly preserves sturgeon roe."
    )]
    public bool InstantSturgeonRoe { get; set; } = true;

    [ConfigOption(
        fieldId: $"{preservesJarId}:Default_Roe",
        name: "Instant Roe",
        tooltip: "Instantly preserves any type of roe except sturgeon."
    )]
    public bool InstantRoe { get; set; } = true;

    [ConfigOption(
        fieldId: $"{preservesJarId}:Default_Pickled",
        name: "Instant Pickled Vegetables",
        tooltip: "Instantly pickles vegetables."
    )]
    public bool InstantPickledVegetables { get; set; } = true;

    [ConfigOption(
        fieldId: $"{preservesJarId}:Default_Jelly",
        name: "Instant Jelly",
        tooltip: "Instantly turns fruit into jelly."
    )]
    public bool InstantJelly { get; set; } = true;

    /*********
    ** Cheese Press
    *********/

    [ConfigOption(
        fieldId: $"{cheesePressId}:Default_GoatMilk",
        name: "Instant Goat Milk",
        tooltip: "Instantly turns goat milk into cheese."
    )]
    public bool InstantGoatMilk { get; set; } = true;

    [ConfigOption(
        fieldId: $"{cheesePressId}:Default_LargeGoatMilk",
        name: "Instant Large Goat Milk",
        tooltip: "Instantly turns large goat milk into cheese."
    )]
    public bool InstantLargeGoatMilk { get; set; } = true;

    [ConfigOption(
        fieldId: $"{cheesePressId}:Default_Milk",
        name: "Instant Milk",
        tooltip: "Instantly turns cow milk into cheese."
    )]
    public bool InstantMilk { get; set; } = true;

    [ConfigOption(
        fieldId: $"{cheesePressId}:Default_LargeMilk",
        name: "Instant Large Milk",
        tooltip: "Instantly turns large cow milk into cheese."
    )]
    public bool InstantLargeMilk { get; set; } = true;

    /*********
    ** Oil Maker
    *********/

    [ConfigOption(
        fieldId: $"{oilMakerId}:Default_Corn",
        name: "Instant Corn Oil",
        tooltip: "Instantly turns corn into all purpose cooking oil."
    )]
    public bool InstantCornOil { get; set; } = true;

    [ConfigOption(
        fieldId: $"{oilMakerId}:Default_Sunflower",
        name: "Instant Sunflower Oil",
        tooltip: "Instantly turns sunflowers into all purpose cooking oil."
    )]
    public bool InstantSunflowerOil { get; set; } = true;

    [ConfigOption(
        fieldId: $"{oilMakerId}:Default_SunflowerSeeds",
        name: "Instant Sunflower Seed Oil",
        tooltip: "Instantly turns sunflower seed into all purpose cooking oil."
    )]
    public bool InstantSunflowerSeeds { get; set; } = true;

    [ConfigOption(
        fieldId: $"{oilMakerId}:Default_Truffle",
        name: "Instant Truffle Oil",
        tooltip: "Instantly turns truffles into truffle oil."
    )]
    public bool InstantTruffle { get; set; } = true;

    /*********
    ** Mayonnaise Machine
    *********/

    [ConfigOption(
        fieldId: $"{mayonnaiseMachineId}:Default_OstrichEgg",
        name: "Instant Ostrich Egg Mayo",
        tooltip: "Instantly turns an ostrich egg into mayo."
    )]
    public bool InstantOstrichEggMayo { get; set; } = true;

    [ConfigOption(
        fieldId: $"{mayonnaiseMachineId}:Default_DuckEgg",
        name: "Instant Duck Egg Mayo",
        tooltip: "Instantly turns a duck egg into mayo."
    )]
    public bool InstantDuckEggMayo { get; set; } = true;

    [ConfigOption(
        fieldId: $"{mayonnaiseMachineId}:Default_VoidEgg",
        name: "Instant Void Egg Mayo",
        tooltip: "Instantly turns a void egg into mayo."
    )]
    public bool InstantVoidEggMayo { get; set; } = true;

    [ConfigOption(
        fieldId: $"{mayonnaiseMachineId}:Default_DinosaurEgg",
        name: "Instant Dinosaur Egg Mayo",
        tooltip: "Instantly turns a dinosaur egg into mayo."
    )]
    public bool InstantDinosaurEggMayo { get; set; } = true;

    [ConfigOption(
        fieldId: $"{mayonnaiseMachineId}:Default_GoldenEgg",
        name: "Instant Golden Egg Mayo",
        tooltip: "Instantly turns a golden egg into mayo."
    )]
    public bool InstantGoldenEggMayo { get; set; } = true;

    [ConfigOption(
        fieldId: $"{mayonnaiseMachineId}:Default_LargeEgg",
        name: "Instant Large Egg Mayo",
        tooltip: "Instantly turns a large egg into mayo."
    )]
    public bool InstantLargeEggMayo { get; set; } = true;

    [ConfigOption(
        fieldId: $"{mayonnaiseMachineId}:Default_Egg",
        name: "Instant Egg Mayo",
        tooltip: "Instantly turns a small egg into mayo."
    )]
    public bool InstantEggMayo { get; set; } = true;

    /*********
    ** Seed Maker
    *********/

    [ConfigOption(
        fieldId: $"{seedMakerId}:Default",
        name: "Instant Seed Maker",
        tooltip: "Instantly turns crops into seeds."
    )]
    public bool InstantSeedMaker { get; set; } = true;

    /*********
    ** Fish Smoker
    *********/

    [ConfigOption(
        fieldId: $"{fishSmokerId}:SmokedFish",
        name: "Instant Smoked Fish",
        tooltip: "Instantly smokes fish."
    )]
    public bool InstantSmokedFish { get; set; } = true;
}
