# Simple Examples

This mod contains multiple small examples of common modding tasks

## What's in here?

### Localization

Contains a simple demonstartion of having an English and Spanish translation

### Choosing from a specific list of options

 * The "Cookbook" Arcana shows a Chooser with a specific list of options
 * `Scripts/UniqueSpawnableChooserData.cs` defines a re-usable DataObject for making unique lists
 * `Data/Mod/UniqueFoodChooserData` is an instance of `UniqueSpawnableChooserData` that specifies a list of cooked food
 * `Data/AbilityData/Arcana/Ability_Arcana_Cookbook`'s Behavior uses that list in a "Choose" action
