using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using Microsoft.Maui.Networking;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                // 🔹 Verifica se o campo está vazio
                if (string.IsNullOrWhiteSpace(txt_cidade.Text))
                {
                    await DisplayAlert(
                        "Atenção",
                        "Preencha o nome da cidade.",
                        "OK");
                    return;
                }

                // 🔹 Verifica conexão com internet
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    await DisplayAlert(
                        "Sem conexão",
                        "Verifique sua conexão com a internet.",
                        "OK");
                    return;
                }

                // 🔹 Chama o serviço
                Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                // 🔹 Cidade não encontrada ou erro
                if (t == null)
                {
                    await DisplayAlert(
                        "Cidade não encontrada",
                        "Verifique o nome digitado.",
                        "OK");

                    lbl_res.Text = "";
                    return;
                }

                // 🔹 Monta resultado
                string dados_previsao =
                    $"Tempo: {t.main}  \n" +
                    $"Descrição: {t.description}  \n" +
                    $"Temperatura: {t.temp}°C  \n" +
                    $"Visibilidade: {t.visibility}  \n" +
                    $"Velocidade do vento: {t.speed} m/s";

                lbl_res.Text = dados_previsao;
            }
            catch (Exception ex)
            {
                await DisplayAlert(
                    "Erro",
                    ex.Message,
                    "OK");
            }
        }
    }
}