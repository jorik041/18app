﻿using Italia.DiciottoApp.Models;
using Italia.DiciottoApp.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Italia.DiciottoApp.ViewModels
{
    public class CouponViewModel : BaseViewModel
    {
        private readonly Color red = new Color(r: .82, g: .01, b: .11);
        private readonly Color green = new Color(r: 0.13, g: 0.69, b: 0.45);
        private readonly CultureInfo ci = new CultureInfo("it-IT");

        #region Properties

        private bool justCreated;
        public bool JustCreated
        {
            get => justCreated;
            set => SetProperty(ref justCreated, value, onChanged: () =>
            {
                OnPropertyChanged(nameof(PageTitle));
            });
        }

        public string PageTitle => (Coupon?.Spent ?? false) ? "Buono utilizzato"
                                   : JustCreated ? "Buono creato"
                                   : "Dettagli del buono";

        public AppArea AppArea => AppArea.Wallet;

        public string CouponOwner => $"{Settings.UserName} {Settings.UserSurname}";

        public string ShopBkgndImageSource => (Coupon?.Shop?.Categorie?.Count() > 0) ? Coupon.Shop.Categorie[0].BkgndImageSource : null;

        public string ShopKindImageSource => (Coupon?.Shop?.IsOnline ?? false) ? "location_online_white" : "location_white";

        public string ShopAddress => (Coupon?.Shop == null) ? string.Empty
                                     : Coupon.Shop.IsOnline ? Coupon.Shop.Url
                                     : $"{Coupon.Shop.Address?.Comune} ({Coupon.Shop.Address?.SiglaProvincia})";

        public string CouponStatus =>
            Coupon == null ? string.Empty
                           : Coupon.Spent && Coupon.SpentDateTime != null ? $"Buono utilizzato il {Coupon.SpentDateTime.Value.ToString("dd MMMM yy", ci)} alle ore {Coupon.SpentDateTime.Value.ToString("hh.mm")}"
                           : JustCreated ? "Il nuovo buono è stato creato correttamente"
                           : "Buono ancora da spendere" ;

        public Color CouponStatusTextColor => (Coupon?.Spent ?? false) ? red : green;

        public bool UseCouponOnlineButtonIsVisible => (!Coupon?.Spent ?? false) && (Coupon?.Shop?.IsOnline ?? false);

        public bool ShopRouteButtonIsVisible => (!Coupon?.Spent ?? false) && (!Coupon?.Shop?.IsOnline ?? false);

        public bool CouponNotSpent => !Coupon?.Spent ?? false;

        public bool CouponSpent => Coupon?.Spent ?? false;

        public string QRcodeImageSource => "fake_qrcode";

        public string BarcodeImageSource => "fake_barcode";

        private Voucher coupon;
        public Voucher Coupon
        {
            get => coupon;
            set => SetProperty(ref coupon, value, onChanged: () =>
            {
                OnPropertyChanged(nameof(PageTitle));
                OnPropertyChanged(nameof(ShopBkgndImageSource));
                OnPropertyChanged(nameof(ShopKindImageSource));
                OnPropertyChanged(nameof(ShopAddress));
                OnPropertyChanged(nameof(CouponStatus));
                OnPropertyChanged(nameof(CouponStatusTextColor));
                OnPropertyChanged(nameof(UseCouponOnlineButtonIsVisible));
                OnPropertyChanged(nameof(ShopRouteButtonIsVisible));
                OnPropertyChanged(nameof(CouponNotSpent));
                OnPropertyChanged(nameof(CouponSpent));
                OnPropertyChanged(nameof(QRcodeImageSource));
                OnPropertyChanged(nameof(BarcodeImageSource));
            });
        }

        #endregion

        public CouponViewModel() : base()
        {
            // TODO: remove!!!
            FakeInitialize();
        }

        private void FakeInitialize()
        {
            // Title
            JustCreated = false;

            // Coupon
            string id = "DF69A8D5";
            Shop fakeShop = FakeShops.GetList().ToList()[2];
            Voucher coupon = new Voucher
            {
                Id = id,
                Category = CategoriaFromTipoCategoria(TipoCategoria.Libri),
                Product = CategoriaFromTipoCategoria(TipoCategoria.Libri).Prodotti[0],
                RequestedValue = 12.34,
                ValidatedValue = 0.0,
                QrCodeValue = id,
                BarCodeValue = id,
                ShopId = fakeShop.Id,
                Shop = fakeShop,
                Spent = true
            };

            Coupon = coupon;
        }

        private static Categoria CategoriaFromTipoCategoria(TipoCategoria tipoCategoria)
        {
            return Categoria.List.SingleOrDefault(c => c.Tipo == tipoCategoria);
        }

    }
} 