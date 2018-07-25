﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Italia.DiciottoApp.Models;
using Xamarin.Essentials;

namespace Italia.DiciottoApp.Services
{
    public class ShopsService : IShopsService
    {
        public async Task<Shop> GetShopByIdAsync(string shopId)
        {
            if (string.IsNullOrWhiteSpace(shopId))
            {
                throw new ArgumentNullException("shopId");
            }

            // TODO: Get shop from 18App REST Service
            var fakeShopsService = new FakeShopsService();
            return await fakeShopsService.GetShopByIdAsync(shopId);
        }

        public async Task<IEnumerable<Shop>> NearToLocationShopsAsync(Location location, int maxItems = 10)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location");
            }

            IEnumerable<Shop> shops;

            // TODO: Get shops from 18App REST Service
            var fakeShopsService = new FakeShopsService();
            shops = await fakeShopsService.NearToLocationShopsAsync(location, maxItems);

            return shops;
        }

        public async Task<IEnumerable<Shop>> OnlineShopsAsync(Categoria category, int maxItems = 10)
        {
            IEnumerable<Shop> shops;

            // TODO: Get shops from 18App REST Service
            var fakeShopsService = new FakeShopsService();
            shops = await fakeShopsService.OnlineShopsAsync(category, maxItems);

            return shops;
        }

        public async Task<IEnumerable<Shop>> FindShopsAsync(Categoria category, Municipality municipality, string text = null, int maxItems = 10, CancellationToken ct = default(CancellationToken))
        {
            IEnumerable<Shop> shops;

            // TODO: Get shops from 18App REST Service
            var fakeShopsService = new FakeShopsService();
            shops = await fakeShopsService.FindShopsAsync(category, municipality, text, maxItems);

            return shops;
        }

        public IEnumerable<Municipality> FindMunicipality(string partialName, int maxItems = 100)
        {
            var municipalities = Municipalities.List;

            if (!string.IsNullOrWhiteSpace(partialName))
            {
                municipalities = municipalities.Where(m => m.Name.Contains(partialName));
            }

            return municipalities.Take(maxItems);
        }

    }
}
