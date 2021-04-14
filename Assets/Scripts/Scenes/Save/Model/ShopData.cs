namespace Assets.Scripts.Scenes.Save
{
    [System.Serializable]
    public class ShopData
    {
        [System.Serializable]
        public struct DATA_MODEL {
            public int id;
            public int price;
            public string name;
            public bool owned;
            public string material;
        }

        public DATA_MODEL[] Balls;
        public DATA_MODEL[] Platforms;
        public DATA_MODEL[] Holes;

        public DATA_MODEL[] GetByTypeName(string name)
        {
            switch (name)
            {
                case "MaterialName":
                    return Balls;
                case "HoleSkin":
                    return Holes;
                default: //PlatformSkin
                    return Platforms;
            }
        }
    }
}
