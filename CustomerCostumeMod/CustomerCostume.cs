using Kitchen;
using KitchenData;
using KitchenMods;
using System.Collections.Generic;
using System.Linq;

namespace CustomerCostumeMod.System
{
    public class CustomerCostumeSystem : GameSystemBase, IModSystem
    {
        public List<PlayerCosmetic> Cosmetics = new List<PlayerCosmetic>();
        //public List<PlayerCosmetic> Hats = new List<PlayerCosmetic>();
        //public List<PlayerCosmetic> Outfits = new List<PlayerCosmetic>();
        //public List<PlayerCosmetic> Others = new List<PlayerCosmetic>();
        public List<CustomerType> Types = new List<CustomerType>();
        public CustomerType CustomerType = new CustomerType();

        protected override void Initialise()
        {
            base.Initialise();

            Cosmetics = GameData.Main.Get<PlayerCosmetic>().ToList(); // Storing all Cosmetics into a List of PlayerCosmetics
            Types = GameData.Main.Get<CustomerType>().ToList(); //

            
            foreach (var type in Types) // Iterating through CustomerTypes
            {
                if (type.name.ToString().Equals("Generic Customer")) // Checking for CusterType of "Generic Customer"
                {
                    CustomerType = type;
                }
            }

            foreach (var cosmetic in Cosmetics) // Iterating through List of PlayerCosmetics
            {
                if (cosmetic.CosmeticType.ToString().Equals("Hat") && Main.PrefManager.Get<bool>(Main.COSTUME_HATS_ENABLED_ID)) // Checking for CosmeticType of "Hat"
                {
                    //Hats.Add(cosmetic); // Storing Hat Cosmetics to Hats List
                    CustomerType.Cosmetics.Add(cosmetic); // Adding Hats to PlayerCosmetic List in "Generic Customer" CustomerType
                }
                else if (cosmetic.CosmeticType.ToString().Equals("Outfit") && Main.PrefManager.Get<bool>(Main.COSTUME_OUTFITS_ENABLED_ID)) // Checking for CosmeticType of "Outfit"
                {
                    //Outfits.Add(cosmetic); // Adding Outfits to PlayerCosmetic List in "Generic Customer" CustomerType
                    CustomerType.Cosmetics.Add(cosmetic); // Storing Outfit Cosmetics to Outfits List
                }
                else // Checking for CosmeticType of anything that isn't a Hat or Outfit, aka Capes
                {
                    //Others.Add(cosmetic); // Storing Other Cosmetics to Others List
                    //CustomerType.Cosmetics.Add(cosmetic); // Not Sure If I should add or not because they are disabled
                }
            }
        }

        protected override void OnUpdate()
        {
            
        }
    }   
}