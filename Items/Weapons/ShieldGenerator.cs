using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShieldMod.Items.Weapons
{
    public class ShieldGenerator : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shield Generator");
            Tooltip.SetDefault("A Deuterium Fueled Fusion Condenser");
        }
        public override void SetDefaults()
        {
            item.damage = 35;
            item.magic = true;
            item.channel = true;
            item.mana = 8;
            item.width = 28;
            item.height = 22;
            item.useTime = 20;
            item.useAnimation = 20;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.useStyle = 5;
            item.knockBack = 15;
            item.rare = 11;
            item.UseSound = SoundID.Item13;
            item.shoot = mod.ProjectileType("Generator");
            item.shootSpeed = 14f;
            //item.useAmmo = mod.ItemType("Deuterium");
            item.value = Item.sellPrice(1, 0, 0, 0);
        }
        public override bool CanUseItem(Player player)
        {
            if (player.HasItem(mod.ItemType("Deuterium")))
            {
                player.ConsumeItem(mod.ItemType("Deuterium"));
                item.shoot = mod.ProjectileType("GeneratorEX");
                item.mana = 6;
            }
            else
            {
                item.shoot = mod.ProjectileType("Generator");
                item.mana = 10;
            }
            return base.CanUseItem(player);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChargedBlasterCannon);
            //recipe.AddIngredient(mod.ItemType("Deuterium"), 10);
            recipe.AddIngredient(ItemID.SoulofMight, 10);
            recipe.AddIngredient(ItemID.SpectreBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
