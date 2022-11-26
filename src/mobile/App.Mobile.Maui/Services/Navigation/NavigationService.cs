using App.Mobile.Maui.ViewModels;
using App.Mobile.Maui.Views;

namespace App.Mobile.Maui.Services.Navigation
{
    public class NavigationService
    {
        static readonly Lazy<NavigationService> _Lazy = new(() => new NavigationService());

        public static NavigationService Current { get => _Lazy.Value; }

        NavigationService()
        {
            _Mappings = new Dictionary<Type, Type>();
            MapearViewModels();
        }

        Application CurrentApplication => Application.Current ?? throw new Exception("erro ao inicializar navegação");

        INavigation Navigation
        {
            get
            {
                var page = (CurrentApplication.MainPage as CustomNavigationPage)!;
                return page.Navigation;
            }
        }

        readonly Dictionary<Type, Type> _Mappings;

        void MapearViewModels()
        {
            _Mappings.Add(typeof(LoginPageViewModel), typeof(LoginPage));
            _Mappings.Add(typeof(CadastroPageViewModel), typeof(CadastroPage));
            _Mappings.Add(typeof(HomePageViewModel), typeof(HomePage));
            _Mappings.Add(typeof(ProdutoDetalhePageViewModel), typeof(ProdutoDetalhePage));
            _Mappings.Add(typeof(CarrinhoPageViewModel), typeof(CarrinhoPage));
        }

        internal Task Navegar<TViewModel>(object? parameter = null) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        async Task InternalNavigateToAsync(Type viewModelType, object? parameter = null)
        {
            try
            {
                Page? page = null;
                List<Page>? pagesToRemove = null;

                if (viewModelType == typeof(HomePageViewModel))
                {
                    if (Navigation.NavigationStack.Any(lbda =>
                        lbda.BindingContext.GetType() == typeof(HomePageViewModel)) is false)
                    {
                        pagesToRemove = Navigation.NavigationStack.ToList();

                        page = CreateAndBindPage(viewModelType);

                        await Navigation.PushAsync(page);
                    }
                    else
                        await GoBack(toRoot: true);
                }
                else
                {
                    page = CreateAndBindPage(viewModelType);

                    if (viewModelType.BaseType == typeof(BaseModalPageViewModel))
                        await Navigation.PushModalAsync(page, true);
                    else
                        await Navigation.PushAsync(page, true);
                }

                if (page is not null) await ((BaseViewModel)page.BindingContext).Initialize(parameter);

                if (pagesToRemove is null) return;

                foreach (var pageToRemove in pagesToRemove)
                    Navigation.RemovePage(pageToRemove);
            }
            catch (Exception)
            {
                throw;
            }
        }

        Type GetPageTypeForViewModel(Type viewModelType)
        {
            try
            {
                return _Mappings.ContainsKey(viewModelType) is false
                    ? throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings")
                    : _Mappings[viewModelType];
            }
            catch (Exception)
            {
                throw;
            }
        }

        Page CreateAndBindPage(Type viewModelType)
        {
            try
            {
                Type pageType = GetPageTypeForViewModel(viewModelType);

                if (pageType is null) throw new Exception($"Mapping type for {viewModelType} is not a page");

                Page page = Activator.CreateInstance(pageType) as Page ??
                    throw new Exception("Erro ao Bindar ViewModel");

                page.BindingContext = Activator.CreateInstance(viewModelType) as BaseViewModel;

                return page;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task GoBack(bool toRoot = false, bool animated = true)
        {
            if (toRoot) return Navigation.PopToRootAsync(animated);

            if (Navigation.ModalStack.Count > 0) return Navigation.PopModalAsync(animated);

            return Navigation.PopAsync(animated);
        }

        internal void Initialize(object? args = null)
        {
            CurrentApplication.MainPage = new CustomNavigationPage(new LoginPage() { BindingContext = new LoginPageViewModel() });
            MainThread.BeginInvokeOnMainThread(async () => await ((BaseViewModel)CurrentApplication.MainPage.Navigation.NavigationStack.First().BindingContext).Initialize(args));
        }
    }
}
