using Xamarin.Forms;

namespace EleicoesUni.Service
{
    public interface ILodingPageService
    {
        void InitLoadingPage(ContentPage loadingIndicatorPage = null);

        void ShowLoadingPage();

        void HideLoadingPage();
    }
}
