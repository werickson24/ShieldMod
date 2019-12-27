using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.UI.Chat;
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
            //item.useAmmo = mod.ItemType("Deuterium"); //I don't use this to allow a different action on no ammo
            item.value = Item.sellPrice(1, 0, 0, 0);
        }
        public override bool CanUseItem(Player player)
        {
            if (player.HasItem(mod.ItemType("Deuterium")))
            {
                player.ConsumeItem(mod.ItemType("Deuterium"));//Since the player has ammo, consume it, and set the projectile and mana to be used
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
        public int ammocount = 0;
        public bool invopen = false;
        public override void UpdateInventory(Player player)
        {
            invopen = Main.playerInventory;//Inventory open check
            ammocount = 0;//Stop the loop from going to infinity and beyond
            for (int j = 0; j < 58; j++)
            {
                if (player.inventory[j].type == mod.ItemType("Deuterium"))
                {
                    ammocount += player.inventory[j].stack;//Vanilla code to add up all of the ammo of a specific type in inventory
                }
            }
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            //Because I don't use the normal "useAmmo" nothing appears in the inventory telling you how much ammo you have so we draw it manually
            if (ammocount != 0 && !invopen)//Only draw the ammo count if you have ammo and the inventory isn't open
            {
                ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontItemStack, ammocount.ToString(), position + new Vector2(-4f, 15f) * scale, drawColor, 0f, Vector2.Zero, new Vector2(scale * 0.8f), -1f, scale);
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ChargedBlasterCannon);
            //recipe.AddIngredient(mod.ItemType("Deuterium"), 10);//old
            recipe.AddIngredient(ItemID.SoulofMight, 10);
            recipe.AddIngredient(ItemID.SpectreBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
