using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamTrg.MVVM.Models;
using XamTrg.MVVM.Services;

namespace XamTrg.MVVM.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        /// <summary>
        /// Private declarations
        /// </summary>
        private ProductInfo _Product;
        private ObservableCollection<ProductInfo> _Products;
        private ProductService productService;
        private bool _IsAddEnabled;


        public ProductViewModel()
        {

            #region Instantiate public properties and service objects
            Product = new ProductInfo();
            Products = new ObservableCollection<ProductInfo>();
            productService = new ProductService();
            _IsAddEnabled = false;
            #endregion


            #region Configure Command Properties by passing private method references to it
            GetProductsCommand = new Command(GetData);
            AddProductCommand = new Command(AddProduct);
            NavigateCommand = new Command(Navigate);

            #endregion

        }

        #region Command Properties
        public ICommand GetProductsCommand { get; private set; }
        public ICommand AddProductCommand { get; private set; }
        public ICommand NavigateCommand { get; private set; }
        #endregion


        #region Public Propertie for Private declarations 
        public ProductInfo Product
        {
            get 
            {
                return _Product;
            }
            set
            {
                _Product = value;
                OnProprtyChanged("Product");
            }
        }

        public ObservableCollection<ProductInfo> Products
        {
            get
            {
                return _Products;
            }
            set
            {
                _Products = value;
                OnProprtyChanged("Products");
            }
        }

        public bool IsAddEnabled
        {
            get
            {
                return _IsAddEnabled;
            }
            set
            {
                _IsAddEnabled = value;
                OnProprtyChanged("IsAddEnabled");
            }
        }
        #endregion


        #region Private one-way methods. These method will call to Services for Read/Write operations
        private async void GetData()
        {
            // clear the Products collection
            Products.Clear();
            var productsResponse = await productService.GetProductInfoDataAsync();
            foreach (ProductInfo prd in productsResponse)
            {
                _Products.Add(prd); // call CollectionChanged and then PropertyChanged on ObservableCollection
            }
            if (_Products.Count >= 0)
            {
                IsAddEnabled = true;
            }
        }

        private async void AddProduct()
        {
            Product = await productService.PostProductInfoAsync(Product);
            if (Product.ProductRowId > 0)
            {
                // navigate to the Product List View
                await App.Current.MainPage.Navigation.PopModalAsync();
            }
        }

        private async void Navigate()
        {
            // naigate to Add Product Page
            // App.Current.MainPage, the current page object
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.AddProduct());
        }

        #endregion

    }
}
