using System;
using System.Collections;
using FluentAssertions;
using Machine.Specifications;
using Ploeh.AutoFixture;
using StructureMap;

namespace Demo.PracticalOO.CSharp.Chapter1SRP
{
    [Subject(typeof(Gear))]
    public class when_initializing_with_hashtable
    {
        private static Hashtable _args;
        private static int _chainring;
        private static int _cog;
        private static readonly Fixture Fixture = new Fixture();
        private static Gear _subject;

        private Establish context = () =>
        {
            _chainring = Fixture.Create<int>();
            _cog = Fixture.Create<int>();

            _args = new Hashtable {{"chainring", _chainring}, {"cog", _cog}};
        };

        private Because of = () => _subject = new Gear(_args);

        private It should_return_proper_ratio = () => _subject.Ratio.Should().Be(_chainring/Convert.ToDouble(_cog));
    }

    [Subject(typeof(Gear))]
    public class when_initializing_with_values
    {
        private static int _chainring;
        private static int _cog;
        private static readonly Fixture Fixture = new Fixture();
        private static Gear _subject;

        private Establish context = () =>
        {
            _chainring = Fixture.Create<int>();
            _cog = Fixture.Create<int>();
        };

        private Because of = () => _subject = new Gear(_chainring, _cog);

        private It should_return_proper_ratio = () => _subject.Ratio.Should().Be(_chainring/Convert.ToDouble(_cog));

        private Cleanup after = () => _subject = null;
    }

    [Subject(typeof(Gear))]
    public class when_initializing_with_gear_arguments
    {
        private static Gear.GearArguments _args;
        private static int _chainring;
        private static int _cog;
        private static readonly Fixture Fixture = new Fixture();
        private static Gear _subject;

        private Establish context = () =>
        {
            _chainring = Fixture.Create<int>();
            _cog = Fixture.Create<int>();

            _args = new Gear.GearArguments { Chainring = _chainring, Cog = _cog };
        };

        private Because of = () => _subject = new Gear(_args);

        private It should_return_proper_ratio = () => _subject.Ratio.Should().Be(_chainring / Convert.ToDouble(_cog));
    }

    [Subject(typeof(Gear))]
    public class when_initializing_with_dynamic_argument
    {
        private static int _chainring;
        private static int _cog;
        private static readonly Fixture Fixture = new Fixture();
        private static Gear _subject;

        private Establish context = () =>
        {
            _chainring = Fixture.Create<int>();
            _cog = Fixture.Create<int>();
        };

        private Because of = () => _subject = new Gear(new { Chainring = _chainring, Cog = _cog });

        private It should_return_proper_ratio = () => _subject.Ratio.Should().Be(_chainring / Convert.ToDouble(_cog));
    }

    [Subject(typeof(Gear))]
    public class when_initializing_with_structuremap
    {
        private static readonly Fixture Fixture = new Fixture();
        private static Gear _subject;

        Establish context = () => ObjectFactory.Configure(
            x => x.For<Gear>().Use(container => new Gear(container.GetInstance<Gear.GearArguments>())));

        private Because of = () => _subject = ObjectFactory.GetInstance<Gear>();

        private It should_calculate_ratio_based_on_defaults = () => _subject.Ratio.Should().Be(Gear.CHAINRING_DEFAULT / Convert.ToDouble(Gear.COG_DEFAULT));
    }
}
