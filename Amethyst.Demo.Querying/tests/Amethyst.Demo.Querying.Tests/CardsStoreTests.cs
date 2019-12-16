using System;
using System.Threading.Tasks;
using Amethyst.Demo.Querying.Models.Entities;
using Amethyst.Demo.Querying.Store;
using Amethyst.Demo.Querying.Tests.Infrastructure;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Amethyst.Demo.Querying.Tests
{
    public sealed class CardsStoreTests : StoreBase
    {
        [Fact]
        public async Task Add()
        {
            var store = new CardsStore(Context);
            var card = Fixture.Create<Card>();

            await store.AddAsync(card);

            var actual = await Context.Cards.SingleOrDefaultAsync(x => x.CardId == card.CardId);
            actual.Should().NotBeNull();
        }

        [Fact]
        public async Task Exists_True()
        {
            var store = new CardsStore(Context);
            var card = Fixture.Create<Card>();

            Context.Cards.Add(card);
            await Context.SaveChangesAsync();

            var actual = await store.ExistsAsync(card.CardId);
            actual.Should().BeTrue();
        }
        
        [Fact]
        public async Task Exists_False()
        {
            var store = new CardsStore(Context);
            var card = Fixture.Create<Card>();

            var actual = await store.ExistsAsync(card.CardId);
            actual.Should().BeFalse();
        }

        [Fact]
        public async Task Update()
        {
            var store = new CardsStore(Context);
            var card = Fixture.Create<Card>();

            await store.AddAsync(card);
            await Context.SaveChangesAsync();

            const string newName = "New card name";
            
            card.Rename(newName);
            await store.UpdateAsync(card);

            var actual = await Context.Cards.SingleOrDefaultAsync(x => x.CardId == card.CardId);
            actual.Name.Should().Be(newName);
        }
        
        [Fact]
        public async Task Get_Success()
        {
            var store = new CardsStore(Context);
            var card = Fixture.Create<Card>();

            Context.Cards.Add(card);
            await Context.SaveChangesAsync();

            var actual = await store.GetAsync(card.CardId);
            actual.Should().BeEquivalentTo(card);
        }

        [Fact]
        public async Task Get_Fail()
        {
            var store = new CardsStore(Context);
            var card = Fixture.Create<Card>();

            Func<Task> getTask = () => store.GetAsync(card.CardId);

            await getTask.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}