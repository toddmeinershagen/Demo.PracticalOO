using System;
using System.Collections;

namespace Demo.PracticalOO.CSharp.Chapter1SRP
{
    public class Gear
    {
        public Gear(int chainring, int cog)
        {
            Chainring = chainring;
            Cog = cog;
        }

        public Gear(IDictionary args)
        {
            Chainring = args.Fetch("chainring", CHAINRING_DEFAULT);
            Cog = args.Fetch("cog", COG_DEFAULT);
        }

        public const int CHAINRING_DEFAULT = 11;
        public const int COG_DEFAULT = 52;

        public Gear(GearArguments args)
        {
            Chainring = args.GetProperty(x => x.Chainring, CHAINRING_DEFAULT);
            Cog = args.GetProperty(x => x.Cog, COG_DEFAULT);
        }

        public Gear(object args)
        {
            Chainring = args.GetProperty("Chainring", CHAINRING_DEFAULT);
            Cog = args.GetProperty("Cog", COG_DEFAULT);
        }

        public class GearArguments : Defaultable<GearArguments>
        {
            public int Chainring { get; set; }
            public int Cog { get; set; }
        }

        public int Chainring { get; private set; }
        public int Cog { get; private set; }

        public double Ratio
        {
            get { return Chainring/Convert.ToDouble(Cog); }
        }}
}