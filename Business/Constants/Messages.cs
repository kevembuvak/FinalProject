﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages // static demek, new'lenemez, Messages diye yazdığınızda var olan tek instance kullanılır
    {
        public static string ProductAdded = "Ürün Eklendi"; // public olduğu için büyük harfle başlar
        public static string ProductAddFailure = "Ürün Eklenemedi";
        public static string ProductNameInvalid = "Ürün İsmi Geçersiz";
        public static string MaintenanceTime = "Sistem Bakımda";
        public static string ProductsListed = "Ürünler Listelendi";
        public static string UnitPriceInvalid = "Ürün Ücreti Hatalı";
        public static string ExcessiveProduct = "Ürün Sınırı Aşıldı";
        public static string ProductNameAlreadyExists = "Aynı İsimde Başka Bir Ürün Var";
        public static string CategoryLimitIsReached = "Kategori Limitine Ulaşıldı";
    }
}
