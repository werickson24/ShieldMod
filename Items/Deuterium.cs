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
            Tooltip.SetDefault("It's Shimmering");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 3));
        }
        public override void SetDefaults()
        {
            item.damage = 20;
            item.magic = true;
            item.width = 8;
            item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 10f;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 8;
            //item.shoot = mod.ProjectileType("GeneratorEX");
            item.ammo = item.type;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BottledWater, 5);
            recipe.AddIngredient(ItemID.Ectoplasm, 1);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void OnCraft(Recipe recipe)
        {
            Player player = Main.LocalPlayer;
            player.QuickSpawnItem(ItemID.Bottle, 5);
        }
    }
}
