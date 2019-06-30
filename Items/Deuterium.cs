using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShieldMod.Items
{
    public class Deuterium : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/572796: It's Shimmering]");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 3));
        }
        public override void SetDefaults()
        {
            item.damage = 0;
            item.magic = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 1.5f;
            item.value = Item.sellPrice(0, 2, 50, 0);
            item.rare = 6;
            item.ammo = item.type;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater, 15);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void OnCraft(Recipe recipe)
        {
            Player player = Main.LocalPlayer;
            player.QuickSpawnItem(ItemID.Bottle, 15);
        }
    }
}
